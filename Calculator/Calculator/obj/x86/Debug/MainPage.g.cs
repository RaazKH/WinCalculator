﻿#pragma checksum "C:\Users\serxt\Desktop\WinCalculator\Calculator\Calculator\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A9A19736C97F1DB3D9180A45BC930D41"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Calculator
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // MainPage.xaml line 1
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)(target);
                    ((global::Windows.UI.Xaml.Controls.Page)element1).KeyUp += this.Grid_KeyPressed;
                }
                break;
            case 2: // MainPage.xaml line 14
                {
                    global::Windows.UI.Xaml.Controls.Grid element2 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                    ((global::Windows.UI.Xaml.Controls.Grid)element2).KeyDown += this.Grid_KeyPressed;
                }
                break;
            case 3: // MainPage.xaml line 31
                {
                    this.textBox1 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 4: // MainPage.xaml line 41
                {
                    this.bequals = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.bequals).Click += this.Button_Evaluate;
                }
                break;
            case 5: // MainPage.xaml line 56
                {
                    this.b0 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.b0).Click += this.Button_Click;
                }
                break;
            case 6: // MainPage.xaml line 72
                {
                    this.b1 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.b1).Click += this.Button_Click;
                }
                break;
            case 7: // MainPage.xaml line 88
                {
                    this.b2 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.b2).Click += this.Button_Click;
                }
                break;
            case 8: // MainPage.xaml line 104
                {
                    this.b3 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.b3).Click += this.Button_Click;
                }
                break;
            case 9: // MainPage.xaml line 120
                {
                    this.b4 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.b4).Click += this.Button_Click;
                }
                break;
            case 10: // MainPage.xaml line 136
                {
                    this.b5 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.b5).Click += this.Button_Click;
                }
                break;
            case 11: // MainPage.xaml line 152
                {
                    this.b6 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.b6).Click += this.Button_Click;
                }
                break;
            case 12: // MainPage.xaml line 168
                {
                    this.b7 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.b7).Click += this.Button_Click;
                }
                break;
            case 13: // MainPage.xaml line 184
                {
                    this.b8 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.b8).Click += this.Button_Click;
                }
                break;
            case 14: // MainPage.xaml line 200
                {
                    this.b9 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.b9).Click += this.Button_Click;
                }
                break;
            case 15: // MainPage.xaml line 216
                {
                    this.bclear = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.bclear).Click += this.Button_Special;
                }
                break;
            case 16: // MainPage.xaml line 231
                {
                    this.bdecimal = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.bdecimal).Click += this.Button_Click;
                }
                break;
            case 17: // MainPage.xaml line 247
                {
                    this.bopenP = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.bopenP).Click += this.Button_Symbol;
                }
                break;
            case 18: // MainPage.xaml line 263
                {
                    this.bcloseP = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.bcloseP).Click += this.Button_Symbol;
                }
                break;
            case 19: // MainPage.xaml line 279
                {
                    this.bbackspace = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.bbackspace).Click += this.Button_Special;
                }
                break;
            case 20: // MainPage.xaml line 294
                {
                    this.bplus = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.bplus).Click += this.Button_Symbol;
                }
                break;
            case 21: // MainPage.xaml line 310
                {
                    this.bminus = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.bminus).Click += this.Button_Symbol;
                }
                break;
            case 22: // MainPage.xaml line 326
                {
                    this.btimes = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btimes).Click += this.Button_Symbol;
                }
                break;
            case 23: // MainPage.xaml line 342
                {
                    this.bdivided = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.bdivided).Click += this.Button_Symbol;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

