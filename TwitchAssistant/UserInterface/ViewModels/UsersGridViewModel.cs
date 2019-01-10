using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using DateBaseController;
using DateBaseController.Models;
using Tools.MVVMBaseClasses;
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
            foreach (var user in DbRepository.Instance.GetViewers())
            {
                Users.Add(user);
            }

            TotalUsers = DbRepository.Instance.GetViewers().Count;
            FilterUsers();
        }


        private static bool _refreshCoinsDialog;
        public bool RefreshCoinsDialog
        {
            get { return _refreshCoinsDialog; }
            set
            {
                _refreshCoinsDialog = value;
                OnPropertyChanged(nameof(RefreshCoinsDialog));
            }
        }
        private ICommand _openRefreshDialogCommand;
        public ICommand OpenRefreshDialogCommand
        {
            get
            {
                return _openRefreshDialogCommand ?? (_openRefreshDialogCommand = new Command((arg) => ShowHideRefreshDialog()));
            }
        }
        private void ShowHideRefreshDialog()
        {
            RefreshCoinsDialog = !RefreshCoinsDialog;
        }


        static UsersGridViewModel()
        {
            _refreshCoinsDialog = false;
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
            foreach (var user in DbRepository.Instance.GetViewers())
            {
                user.Coins = 0;
            }

            DbRepository.Instance.SaveChanges();
            RefreshUserList();
            ShowHideRefreshDialog();
            // MessageBox.Show("Implement method!!!");
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

            List<Viewer> Subscribers = DbRepository.Instance.GetViewers();
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
