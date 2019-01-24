using DateBaseController;
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
using TwitchAssistant.UserInterface.ViewModels;

namespace TwitchAssistant.UserInterface.Windows
{
    /// <summary>
    /// Interaction logic for EditUserBaseWindow.xaml
    /// </summary>
    public partial class EditUserBaseWindow : Window
    {
        private int _deleteTarget;
        public EditUserBaseWindow()
        {
            InitializeComponent();
        }
       
        private void DeleteAllCoins_OnClick(object sender, RoutedEventArgs e)
        {
            _deleteTarget = 1;
            DeleteTargetLabel.Text = "You want delete all coins";
            ConfimActionModal.IsOpen = true;
        }

        private void DeleteAllWatchngTimes_OnClick(object sender, RoutedEventArgs e)
        {
            _deleteTarget = 2;
            DeleteTargetLabel.Text = "You want delete all wathing times";
            ConfimActionModal.IsOpen = true;
        }

        private void DeleteAllUsers_OnClick(object sender, RoutedEventArgs e)
        {
            _deleteTarget = 3;
            DeleteTargetLabel.Text = "You want delete all Users";
            ConfimActionModal.IsOpen = true;
        }

        private void DeleteTarget_OnClick(object sender, RoutedEventArgs e)
        {
            switch (_deleteTarget)
            {
                case 1: DeleteAllCoins();
                    break;
                case 2: DeleteAllWathingTimes();
                    break;
                case 3: DeleteAllUsers();
                    break;        
            }
            ConfimActionModal.IsOpen = false;
        }

        private void DeleteAllCoins()
        {
            foreach (var user in AssistantDb.Instance.Viewers.GetAll())
            {
                user.Coins = 0;
            }
            AssistantDb.Instance.SaveChanges();
            UsersGridViewModel.Instance.RefreshUserList();
        }

        private void DeleteAllWathingTimes()
        {
            foreach (var user in AssistantDb.Instance.Viewers.GetAll())
            {
                user.WatchingTime = new DateTime();
            }
            AssistantDb.Instance.SaveChanges();
            UsersGridViewModel.Instance.RefreshUserList();
        }

        private void DeleteAllUsers()
        {
            AssistantDb.Instance.Viewers.DeletAll();
            AssistantDb.Instance.SaveChanges();
            UsersGridViewModel.Instance.RefreshUserList();
        }

    }
}
