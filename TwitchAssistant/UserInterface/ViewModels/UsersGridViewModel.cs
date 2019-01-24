using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using DateBaseController;
using DateBaseController.Models;
using Tools;
using Tools.MVVMBaseClasses;
using TwitchAssistant.UserInterface.Windows;
using TwitchBot;

namespace TwitchAssistant.UserInterface.ViewModels
{
    public class UsersGridViewModel : SingletonBaseTemplate<UsersGridViewModel>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private UsersGridViewModel()
        {
            Users = new List<Viewer>();
            RefreshUserList();
        }

        public List<Viewer> Users { get; set; }
   
        private ICommand _refreshUsersCommand;
        public ICommand RefreshUsersCommand
        {
            get
            {
                return _refreshUsersCommand ?? (_refreshUsersCommand = new Command((arg) => RefreshUserList()));
            }
        }
        public void RefreshUserList()
        {

            Users.Clear();
            int i = 0;
            //UsersRepository usersRepository = new UsersRepository();
            foreach (var user in AssistantDb.Instance.Viewers.GetAll())
            {
                Users.Add(user);
                i++;
            }         
            FilterUsers();
            UsersCount = i.ToString();
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


        private string _usersCount;
        public string UsersCount
        {
            get { return _usersCount; }
            set
            {
                _usersCount = value;
                OnPropertyChanged(nameof(UsersCount));
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
                    filter &= Subscribers.Any(user => user.Id == u.Id && user.IsSubscriber == true);
                }
                if (IsOnlyModerator)
                {
                    filter &= Subscribers.Any(user => user.Id == u.Id && user.IsModerator == true);
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
