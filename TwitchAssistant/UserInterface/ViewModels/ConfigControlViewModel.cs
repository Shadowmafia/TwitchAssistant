using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AssistantConfig;
using DataClasses.ConfigDataClasses;
using DataClasses.Enums;
using DataClasses.UIDataClasses;
using Tools;
using Tools.MVVMBaseClasses;
using TwitchAssistant.UserInterface.Windows;
using TwitchAuthortization;
using TwitchBot;
using TwitchLib.Api.Helix.Models.Games;
using TwitchLib.Api.V5.Models.Channels;
using TwitchLib.Client.Enums;

namespace TwitchAssistant.UserInterface.ViewModels
{
    public class ConfigControlViewModel : SingletonBaseTemplate<ConfigControlViewModel>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly System.Timers.Timer _updateChanelDataTimer;
        public ObservableCollection<BotColor> ColorsList { get; set; } = new ObservableCollection<BotColor>();
        public ObservableCollection<StreamGame> GamesList { get; set; } = new ObservableCollection<StreamGame>();

        private ConfigControlViewModel()
        {
            _updateChanelDataTimer = new System.Timers.Timer();
            _updateChanelDataTimer.Elapsed += UpdateData;
            _updateChanelDataTimer.Interval = 60000;
        }

        private void UpdateData(object sender, ElapsedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(async (c) =>
            {
                ChannelAuthed chanel = await TwitchApiController.GetChanel();
                Game[] gamesFromApi = await TwitchApiController.GetAllGames();
                App.Current.Dispatcher.Invoke(() =>
                {
                    foreach (StreamGame item in GamesList)
                    {
                        if (item.Name == chanel.Game)
                        {
                            CurrentGame = item;
                        }
                    }
                });

                StreamTitle = chanel.Status;
                StreamLink = chanel.Url;
                ChanelViews = chanel.Views;
                ChanelFollowers = chanel.Followers;
                ChanelLanguage = chanel.Language;
                ChanelUsersOnline = TwitchApiController.GetChatters().Result.Count;
            });
        }

        //Authorization config_____________________________________________________________
        public string StreamerOAuth
        {
            get => ConfigSet.Config.Auth.StreamerAuth.Tokens.AccessToken;
            set
            {
                ConfigSet.Config.Auth.StreamerAuth.Tokens.AccessToken = value;
                OnPropertyChanged(nameof(StreamerOAuth));
            }
        }
        public string BotOAuth
        {
            get => ConfigSet.Config.Auth.BotAuth.Tokens.AccessToken;
            set
            {
                ConfigSet.Config.Auth.BotAuth.Tokens.AccessToken = value;
                OnPropertyChanged(nameof(BotOAuth));
            }
        }
        public int BotPort
        {
            get => Int32.Parse(ConfigSet.Config.Port);

            set
            {
                ConfigSet.Config.Port = value.ToString();
                OnPropertyChanged(nameof(IsDualMode));
            }
        }

        //Bot config  _____________________________________________________________________
        public bool IsDualMode
        {
            get => ConfigSet.Config.BotConfig.IsDualMode;
            set
            {
                ConfigSet.Config.BotConfig.IsDualMode = value;
                OnPropertyChanged(nameof(IsDualMode));
            }
        }
        public string BotName
        {
            get => ConfigSet.Config.BotConfig.BotName;
            set
            {
                ConfigSet.Config.BotConfig.BotName = value;
                OnPropertyChanged(nameof(BotName));
            }
        }
        public BotColor BotColor
        {
            get => ConfigSet.Config.BotConfig.BotColor;
            set
            {
                ConfigSet.Config.BotConfig.BotColor = value;
                OnPropertyChanged(nameof(BotColor));
            }
        }
        public string ChanelName
        {
            get => ConfigSet.Config.BotConfig.StreamName;
            set
            {
                ConfigSet.Config.BotConfig.StreamName = value;
                OnPropertyChanged(nameof(ChanelName));
            }
        }

        //Stream settings _________________________________________________________________
        private string _streamTitle;
        public string StreamTitle
        {
            get => _streamTitle;
            set
            {
                _streamTitle = value;
                OnPropertyChanged(nameof(StreamTitle));
            }
        }

        private StreamGame _currentGame;
        public StreamGame CurrentGame
        {
            get => _currentGame;
            set
            {
                _currentGame = value;
                OnPropertyChanged(nameof(CurrentGame));
            }
        }

        //Stream info______________________________________________________________________
        private string _streamLink;
        public string StreamLink
        {
            get => _streamLink;
            set
            {
                _streamLink = value;
                OnPropertyChanged(nameof(StreamLink));
            }
        }

        private int _chanelViews;
        public int ChanelViews
        {
            get => _chanelViews;
            set
            {
                _chanelViews = value;
                OnPropertyChanged(nameof(ChanelViews));
            }
        }

        private int _chanelUsersOnline;
        public int ChanelUsersOnline
        {
            get => _chanelUsersOnline;
            set
            {
                _chanelUsersOnline = value;
                OnPropertyChanged(nameof(ChanelUsersOnline));
            }
        }

        private int _chanelFollowers;
        public int ChanelFollowers
        {
            get => _chanelFollowers;
            set
            {
                _chanelFollowers = value;
                OnPropertyChanged(nameof(ChanelFollowers));
            }
        }

        private int _chanelSubscribers;
        public int ChanelSubscribers
        {
            get => _chanelSubscribers;
            set
            {
                _chanelSubscribers = value;
                OnPropertyChanged(nameof(ChanelSubscribers));
            }
        }

        private string _chanelLanguage;
        public string ChanelLanguage
        {
            get => _chanelLanguage;
            set
            {
                _chanelLanguage = value;
                OnPropertyChanged(nameof(ChanelLanguage));
            }
        }

        //Commands _________________________________________________________________________
        private ICommand _assistantConnectCommand;
        public ICommand AssistantConnectCommand
        {
            get
            {
                return _assistantConnectCommand ?? (_assistantConnectCommand = new Command((arg) => AssistantConnect()));
            }
        }

        private ICommand _configLoadedCommand;
        public ICommand ConfigLoadedCommand
        {
            get
            {
                return _configLoadedCommand ?? (_configLoadedCommand = new Command((arg) => ConfigLoaded()));
            }
        }

        private ICommand _getStreamUriCommand;
        public ICommand GetStreamUriCommand
        {
            get
            {
                return _getStreamUriCommand ?? (_getStreamUriCommand = new Command((arg) => GetStreamUri()));
            }
        }
        private ICommand _updateGameAndTitleCommand;
        public ICommand UpdateGameAndTitleCommand
        {
            get
            {
                return _updateGameAndTitleCommand ?? (_updateGameAndTitleCommand = new Command((arg) => UpdateGameAndTitle()));
            }
        }
        private void UpdateGameAndTitle()
        {
            TwitchApiController.UpdateGameAndTitle(StreamTitle, CurrentGame.Name);
        }

        private ICommand _botAuthCommand;
        public ICommand BotAuthCommand
        {
            get
            {
                return _botAuthCommand ?? (_botAuthCommand = new Command((arg) => BotAuthorization()));
            }
        }
        private ICommand _streamerAuthCommand;
        public ICommand StreamerAuthCommand
        {
            get
            {
                return _streamerAuthCommand ?? (_streamerAuthCommand = new Command((arg) => StreamerAuthorization()));
            }
        }


        //Authorization
        public void BotAuthorization()
        {
            if (GlobalObjects.Authorizator == null)
            {
                GlobalObjects.Authorizator = new Authorizator(AppInteractionConfig.TwitchClientId, AppInteractionConfig.TwitchClientSecret, AppInteractionConfig.OAuthRedirectUrl);
                AuthBrowserWindow authBrowserWindow = new AuthBrowserWindow(false);
                GlobalObjects.Authorizator.InitHttpServerAndWaitCallBack((twitchAuthResponse) =>
                {
                    BotOAuth = twitchAuthResponse.AccessToken;
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        authBrowserWindow.Close();
                    });
                });
                authBrowserWindow.ShowDialog();
                GlobalObjects.Authorizator = null;
            }
        }
        public void StreamerAuthorization()
        {
            if (GlobalObjects.Authorizator == null)
            {
                GlobalObjects.Authorizator = new Authorizator(AppInteractionConfig.TwitchClientId, AppInteractionConfig.TwitchClientSecret, AppInteractionConfig.OAuthRedirectUrl);
                AuthBrowserWindow authBrowserWindow = new AuthBrowserWindow(true);
                GlobalObjects.Authorizator.InitHttpServerAndWaitCallBack((twitchAuthResponse) =>
                {
                    StreamerOAuth = twitchAuthResponse.AccessToken;
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        authBrowserWindow.Close();
                    });
                });
                authBrowserWindow.ShowDialog();
                GlobalObjects.Authorizator = null;
            }
        }
        //Commands methods___________________________________________________________________
        private void AssistantConnect()
        {
            if (TwitchBotGlobalObjects.TwitchBotConnectedState != TwitchBotConnectedState.WaitConnect)
            {
                if (TwitchBotGlobalObjects.Bot == null || TwitchBotGlobalObjects.TwitchBotConnectedState == TwitchBotConnectedState.Disconnected)
                {
                    TwitchBotGlobalObjects.TwitchBotConnectedState = TwitchBotConnectedState.WaitConnect;
                    ThreadPool.QueueUserWorkItem((o) =>
                    {
                        bool validTokenCheck = false;
                        if (ConfigSet.Config.BotConfig.IsDualMode)
                        {
                            validTokenCheck = ValidateDualModeTokens();
                        }
                        else
                        {
                            string response = TwitchApiController.CheckAccessToken(ConfigSet.Config.Auth.StreamerAuth.Tokens.AccessToken);
                            if (response != null)
                            {
                                validTokenCheck = true;
                            }
                            else
                            {
                                App.Current.Dispatcher.Invoke(StreamerAuthorization);
                                response = TwitchApiController.CheckAccessToken(ConfigSet.Config.Auth.StreamerAuth.Tokens.AccessToken);
                                if (response != null)
                                {
                                    validTokenCheck = true;
                                }
                                else
                                {
                                    MessageBox.Show("Invalid stream authorizothion or permission!");
                                }
                            }
                        }

                        if (validTokenCheck)
                        {
                            TwitchApiController.AuthApiController(AppInteractionConfig.TwitchClientId);
                            TwitchBotGlobalObjects.Bot = new Bot();
                            TwitchBotGlobalObjects.StreamUpDownChecker = new PubSub();
                            LoadChanelData();
                            _updateChanelDataTimer.Start();
                        }
                        else
                        {
                            MessageBox.Show("Authorization error");
                            TwitchBotGlobalObjects.TwitchBotConnectedState = TwitchBotConnectedState.Disconnected;
                        }
                    });
                }
                else
                {
                    _updateChanelDataTimer.Stop();
                    TwitchBotGlobalObjects.Bot.Disconnected();
                    TwitchBotGlobalObjects.Bot = null;
                    // TwitchBotGlobalObjects.StreamUpDownChecker.Disconnect();
                    // TwitchBotGlobalObjects.StreamUpDownChecker = null;
                }
            }
            else
            {
                MessageBox.Show("Wait!!");
            }
        }

        private void ConfigLoaded()
        {
            FillBotColorListAndLanguageList();
            int x = 0;
            foreach (BotColor item in ColorsList)
            {
                if (item.Color == ConfigSet.Config.BotConfig.BotColor.Color)
                {
                    BotColor = ColorsList[x];
                }
                x++;
            }
        }

        private void GetStreamUri()
        {
            Clipboard.SetText(StreamLink);
        }

        private bool ValidateDualModeTokens()
        {
            bool validTokenCheck = false;
            string response = TwitchApiController.CheckAccessToken(ConfigSet.Config.Auth.StreamerAuth.Tokens.AccessToken);
            if (response != null)
            {
                string response2 = TwitchApiController.CheckAccessToken(ConfigSet.Config.Auth.BotAuth.Tokens.AccessToken);
                if (response2 != null)
                {
                    validTokenCheck = true;
                }
                else
                {
                    MessageBox.Show("invalid Bot oAuth");
                }
            }
            else
            {
                MessageBox.Show("invalid Streamer oAuth");
            }
            return validTokenCheck;
        }
        //Other methods______________________________________________________________________

        private void LoadChanelData()
        {
            ThreadPool.QueueUserWorkItem(async (c) =>
            {
                ChannelAuthed chanel = await TwitchApiController.GetChanel();
                Game[] gamesFromApi = await TwitchApiController.GetAllGames();

                App.Current.Dispatcher.Invoke(() =>
                {
                    foreach (Game game in gamesFromApi)
                    {
                        GamesList.Add(new StreamGame() { Name = game.Name, Image = new BitmapImage(new Uri(game.BoxArtUrl.Replace("{width}x{height}", "48x65"))) });
                    }

                });

                foreach (StreamGame item in GamesList)
                {
                    if (item.Name == chanel.Game)
                    {
                        CurrentGame = item;
                    }
                }
                ChanelName = ConfigSet.Config.BotConfig.StreamName;
                StreamTitle = chanel.Status;
                StreamLink = chanel.Url;
                ChanelViews = chanel.Views;
                ChanelFollowers = chanel.Followers;
                ChanelLanguage = chanel.Language;
                ChanelUsersOnline = TwitchApiController.GetChatters().Result.Count;
            });
        }
        private void FillBotColorListAndLanguageList()
        {
            ColorsList.Add(new BotColor() { RbgColor = new SolidColorBrush(Color.FromRgb(255, 0, 0)), Color = ChatColorPresets.Red });
            ColorsList.Add(new BotColor() { RbgColor = new SolidColorBrush(Color.FromRgb(255, 69, 0)), Color = ChatColorPresets.OrangeRed });
            ColorsList.Add(new BotColor() { RbgColor = new SolidColorBrush(Color.FromRgb(255, 105, 180)), Color = ChatColorPresets.HotPink });
            ColorsList.Add(new BotColor() { RbgColor = new SolidColorBrush(Color.FromRgb(218, 165, 32)), Color = ChatColorPresets.GoldenRod });
            ColorsList.Add(new BotColor() { RbgColor = new SolidColorBrush(Color.FromRgb(178, 34, 34)), Color = ChatColorPresets.Firebrick });
            ColorsList.Add(new BotColor() { RbgColor = new SolidColorBrush(Color.FromRgb(44, 44, 252)), Color = ChatColorPresets.Blue });
            ColorsList.Add(new BotColor() { RbgColor = new SolidColorBrush(Color.FromRgb(108, 165, 168)), Color = ChatColorPresets.CadetBlue });
            ColorsList.Add(new BotColor() { RbgColor = new SolidColorBrush(Color.FromRgb(146, 59, 227)), Color = ChatColorPresets.BlueViolet });
            ColorsList.Add(new BotColor() { RbgColor = new SolidColorBrush(Color.FromRgb(39, 144, 255)), Color = ChatColorPresets.DodgerBlue });
            ColorsList.Add(new BotColor() { RbgColor = new SolidColorBrush(Color.FromRgb(255, 127, 80)), Color = ChatColorPresets.Coral });
            ColorsList.Add(new BotColor() { RbgColor = new SolidColorBrush(Color.FromRgb(8, 254, 113)), Color = ChatColorPresets.SeaGreen });
            ColorsList.Add(new BotColor() { RbgColor = new SolidColorBrush(Color.FromRgb(19, 137, 19)), Color = ChatColorPresets.Green });
            ColorsList.Add(new BotColor() { RbgColor = new SolidColorBrush(Color.FromRgb(157, 206, 57)), Color = ChatColorPresets.YellowGreen });
            ColorsList.Add(new BotColor() { RbgColor = new SolidColorBrush(Color.FromRgb(57, 145, 96)), Color = ChatColorPresets.SeaGreen });
            ColorsList.Add(new BotColor() { RbgColor = new SolidColorBrush(Color.FromRgb(210, 105, 30)), Color = ChatColorPresets.Chocolate });
        }

    }
}
