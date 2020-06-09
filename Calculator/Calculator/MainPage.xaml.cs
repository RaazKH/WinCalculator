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

namespace Calculator
{
    public sealed partial class MainPage : Page
    {
        bool resultDisplayed = false;
        public MainPage()
        {
            this.InitializeComponent();

            // need this too for some reason
            //this.Loaded += delegate { this.Focus(FocusState.Programmatic); };

            // initializes keyboard input on app startup
            // without this a button will need to be hit on the screen before keyboard works
            //this.Loaded += delegate { this.Focus(FocusState.Keyboard); };
            
        }
         
        private void button_click(object sender, RoutedEventArgs e)
        {
            if (resultDisplayed == true)
            {
                return;
            }

            Button clickedButton = (sender as Button);
            var texr = clickedButton.Tag.ToString();

            if (textBox1.Text == "0" && texr != ".")
            {
                textBox1.Text = texr;
            }
            else
            {
                textBox1.Text = textBox1.Text + texr;
            }
        }

        // when backspace or clear are used
        private void button_special(object sender, RoutedEventArgs e)
        {
            string texr = (sender as Button).Tag.ToString();
            if (texr == "clear" || resultDisplayed == true)
            {
                textBox1.Text = "0";
            }
            else if (texr == "back" && textBox1.Text.Length > 1)
            {
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
            }
            else if (texr == "back" && textBox1.Text.Length == 1 || resultDisplayed == true)
            {
                textBox1.Text = "0";
            }

            resultDisplayed = false;
        }

        // when the calculation needs to be made
        private void button_evaluate (object sender, RoutedEventArgs e)
        {
            if(resultDisplayed == true)
            {
                return;
            }

            string eval = textBox1.Text;
            int len = eval.Length;
            string temp = "";
            string[] operation = new string[len];
            int pos = 0;
            foreach (char a in eval)
            {
                if (a == '/' || a == '*' || a == '+' || a == '-') // add ()
                {
                    operation[pos] = temp;
                    
                    operation[pos + 1] = ""+a;

                    pos = pos + 2;
                    temp = "";
                }
                else
                {
                    temp += a;
                }
            }
            operation[pos] = temp;

            foreach (string a in operation)
            {
                Debug.WriteLine(operation.Length);
            }

            // evaluate
            double num = Convert.ToDouble(operation[0]);
            for (int i = 0; i < operation.Length; i++)
            {
                if (operation[i] == null)
                    break;

                if (i % 2 != 0)
                {
                    if (operation[i] == "+")
                    {
                        num += Convert.ToDouble(operation[i + 1]);
                    }
                    else if (operation[i] == "*")
                    {
                        num *= Convert.ToDouble(operation[i + 1]);
                    }
                    else if (operation[i] == "/")
                    {
                        num /= Convert.ToDouble(operation[i + 1]);
                    }
                    else if (operation[i] == "-")
                    {
                        num -= Convert.ToDouble(operation[i + 1]);
                    }
                }
            }


            textBox1.Text += " = " + num;
            resultDisplayed = true;

            // print result


            /**
            for (int i = 0; i < len; i++)
            {
                char a = eval[i];
                if (a == '/' || a == '*' || a == '+' || a == '-')
                {
                    // store temp in the string array and store the operator in the next position
                    // increment array pos by 2
                }

            }**/






            // check for valid characters
            // 40 = (
            // 41 = )
            // 42 = *
            // 43 = +
            // 44 = , IGNORE
            // 45 = -
            // 46 = .
            // 47 = /
            // 48-57 = 0-9 
            // 120 = x
            // 247 = ÷
            /**
            for (int i = 0; i < len; i++)
            {
                int a = (int)eval[i];
                if ((a >= 40 && a < 44) || (a > 44 && a <= 57))
                {
                    // valid
                }
                else
                {
                    Debug.WriteLine("Invalid character:" + a);
                    return;
                }
                
            }
            **/
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
                    button_click(btimes, e);
                }
                else if ((int)e.Key == 187)
                {
                    button_click(bplus, e);
                }
                else
                {
                    Debug.WriteLine("Not mapped, keycode = Shift + " + e.Key);
                }
            }

            // The remaining keys will only work if shift is not being held
            else if(e.Key == VirtualKey.Number1 || e.Key == VirtualKey.NumberPad1)
            {
                button_click(b1, e);
            }
            else if (e.Key == VirtualKey.Number2 || e.Key == VirtualKey.NumberPad2)
            {
                button_click(b2, e);
            }
            else if (e.Key == VirtualKey.Number3 || e.Key == VirtualKey.NumberPad3)
            {
                button_click(b3, e);
            }
            else if (e.Key == VirtualKey.Number4 || e.Key == VirtualKey.NumberPad4)
            {
                button_click(b4, e);
            }
            else if (e.Key == VirtualKey.Number5 || e.Key == VirtualKey.NumberPad5)
            {
                button_click(b5, e);
            }
            else if (e.Key == VirtualKey.Number6 || e.Key == VirtualKey.NumberPad6)
            {
                button_click(b6, e);
            }
            else if (e.Key == VirtualKey.Number7 || e.Key == VirtualKey.NumberPad7)
            {
                button_click(b7, e);
            }
            else if (e.Key == VirtualKey.Number8 || e.Key == VirtualKey.NumberPad8)
            {
                button_click(b8, e);
            }
            else if (e.Key == VirtualKey.Number9 || e.Key == VirtualKey.NumberPad9)
            {
                button_click(b9, e);
            }
            else if (e.Key == VirtualKey.Number0 || e.Key == VirtualKey.NumberPad0)
            {
                button_click(b0, e);
            }
            else if (e.Key == VirtualKey.Add)
            {
                button_click(bplus, e);
            }
            else if (e.Key == VirtualKey.Subtract || (int)e.Key == 189)
            {
                button_click(bminus, e);
            }
            else if (e.Key == VirtualKey.Multiply)
            {
                button_click(btimes, e);
            }
            else if (e.Key == VirtualKey.Divide || (int)e.Key == 191)
            {
                button_click(bdivided, e);
            }
            else if (e.Key == VirtualKey.Decimal || (int)e.Key == 190)
            {
                button_click(bdecimal, e);
            }
            else if (e.Key == VirtualKey.Back)
            {
                button_special(bbackspace, e);
            }
            else if (e.Key == VirtualKey.Delete)
            {
                button_special(bclear, e);
            }
            else if (e.Key == VirtualKey.Enter || (int)e.Key == 187) // DOESNT WORK!!!!!!!!!!!!!!!!!!!!!!!!!!
            {
                button_evaluate(bequals, e);
            }
            else
            {
                Debug.WriteLine("Not mapped, keycode = " + e.Key);
            }
        }
    }
}
