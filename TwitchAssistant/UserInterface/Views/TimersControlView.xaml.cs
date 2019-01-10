using System;
using System.Windows;
using System.Windows.Controls;
using TwitchAssistant.UserInterface.ViewModels;

namespace TwitchAssistant.UserInterface.Views
{
    /// <summary>
    /// Логика взаимодействия для TimerView.xaml
    /// </summary>
    public partial class TimersViewControl : UserControl
    {
        private TimersControlViewModel viewModel;
        public TimersViewControl()
        {
            viewModel= new TimersControlViewModel();
            DataContext = viewModel;
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            int id = Int32.Parse(((Button)sender).Tag.ToString());
            viewModel.DeleteTimerById(id);
        }

        private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
        {
            viewModel.SaveChange();
        }
    }
}
