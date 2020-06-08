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

namespace Calculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            // worthless
            //this.Loaded += delegate { this.Focus(FocusState.Programmatic); };

            // initializes keyboard input on app startup
            // without this a button will need to be hit on the screen before keyboard works
            this.Loaded += delegate { this.Focus(FocusState.Keyboard); };
        }

        private void button_click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (sender as Button);
            var texr = clickedButton.Tag.ToString();

            if(textBox1.Text == "0" && texr != ".")
            {
                textBox1.Text = texr;
            }
            else
            {
                textBox1.Text = textBox1.Text + texr;
            }
        }

        private void button_special(object sender, RoutedEventArgs e)
        {
            string texr = (sender as Button).Tag.ToString();
            if(texr == "clear")
            {
                textBox1.Text = "0";
            }
            else if(texr == "back" && textBox1.Text.Length > 1)
            {
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
            }
            else if (texr == "back" && textBox1.Text.Length == 1)
            {
                textBox1.Text = "0";
            }
        }

        private void button_evaluate (object sender, RoutedEventArgs e)
        {
            string eval = textBox1.Text;
            int len = eval.Length;
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

            for (int i = 0; i < len; i++)
            {
                int a = (int)eval[i];
                if((a >= 40 && a < 44) || (a > 44 && a <= 57))
                {
                    // valid
                }
                else
                {
                    Debug.WriteLine("Invalid character:" + a);
                    return;
                }
                
            }
        }

        private void Grid_KeyPressed(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key == VirtualKey.Number1 || e.Key == VirtualKey.NumberPad1)
            {
                button_click(b1, e);
            }
            else if(e.Key == VirtualKey.Number2 || e.Key == VirtualKey.NumberPad2)
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


        }
    }
}
