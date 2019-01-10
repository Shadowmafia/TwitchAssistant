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
using System.Windows.Shapes;
using AssistantConfig;
using AssitantPlayer;
using TwitchBot.CommandsSystem;

namespace TwitchAssistant.UserInterface.Windows
{
    /// <summary>
    /// Логика взаимодействия для RootWindow.xaml
    /// </summary>
    public partial class RootWindow : Window
    {
        public RootWindow()
        {
            InitializeComponent();
        }

        private void RootWindowView_OnLoaded(object sender, RoutedEventArgs e)
        {
            MyPlayer.Instance.PlayerLoaded();
        }

        private void RootWindowView_OnClosed(object sender, EventArgs e)
        {
            MyPlayer.Instance.PlayerClosed();
            DefaultCommandsSet.SaveCommands();
            ConfigSet.SaveConfig();
        }
    }
}
