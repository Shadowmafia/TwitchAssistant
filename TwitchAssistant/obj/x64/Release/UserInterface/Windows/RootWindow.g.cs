﻿#pragma checksum "..\..\..\..\..\UserInterface\Windows\RootWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E011ECF355D5B16E8DED33AE3DA671A2C7BF31CB"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using AssitantPlayer;
using Dragablz;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TwitchAssistant.UserInterface.CompositeViews;
using TwitchAssistant.UserInterface.Views;
using TwitchAssistant.UserInterface.Windows;
using TwitchBot;


namespace TwitchAssistant.UserInterface.Windows {
    
    
    /// <summary>
    /// RootWindow
    /// </summary>
    public partial class RootWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\..\..\UserInterface\Windows\RootWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TwitchAssistant.UserInterface.Windows.RootWindow RootWindowView;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\..\UserInterface\Windows\RootWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button StartMiniChatBtn;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\..\..\UserInterface\Windows\RootWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button HelperButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TwitchAssistant;component/userinterface/windows/rootwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\UserInterface\Windows\RootWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.RootWindowView = ((TwitchAssistant.UserInterface.Windows.RootWindow)(target));
            
            #line 17 "..\..\..\..\..\UserInterface\Windows\RootWindow.xaml"
            this.RootWindowView.Loaded += new System.Windows.RoutedEventHandler(this.RootWindowView_OnLoaded);
            
            #line default
            #line hidden
            
            #line 17 "..\..\..\..\..\UserInterface\Windows\RootWindow.xaml"
            this.RootWindowView.Closed += new System.EventHandler(this.RootWindowView_OnClosed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.StartMiniChatBtn = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.HelperButton = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

