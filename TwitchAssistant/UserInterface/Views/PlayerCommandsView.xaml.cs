using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TwitchAssistant.UserInterface.ViewModels;
using TwitchAssistant.UserInterface.Windows;
using TwitchBot.CommandsSystem;
using TwitchBot.CommandsSystem.Commands;

namespace TwitchAssistant.UserInterface.Views
{
    /// <summary>
    /// Логика взаимодействия для PlayerCommandsView.xaml
    /// </summary>
    public partial class PlayerCommandsView : UserControl
    {
        public PlayerCommandsView()
        {
            DataContext = new PlayerCommandsViewModel();
            InitializeComponent();
        }
        private void ButtonBase_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            int id = Int32.Parse(((Button)sender).Tag.ToString());
            DetailsCommandSettingsWindow settingsWindow = new DetailsCommandSettingsWindow();
            PlayerBotCommand xCommand = CommandsController.PlayerCommandsList.FirstOrDefault(command => command.Id == id);
            settingsWindow.DataContext = new DetailsCommandSettingsViewModel<PlayerBotCommand>(xCommand);
            settingsWindow.ShowDialog();

        }
    }
}
