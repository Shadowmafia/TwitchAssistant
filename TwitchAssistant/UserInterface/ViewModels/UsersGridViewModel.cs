using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using DateBaseController;
using DateBaseController.Models;
using Tools.MVVMBaseClasses;
using TwitchAssistant.UserInterface.Windows;
using TwitchBot;

namespace TwitchAssistant.UserInterface.ViewModels
{
    public class UsersGridViewModel : ViewModelBase
    {
        public List<Viewer> Users { get; set; }
        public UsersGridViewModel()
        {
            Users = new List<Viewer>();

            RefreshUserList();

            // IsOnlyFolowers = IsOnlyOnline = true;
        }

        private ICommand _refreshUsersCommand;
        public ICommand RefreshUsersCommand
        {
            get
            {
                return _refreshUsersCommand ?? (_refreshUsersCommand = new Command((arg) => RefreshUserList()));
            }
        }
        private void RefreshUserList()
        {

            Users.Clear();
            //UsersRepository usersRepository = new UsersRepository();
            foreach (var user in AssistantDb.Instance.Viewers.GetAll())
            {
                Users.Add(user);
            }

            TotalUsers = AssistantDb.Instance.Viewers.GetAll().ToList().Count;
            FilterUsers();
        }


        private ICommand _showEditUserBaseWindow;
        public ICommand ShowEditUserBaseWindow
        {
            get
            {
                return _showEditUserBaseWindow ?? (_showEditUserBaseWindow = new Command((arg) => ShowEditWindow()));
            }
        }

        private void ShowEditWindow()
        {
            EditUserBaseWindow tmpWindow = new EditUserBaseWindow();
            tmpWindow.ShowDialog();
        }


    


        private ICommand _refreshCoinsCommand;
        public ICommand RefreshCoinsCommand
        {
            get
            {
                return _refreshCoinsCommand ?? (_refreshCoinsCommand = new Command((arg) => RefreshCoins()));
            }
        }
        private void RefreshCoins()
        {
            foreach (var user in AssistantDb.Instance.Viewers.GetAll())
            {
                user.Coins = 0;
            }

            AssistantDb.Instance.SaveChanges();
            RefreshUserList();
        }


        private bool _isOnlySubscriber;
        public bool IsOnlySubscriber
        {
            get { return _isOnlySubscriber; }
            set
            {
                _isOnlySubscriber = value;
                OnPropertyChanged(nameof(IsOnlyFollowers));
                FilterUsers();
            }
        }

        private bool _isOnlyModerator;
        public bool IsOnlyModerator
        {
            get { return _isOnlyModerator; }
            set
            {
                _isOnlyModerator = value;
                OnPropertyChanged(nameof(IsOnlyModerator));
                FilterUsers();
            }
        }

        private bool _isOnlyFollows;
        public bool IsOnlyFollowers
        {
            get { return _isOnlyFollows; }
            set
            {
                _isOnlyFollows = value;
                OnPropertyChanged(nameof(IsOnlyFollowers));
                FilterUsers();
            }
        }

        private bool _isOnlyOnline;
        public bool IsOnlyOnline
        {
            get { return _isOnlyOnline; }
            set
            {
                _isOnlyOnline = value;
                OnPropertyChanged(nameof(IsOnlyOnline));
                FilterUsers();
            }
        }

        private int _totalUsers;
        public int TotalUsers
        {
            get { return _totalUsers; }
            set
            {
                _totalUsers = value;
                OnPropertyChanged(nameof(TotalUsers));
                FilterUsers();
            }
        }

        private string _nameFilter;
        public string NameFilter
        {
            get { return _nameFilter; }
            set
            {
                _nameFilter = value;
                OnPropertyChanged(nameof(NameFilter));
                FilterUsers();
            }
        }


        private void FilterUsers()
        {
            ICollectionView cv = CollectionViewSource.GetDefaultView(Users);

            List<Viewer> Subscribers = AssistantDb.Instance.Viewers.GetAll().ToList();
            cv.Filter = o =>
            {
                Viewer u = o as Viewer;

                bool filter = true;
                if (IsOnlyFollowers)
                {
                    filter &= TwitchBotGlobalObjects.ChanelData.Followers.Any(follow => follow.User.Id == u.Id.ToString());
                }
                if (IsOnlySubscriber)
                {
                    filter &= Subscribers.Any(user => user.IsSubscriber == true);
                }
                if (IsOnlyModerator)
                {
                    filter &= Subscribers.Any(user => user.IsModerator == true);
                }
                if (IsOnlyOnline)
                {
                    filter &= TwitchBotGlobalObjects.ChanelData.Watchers.Any(watcher => watcher.Id == u.Id);
                }
                if (!String.IsNullOrWhiteSpace(NameFilter))
                {
                    filter &= u.Username.ToLower().Contains(NameFilter.ToLower());
                }

                return filter;
            };
        }

    }

}
