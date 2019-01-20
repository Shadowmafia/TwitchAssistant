﻿#pragma checksum "..\..\..\PlayerControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D6468D3267199666A5D26D014E65D0A062DD276C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using GongSolutions.Wpf.DragDrop;
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
using YoutubePlayerLib.Cef;


namespace AssitantPlayer {
    
    
    /// <summary>
    /// PlayerControl
    /// </summary>
    public partial class PlayerControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 314 "..\..\..\PlayerControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Anterior;
        
        #line default
        #line hidden
        
        
        #line 335 "..\..\..\PlayerControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal YoutubePlayerLib.Cef.CefYoutubeController Player;
        
        #line default
        #line hidden
        
        
        #line 341 "..\..\..\PlayerControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.DialogHost AddSongsDialog;
        
        #line default
        #line hidden
        
        
        #line 351 "..\..\..\PlayerControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SongLingOrId;
        
        #line default
        #line hidden
        
        
        #line 358 "..\..\..\PlayerControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.DialogHost ClearStreamerPlaylistDialog;
        
        #line default
        #line hidden
        
        
        #line 386 "..\..\..\PlayerControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.DialogHost ClearChatPlayListDialog;
        
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
            System.Uri resourceLocater = new System.Uri("/AssitantPlayer;component/playercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\PlayerControl.xaml"
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
            case 11:
            
            #line 250 "..\..\..\PlayerControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowClearStreamerPlaylistDialog);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 253 "..\..\..\PlayerControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowAddSongDialog);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 280 "..\..\..\PlayerControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowClearChatPlaylistDialog);
            
            #line default
            #line hidden
            return;
            case 14:
            this.Anterior = ((System.Windows.Controls.Button)(target));
            return;
            case 15:
            this.Player = ((YoutubePlayerLib.Cef.CefYoutubeController)(target));
            return;
            case 16:
            this.AddSongsDialog = ((MaterialDesignThemes.Wpf.DialogHost)(target));
            return;
            case 17:
            this.SongLingOrId = ((System.Windows.Controls.TextBox)(target));
            return;
            case 18:
            
            #line 352 "..\..\..\PlayerControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddNewSong);
            
            #line default
            #line hidden
            return;
            case 19:
            this.ClearStreamerPlaylistDialog = ((MaterialDesignThemes.Wpf.DialogHost)(target));
            return;
            case 20:
            
            #line 369 "..\..\..\PlayerControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ClearStreamerPlaylist);
            
            #line default
            #line hidden
            return;
            case 21:
            this.ClearChatPlayListDialog = ((MaterialDesignThemes.Wpf.DialogHost)(target));
            return;
            case 22:
            
            #line 397 "..\..\..\PlayerControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ClearChatPlaylist);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 1:
            
            #line 29 "..\..\..\PlayerControl.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.PlayStreamerSongDocPanel);
            
            #line default
            #line hidden
            break;
            case 2:
            
            #line 38 "..\..\..\PlayerControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PlayStreamerSong);
            
            #line default
            #line hidden
            break;
            case 3:
            
            #line 93 "..\..\..\PlayerControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteSongStreamerPlaylist);
            
            #line default
            #line hidden
            break;
            case 4:
            
            #line 97 "..\..\..\PlayerControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CopySongLinkStreamerPlaylist);
            
            #line default
            #line hidden
            break;
            case 5:
            
            #line 106 "..\..\..\PlayerControl.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.PlayChatSongDocPanel);
            
            #line default
            #line hidden
            break;
            case 6:
            
            #line 115 "..\..\..\PlayerControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PlayChatSong);
            
            #line default
            #line hidden
            break;
            case 7:
            
            #line 173 "..\..\..\PlayerControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddToStreamerPlaylist);
            
            #line default
            #line hidden
            break;
            case 8:
            
            #line 178 "..\..\..\PlayerControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenContextMenu);
            
            #line default
            #line hidden
            break;
            case 9:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.Controls.MenuItem.ClickEvent;
            
            #line 184 "..\..\..\PlayerControl.xaml"
            eventSetter.Handler = new System.Windows.RoutedEventHandler(this.DeleteSongChatPlaylist);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            case 10:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.Controls.MenuItem.ClickEvent;
            
            #line 199 "..\..\..\PlayerControl.xaml"
            eventSetter.Handler = new System.Windows.RoutedEventHandler(this.CopySongLinkChatPlaylist);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            }
        }
    }
}

