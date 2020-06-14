using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using System.Diagnostics;
using Windows.System;
using System.Data;
using System.Drawing;
using Windows.UI.Xaml.Media;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using System.Linq;

namespace Calculator
{
    public sealed partial class MainPage : Page
    {
        // theme managment
        bool boxVis = false;
        ApplicationDataContainer localSettings;

        //  result management
        double prevResult = 0;
        int resultLen = 0;
        bool resultDisplayed = false;

        // input and evaluation control
        bool displayedDecimal = false;
        int openPCount = 0;
        int closedPCount = 0;
        
        public MainPage()
        {
            this.InitializeComponent();

            // Initializes C# Keyinput
            Window.Current.CoreWindow.CharacterReceived += KeyPressed;

            // Hide titlebar panel and add new layout to title bar
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(UserLayout);

            // Initialize settings
            localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localSettings.Values.Count() == 0)
            {
                comboB.PlaceholderText = "Amoeba";
            }
            else
            {
                string bg = localSettings.Values["Background"] as string;
                comboB.PlaceholderText = bg;
                changeBG(bg);
            }
        }
         
        // when a number or decimal is entered
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // clear result
            if (resultDisplayed == true)
            {
                Button_Special(bclear, e);
                resultDisplayed = false;
            }

            // evaluate input before sending to textbox
            Button clickedButton = (sender as Button);
            var texr = clickedButton.Tag.ToString();
            if (textBox1.Text == "0" && texr != ".")
            {
                textBox1.Text = texr;
            }
            else
            {
                if (textBox1.Text[textBox1.Text.Length - 1] == ')')
                {
                    textBox1.Text += "*" + texr;
                }
                else if (texr == ".")
                {
                    if(displayedDecimal == false)
                    {
                        textBox1.Text += texr;
                        displayedDecimal = true;
                    }
                }
                else
                {
                    textBox1.Text += texr;
                }
            }
        }

        // when an operation is sent
        private void Button_Symbol(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (sender as Button);
            var texr = clickedButton.Tag.ToString();
            var text = textBox1.Text;
            var len = text.Length;

            if (text[len-1] == '.')
            {
                return;
            }

            if (resultDisplayed == true)
            {
                Button_Special(bclear, e);
                textBox1.Text = "" + prevResult;
                resultDisplayed = false;
            }

            if (text == "0" && (texr == "(" || texr == "-"))
            {
                textBox1.Text = texr;
                if (texr == "(")
                    openPCount++;
            }
            else if (texr == "(")
            {
                if (text[len - 1] != '+' && text[len - 1] != '*' && text[len - 1] != '-' && text[len - 1] != '/' && text[len - 1] != '(')
                {
                    textBox1.Text = textBox1.Text + "*" + texr;
                }
                else
                {
                    textBox1.Text += texr;
                }
                //Debug.WriteLine();
                openPCount++;
            }
            else if (texr == ")")
            {
                if (text[len - 1] == '(')
                {
                    textBox1.Text = textBox1.Text + "0" + texr;
                    closedPCount++;
                }
                else if(closedPCount < openPCount)
                {
                    textBox1.Text += texr;
                    closedPCount++;
                }
                
            }
            else
            {
                textBox1.Text += texr;
            }

            displayedDecimal = false;
        }

        // when backspace or clear are used
        private void Button_Special(object sender, RoutedEventArgs e)
        {
            string texr = (sender as Button).Tag.ToString();
            if (texr == "clear")
            {
                openPCount = 0;
                closedPCount = 0;
                displayedDecimal = false;
                textBox1.Text = "0";
            }
            else if (texr == "back" && resultDisplayed == true)
            {
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - resultLen);
            }
            else if (texr == "back" && textBox1.Text.Length > 1)
            {
                if(textBox1.Text[textBox1.Text.Length-1] == '(')
                {
                    openPCount--;
                }
                else if (textBox1.Text[textBox1.Text.Length - 1] == ')')
                {
                    closedPCount--;
                }
                else if (textBox1.Text[textBox1.Text.Length - 1] == '.')
                {
                    displayedDecimal = false; ;
                }
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
            }
            else if (texr == "back" && textBox1.Text.Length == 1)
            {
                if (textBox1.Text[0] == '(')
                {
                    openPCount = 0;
                }
                else if (textBox1.Text[0] == '.')
                {
                    displayedDecimal = false; ;
                }
                textBox1.Text = "0";
            }
            resultDisplayed = false;
        }

        // when the calculation needs to be made
        private void Button_Evaluate (object sender, RoutedEventArgs e)
        {
            if (resultDisplayed == true)
            {
                return;
            }
            if (closedPCount != openPCount)
            {
                return;
            }

            string eval = textBox1.Text;
            DataTable dt = new DataTable();
            try
            {
                prevResult = Convert.ToDouble(dt.Compute(eval, ""));
                string result = " = " + prevResult;
                resultLen = result.Length;

                // Display
                textBox1.Text += result;
                resultDisplayed = true;
            }
            catch (System.Data.SyntaxErrorException)
            {
                string result = " = ERROR";
                resultLen = result.Length;
                textBox1.Text += result;
                resultDisplayed = true;
                Debug.WriteLine("Error with input: " + eval);
            }
            catch (System.Data.EvaluateException)
            {
                // Somehow they put text in the box
            }
        }

        // Top left settings icon
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            if (boxVis == false)
            {
                comboB.Width = 200;
                boxVis = true;
            }
            else
            {
                comboB.Width = 0;
                boxVis = false;
                bequals.Focus(FocusState.Programmatic);
            }
        }

        // This method covers all the keys which did not have proper accelerators in the XAML
        private void KeyPressed(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.CharacterReceivedEventArgs key)
        {
            RoutedEventArgs e = new RoutedEventArgs();
            if (key.KeyCode == 42) // *
            {
                Button_Symbol(btimes, e);
            }
            else if (key.KeyCode == 43) // +
            {
                Button_Symbol(bplus, e);
            }
            else if (key.KeyCode == 45) // -
            {
                Button_Symbol(bminus, e);
            }
            else if (key.KeyCode == 46) // .
            {
                Button_Click(bdecimal, e);
            }
            else if (key.KeyCode == 47) // /
            {
                Button_Symbol(bdivided, e);
            }
            else if (key.KeyCode == 61) // =
            {
                Button_Evaluate(bequals, e);
            }
            else
            {
                Debug.WriteLine("Not mapped, keycode = " + key.KeyCode);
            }
        }

        private void comboB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   string bg = comboB.SelectedItem.ToString();
            localSettings.Values["Background"] = comboB.SelectedItem.ToString();
            changeBG(bg);

            // focus the enter key
            bequals.Focus(FocusState.Programmatic);
            // close the button
        }

        // write background selector method which takes a string and 
        private void changeBG(string s)
        {
            switch (s)
            {
                case "Amoeba":
                    page.Background = new SolidColorBrush() { Opacity = 1, Color = Windows.UI.Colors.LightCoral };
                    break;
                case "Black":
                    page.Background = new SolidColorBrush() { Opacity = 1, Color = Windows.UI.Colors.Black };
                    break;
                case "Blue":
                    page.Background = new SolidColorBrush() { Opacity = 1, Color = Windows.UI.Colors.RoyalBlue };
                    break;
                case "Brown":
                    page.Background = new SolidColorBrush() { Opacity = 1, Color = Windows.UI.Colors.SaddleBrown };
                    break;
                case "Green":
                    page.Background = new SolidColorBrush() { Opacity = 1, Color = Windows.UI.Colors.LightGreen };
                    break;
                case "Gray":
                    page.Background = new SolidColorBrush() { Opacity = 1, Color = Windows.UI.Colors.DimGray };
                    break;
                case "Pink":
                    page.Background = new SolidColorBrush() { Opacity = 1, Color = Windows.UI.Colors.Plum };
                    break;
                case "Purple":
                    page.Background = new SolidColorBrush() { Opacity = 1, Color = Windows.UI.Colors.MediumPurple };
                    break;
                case "Olive":
                    page.Background = new SolidColorBrush() { Opacity = 1, Color = Windows.UI.Colors.DarkOliveGreen };
                    break;
                case "Red":
                    page.Background = new SolidColorBrush() { Opacity = 1, Color = Windows.UI.Colors.Tomato };
                    break;
                case "Salmon":
                    page.Background = new SolidColorBrush() { Opacity = 1, Color = Windows.UI.Colors.LightSalmon };
                    break;
                case "Sky":
                    page.Background = new SolidColorBrush() { Opacity = 1, Color = Windows.UI.Colors.LightSkyBlue };
                    break;
                case "Steel":
                    page.Background = new SolidColorBrush() { Opacity = 1, Color = Windows.UI.Colors.LightSlateGray };
                    break;
                default:
                    page.Background = new SolidColorBrush() { Opacity = 1, Color = Windows.UI.Colors.LightCoral };
                    break;
            }
        }
    }
}
