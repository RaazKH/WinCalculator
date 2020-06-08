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

            //?
            this.Loaded += delegate { this.Focus(FocusState.Programmatic); };
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
            if(e.Key == Windows.System.VirtualKey.B)
            {
                button_click(b1, e);
            }
        }
    }
}
