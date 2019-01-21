using System;
using System.Linq;
using System.Windows.Controls;
using TwitchAssistant.UserInterface.ViewModels;
using TwitchAssistant.UserInterface.Windows;
using TwitchBot.CommandsSystem;
using TwitchBot.CommandsSystem.Commands;

namespace TwitchAssistant.UserInterface.Views
{
    /// <summary>
    /// Логика взаимодействия для DefailtCommandsView.xaml
    /// </summary>
    public partial class DefaultCommandsView : UserControl
    {
        public DefaultCommandsView()
        {
            DataContext = new DefaultCommandsViewModel();
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            int id = Int32.Parse(((Button)sender).Tag.ToString());              
            DetailsCommandSettingsWindow settingsWindow = new DetailsCommandSettingsWindow();
            DefaultBotCommand xCommand = CommandsController.DefaultCommandsList.FirstOrDefault(command => command.Id == id);
            settingsWindow.DataContext = new DetailsCommandSettingsViewModel<DefaultBotCommand>(xCommand);
            settingsWindow.ShowDialog();
            //viewModel.DeleteTimerById(id);
        }
    }
}
