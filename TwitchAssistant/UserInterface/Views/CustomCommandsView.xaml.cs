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
using TwitchBot.CommandsSystem.Commands;
using TwitchBot.CommandsSystem;

namespace TwitchAssistant.UserInterface.Views
{
    /// <summary>
    /// Логика взаимодействия для CustomCommandsView.xaml
    /// </summary>
    public partial class CustomCommandsView : UserControl
    {
        CustomCommandsViewModel viewModel;
        public CustomCommandsView()
        {
            viewModel = new CustomCommandsViewModel();
            this.DataContext = viewModel;
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            int id = Int32.Parse(((Button)sender).Tag.ToString());
            viewModel.DeleteCommandById(id);
        }

    
        private void DetailSettingsBtn_OnClick(object sender, RoutedEventArgs e)
        {           
            int id = Int32.Parse(((Button)sender).Tag.ToString());        
            DetailsCommandSettingsWindow settingsWindow = new DetailsCommandSettingsWindow();
            CustomBotCommand xCommand = CommandsController.CustomCommandsList.FirstOrDefault(command => command.Id == id);
            settingsWindow.DataContext = new DetailsCommandSettingsViewModel<CustomBotCommand>(xCommand);
            settingsWindow.ShowDialog();
        }
    }
}
