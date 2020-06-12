﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppCustomWindow : Page
    {
        public AppCustomWindow()
        {
            this.InitializeComponent();

            // Hide titlebar panel and add new layout to title bar
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(UserLayout);

            // Add LayoutMetricsChanged Event to TitleBar
            var tBar = CoreApplication.GetCurrentView().TitleBar;

            // Navigate
            Content.Navigate(typeof(MainPage), null, new SuppressNavigationTransitionInfo()); // Navigate to Home page with null args and null animation
        }
    }
}
