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

namespace Calculator
{
    public sealed partial class MainPage : Page
    {
        bool resultDisplayed = false;
        int openPCount = 0;
        int closedPCount = 0;
        int resultLen = 0;
        public MainPage()
        {
            this.InitializeComponent();
        }
         
        // when a number or decimal is entered
        private void button_click(object sender, RoutedEventArgs e)
        {
            if (resultDisplayed == true)
            {
                button_special(bclear, e);
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
                    textBox1.Text = textBox1.Text + "*" + texr;
                }
                else
                {
                    textBox1.Text = textBox1.Text + texr;
                }
            }
        }

        // when an operation is sent
        private void button_symbol(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (sender as Button);
            var texr = clickedButton.Tag.ToString();
            var text = textBox1.Text;
            var len = text.Length;

            if (resultDisplayed == true)
            {
                if (texr != "(")
                {
                    return;
                }
                button_special(bclear, e);
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
                    textBox1.Text = textBox1.Text + texr;
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
                    textBox1.Text = textBox1.Text + texr;
                }
                closedPCount++;
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
            if (texr == "clear")
            {
                openPCount = 0;
                closedPCount = 0;

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
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
            }
            else if (texr == "back" && textBox1.Text.Length == 1)
            {
                if (textBox1.Text[0] == '(')
                {
                    openPCount = 0;
                }
                textBox1.Text = "0";
            }
            resultDisplayed = false;
        }

        // when the calculation needs to be made
        private void button_evaluate (object sender, RoutedEventArgs e)
        {

            if (resultDisplayed == true)
            {
                return;
            }

            if (closedPCount != openPCount)
            {
                return;
            }

            // Step 2: Split and store
            string eval = textBox1.Text;
            //string temp = "";
            //List<string> operation = new List<string>();
            //int pos = 0;



            // validate the expression 
            // - Are the () balanced
            // - Are the operators entered correctly
            // - Are there too many . in a number


            /**
           
            foreach (char a in eval)
            {
                if(a == '(')
                {
                    if (temp.Length > 0)
                    {
                        // insert multiplication
                    }
                }
                else if (a == '/' || a == '*' || a == '+' || a == '-') // add ()
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
            // check if temp length is > 0?
            operation[pos] = temp;

            // Step 1.1: Debug step
            foreach (string a in operation)
            {
                Debug.WriteLine(a);
            }


            // Step 2: Validate
            double num = Convert.ToDouble(operation[0]);
            for (int i = 0; i < operation.Count; i++)
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
             **/
            // Step 3: Calculate
            DataTable dt = new DataTable();


   
            // Step 4: Display
            string result = " = " + dt.Compute(eval, "");
            resultLen = result.Length;
            textBox1.Text += result;
            resultDisplayed = true;
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
                    button_symbol(btimes, e);
                }
                else if ((int)e.Key == 187)
                {
                    button_symbol(bplus, e);
                }
                else if (e.Key == VirtualKey.Number9)
                {
                    button_symbol(bopenP, e);
                }
                else if (e.Key == VirtualKey.Number0)
                {
                    button_symbol(bcloseP, e);
                }
                else if (e.Key != VirtualKey.Shift)
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
                button_symbol(bplus, e);
            }
            else if (e.Key == VirtualKey.Subtract || (int)e.Key == 189)
            {
                button_symbol(bminus, e);
            }
            else if (e.Key == VirtualKey.Multiply)
            {
                button_symbol(btimes, e);
            }
            else if (e.Key == VirtualKey.Divide || (int)e.Key == 191)
            {
                button_symbol(bdivided, e);
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
