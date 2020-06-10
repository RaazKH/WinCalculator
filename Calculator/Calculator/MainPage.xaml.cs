using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI;
using System.Diagnostics;
using Windows.System;
using Windows.Security.Cryptography.Certificates;
using Windows.UI.Core;
using Windows.Devices.AllJoyn;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using System.Linq.Expressions;
using System.Data;
using Windows.Networking.Sockets;

namespace Calculator
{
    public sealed partial class MainPage : Page
    {
        double prevResult = 0;
        bool resultDisplayed = false;
        bool displayedDecimal = false;
        int openPCount = 0;
        int closedPCount = 0;
        int resultLen = 0;
        
        public MainPage()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.CharacterReceived += CoreWindow_CharacterReceived;
        }
         
        // when a number or decimal is entered
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (resultDisplayed == true)
            {
                Button_Special(bclear, e);
                resultDisplayed = false;
            }

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
                }
                else
                {
                    textBox1.Text += texr;
                }
                closedPCount++;
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
            catch (System.Data.SyntaxErrorException ee)
            {
                string result = " = ERROR";
                resultLen = result.Length;
                textBox1.Text += result;
                resultDisplayed = true;
                Debug.WriteLine("Error with input: " + eval);
            }
        }

        // keyinput method
        private void Grid_KeyPressed(object sender, KeyRoutedEventArgs e)
        {
            // Handle shift + 8 for * and shift + 187 for +
            var shiftState = CoreWindow.GetForCurrentThread().GetKeyState(VirtualKey.Shift);
            if ((shiftState & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down)
            {
                if (e.Key == VirtualKey.Number8)
                {
                    Button_Symbol(btimes, e);
                }
                else if ((int)e.Key == 187)
                {
                    Button_Symbol(bplus, e);
                }
                else if (e.Key == VirtualKey.Number9)
                {
                    Button_Symbol(bopenP, e);
                }
                else if (e.Key == VirtualKey.Number0)
                {
                    Button_Symbol(bcloseP, e);
                }
                else if (e.Key != VirtualKey.Shift)
                {
                    Debug.WriteLine("Not mapped, keycode = Shift + " + e.Key);
                }
            }

            // The remaining keys will only work if shift is not being held
            else if(e.Key == VirtualKey.Number1 || e.Key == VirtualKey.NumberPad1)
            {
                Button_Click(b1, e);
            }
            else if (e.Key == VirtualKey.Number2 || e.Key == VirtualKey.NumberPad2)
            {
                Button_Click(b2, e);
            }
            else if (e.Key == VirtualKey.Number3 || e.Key == VirtualKey.NumberPad3)
            {
                Button_Click(b3, e);
            }
            else if (e.Key == VirtualKey.Number4 || e.Key == VirtualKey.NumberPad4)
            {
                Button_Click(b4, e);
            }
            else if (e.Key == VirtualKey.Number5 || e.Key == VirtualKey.NumberPad5)
            {
                Button_Click(b5, e);
            }
            else if (e.Key == VirtualKey.Number6 || e.Key == VirtualKey.NumberPad6)
            {
                Button_Click(b6, e);
            }
            else if (e.Key == VirtualKey.Number7 || e.Key == VirtualKey.NumberPad7)
            {
                Button_Click(b7, e);
            }
            else if (e.Key == VirtualKey.Number8 || e.Key == VirtualKey.NumberPad8)
            {
                Button_Click(b8, e);
            }
            else if (e.Key == VirtualKey.Number9 || e.Key == VirtualKey.NumberPad9)
            {
                Button_Click(b9, e);
            }
            else if (e.Key == VirtualKey.Number0 || e.Key == VirtualKey.NumberPad0)
            {
                Button_Click(b0, e);
            }
            else if (e.Key == VirtualKey.Add)
            {
                Button_Symbol(bplus, e);
            }
            else if (e.Key == VirtualKey.Subtract || (int)e.Key == 189)
            {
                Button_Symbol(bminus, e);
            }
            else if (e.Key == VirtualKey.Multiply)
            {
                Button_Symbol(btimes, e);
            }
            else if (e.Key == VirtualKey.Divide || (int)e.Key == 191)
            {
                Button_Symbol(bdivided, e);
            }
            else if (e.Key == VirtualKey.Decimal || (int)e.Key == 190)
            {
                Button_Click(bdecimal, e);
            }
            else if (e.Key == VirtualKey.Back)
            {
                Button_Special(bbackspace, e);
            }
            else if (e.Key == VirtualKey.Delete)
            {
                Button_Special(bclear, e);
            }
            else
            {
                Debug.WriteLine("Not mapped, keycode = " + e.Key);
            }
        }

        private void CoreWindow_CharacterReceived(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.CharacterReceivedEventArgs args)
        {
            RoutedEventArgs e = new RoutedEventArgs();
            if (args.KeyCode == 13) //Enter
            {
                Button_Evaluate(bequals, e);
            }
        }
    }

    
}
