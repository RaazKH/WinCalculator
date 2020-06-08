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
        }

        private void button_click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (sender as Button);
            var texr = clickedButton.Tag.ToString();

            if(textBox1.Text == "0")
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

        }
    }
}
