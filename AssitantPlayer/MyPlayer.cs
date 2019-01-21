using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using AssistantConfig;
using AssitantPlayer.SettingsWindow;
using Microsoft.Win32;
using Newtonsoft.Json;
using Tools;
using Tools.MVVMBaseClasses;
using YoutubePlayerLib;

namespace AssitantPlayer
{
    public class MyPlayer : SingletonBaseTemplate<MyPlayer>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       
        public event EventHandler<Song> OnSongChanged;
        protected virtual void OnOnSongChanged(Song e)
        {
            OnSongChanged?.Invoke(this, e);
        }
        private PlayerControl _player;


        public ObservableCollection<Song> StreamerPlaylist { get; set; } = new ObservableCollection<Song>();
        private Song _lastStreamerSong;
        public Song LastStreamerSong
        {
            get { return _lastStreamerSong; }
            set
            {
                _lastStreamerSong = value;
                OnPropertyChanged(nameof(LastStreamerSong));
            }
        }

        public ObservableCollection<Song> ChatPlayList { get; set; } = new ObservableCollection<Song>();
        private Song _lastChatSong;
        public Song LastChatSong
        {
            get { return _lastChatSong; }
            set
            {
                _lastChatSong = value;
                OnPropertyChanged(nameof(LastChatSong));
            }
        }

        private Song _currentSong;
        public Song CurrentSong
        {
            get { return _currentSong; }
            set
            {
                _currentSong = value;
                OnPropertyChanged(nameof(CurrentSong));
                OnOnSongChanged(value);
            }
        }

        public bool Autoplay
        {
            get { return ConfigSet.Config.PlayerConfig.Autoplay; }
            set
            {
                ConfigSet.Config.PlayerConfig.Autoplay = value;
                OnPropertyChanged(nameof(Autoplay));
            }
        }

        private MyPlayer()
        {
       
            StreamerPlaylist.CollectionChanged += StreamerReindexSongs;
            ChatPlayList.CollectionChanged += ChatReindexSongs;
            //GlobalObjects.Player = this;
            // CurrentSong = CreateSongById("lEHM9HZf0IA");

            ////Streamer playlist
            //CurrentSong.IsSelected = true;
            //LastStreamerSong = CurrentSong;
            //StreamerPlaylist.Add(CurrentSong);
            //StreamerPlaylist.Add(CreateSongById("Qz6AOcQp7WI"));
            //StreamerPlaylist.Add(CreateSongById("gU-VTxg2pxw"));
            //StreamerPlaylist.Add(CreateSongById("QglaLzo_aPk"));
            //StreamerPlaylist.Add(CreateSongById("XEymPGxopmg"));
            //StreamerPlaylist.Add(CreateSongById("bM7SZ5SBzyY"));
            //StreamerPlaylist.Add(CreateSongById("K4DyBUG242c"));
            //StreamerPlaylist.Add(CreateSongById("AOeY-nDp7hI"));
            //StreamerPlaylist.Add(CreateSongById("J2X5mJ3HDYE"));
            //StreamerPlaylist.Add(CreateSongById("ZRnrjxDKv6Q"));


            ////Chat playlist 
            //ChatPlayList.CollectionChanged += ChatReindexSongs;
            //LastStreamerSong = CreateSongById("AOeY-nDp7hI");
            //ChatPlayList.Add(LastStreamerSong);
            //ChatPlayList.Add(CreateSongById("J2X5mJ3HDYE"));
            //ChatPlayList.Add(CreateSongById("ZRnrjxDKv6Q"));
        }

        public void SetPlayerControl(PlayerControl player)
        {        
            _player = player;
            _player.Player.bound.PlayerPlayingChanged += EndSongCheck;
        }
        private ICommand _playerLoadedCommand;
        public ICommand PlayerLoadedCommand
        {
            get
            {
                return _playerLoadedCommand ?? (_playerLoadedCommand = new Command((arg) => PlayerLoaded()));
            }
        }
        public void PlayerLoaded()
        {

            string stringConfig = "";
            if (!File.Exists(ConfigSet.PlayerSaveFilePath))
            {
                PlayerClosed();
            }
            using (BinaryReader reader = new BinaryReader(File.Open(ConfigSet.PlayerSaveFilePath, FileMode.OpenOrCreate)))
            {
                stringConfig = reader.ReadString();
            }
            PlayerSaveObject tmp = JsonConvert.DeserializeObject<PlayerSaveObject>(stringConfig);


            foreach (var item in tmp.StreamerPlayList)
            {
                StreamerPlaylist.Add(item);
            }
            if (StreamerPlaylist.Count != 0)
            {
                StreamerPlaylist[tmp.LastStreamerSong.Index - 1].IsSelected = true;
                LastStreamerSong = StreamerPlaylist[tmp.LastStreamerSong.Index - 1];
            }

            CurrentSong = null;
            foreach (var item in tmp.ChatPlayList)
            {
                ChatPlayList.Add(item);
            }
            if (ChatPlayList.Count != 0)
            {
                ChatPlayList[tmp.LastChatSong.Index - 1].IsSelected = true;
                LastChatSong = ChatPlayList[tmp.LastChatSong.Index - 1];
                if (LastChatSong.Index < ChatPlayList.Count)
                {
                    CurrentSong = LastChatSong;
                }
            }
            if (CurrentSong == null)
            {
                CurrentSong = LastStreamerSong;
            }
        }

        private ICommand _playerClosedCommand;
        public ICommand PlayerClosedCommand
        {
            get
            {
                return _playerClosedCommand ?? (_playerClosedCommand = new Command((arg) => PlayerClosed()));
            }
        }
        public void PlayerClosed()
        {

            PlayerSaveObject tmp = new PlayerSaveObject();
            if (LastChatSong != null)
            {
                tmp.LastChatSong = LastChatSong;
            }
            else if (ChatPlayList.Count != 0)
            {
                tmp.LastChatSong = ChatPlayList[ChatPlayList.Count - 1];
            }

            if (LastStreamerSong != null)
            {
                tmp.LastStreamerSong = LastStreamerSong;
            }
            else if (StreamerPlaylist.Count != 0)
            {
                tmp.LastStreamerSong = StreamerPlaylist[0];
            }

            tmp.ChatPlayList = ChatPlayList;
            tmp.StreamerPlayList = StreamerPlaylist;
            string stringConfig = JsonConvert.SerializeObject(tmp);
            using (BinaryWriter writer = new BinaryWriter(File.Open(ConfigSet.PlayerSaveFilePath, FileMode.OpenOrCreate)))
            {
                writer.Write(stringConfig);
            }
        }

        private ICommand _startStopPlayerCommand;
        public ICommand StartStopPlayerCommand
        {
            get
            {
                return _startStopPlayerCommand ?? (_startStopPlayerCommand = new Command((arg) => StartStopPlayer()));
            }
        }
        private void StartStopPlayer()
        {
            if (_player.Player.PlayerState == YoutubePlayerState.playing)
            {
                if (_player.Player.PauseCommand.CanExecute(null))
                {
                    _player.Player.PauseCommand.Execute(null);
                    Autoplay = ConfigSet.Config.PlayerConfig.Autoplay;
                }
            }
            else
            {
                if (_player.Player.StartCommand.CanExecute(null))
                {
                    _player.Player.StartCommand.Execute(null);
                    Autoplay = ConfigSet.Config.PlayerConfig.Autoplay;
                }
            }

        }

        private ICommand _repeatSongCommand;
        public ICommand RepeatSongCommand
        {
            get
            {
                return _repeatSongCommand ?? (_repeatSongCommand = new Command((arg) => RepeatSong()));
            }
        }
        private void RepeatSong()
        {
            if (_player.Player.StopCommand.CanExecute(null))
            {
                _player.Player.StopCommand.Execute(null);
            }
        }


        private ICommand _playNextSongCommand;
        public ICommand PlayNextSongCommand
        {
            get
            {
                return _playNextSongCommand ?? (_playNextSongCommand = new Command((arg) => PlayNextSong()));
            }
        }
        public void PlayNextSong()
        {
            bool nextChatSong = false;

            if (ConfigSet.Config.PlayerConfig.ChatPlaylistOn)
            {
                if (ChatPlayList.Count != 0)
                {
                    if (LastChatSong != null)
                    {
                        if (LastChatSong.Index < ChatPlayList.Count)
                        {
                            int needindex = LastChatSong.Index + 1;
                            Song tmp = ChatPlayList.First(song => song.Index == needindex);
                            int index = tmp.Index;
                            PlayChatSongByIndex(index);
                            nextChatSong = true;
                        }
                    }
                    else
                    {
                        PlayChatSongByIndex(1);
                        nextChatSong = true;
                    }
                }
            }
            else
            {
                nextChatSong = false;
            }
            if (StreamerPlaylist.Count != 0 && nextChatSong == false)
            {
                int index = 1;
                if (LastStreamerSong != null)
                {
                    if (LastStreamerSong.Index == StreamerPlaylist.Count)
                    {
                        index = 1;
                    }
                    else
                    {
                        int needindex = LastStreamerSong.Index + 1;
                        Song tmp = StreamerPlaylist.First(song => song.Index == needindex);
                        index = tmp.Index;
                    }
                }
                PlayStreamerSongByIndex(index);
            }
        }

        private ICommand _playPreviousSong;
        public ICommand PlayPreviousSongCommand
        {
            get
            {
                return _playPreviousSong ?? (_playPreviousSong = new Command((arg) => PlayPreviousSong()));
            }
        }
        public void PlayPreviousSong()
        {
            if (StreamerPlaylist.Count != 0)
            {
                int index = 0;
                if (CurrentSong.Index == 1)
                {
                    index = StreamerPlaylist.Count;
                }
                else
                {
                    int needindex = CurrentSong.Index - 1;
                    Song tmp = StreamerPlaylist.First(song => song.Index == needindex);
                    index = tmp.Index;
                }
                PlayStreamerSongByIndex(index);
            }
        }

        private ICommand _clearStreamerPlayListCommand;
        public ICommand ClearStreamerPlayListCommand
        {
            get
            {
                return _clearStreamerPlayListCommand ?? (_clearStreamerPlayListCommand = new Command((arg) => ClearStreamerPlayList()));
            }
        }
        public void ClearStreamerPlayList()
        {
            StreamerPlaylist.Clear();
        }

        private ICommand _clearChatPlayListCommand;
        public ICommand ClearChatPlayListCommand
        {
            get
            {
                return _clearChatPlayListCommand ?? (_clearChatPlayListCommand = new Command((arg) => ClearChatPlayList()));
            }
        }
        public void ClearChatPlayList()
        {
            ChatPlayList.Clear();
            LastChatSong = null;
        }

        private ICommand _saveStreamerPlaylistInFileCommand;
        public ICommand SaveStreamerPlaylistInFileCommand
        {
            get
            {
                return _saveStreamerPlaylistInFileCommand ?? (_saveStreamerPlaylistInFileCommand = new Command((arg) => SaveStreamerPlaylistInFile()));
            }
        }
        public void SaveStreamerPlaylistInFile()
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "Text Files(Text Files(*.bin)|*.bin|All(*.*)|*"
            };
            if (dialog.ShowDialog() == true)
            {
                try
                {

                    string stringPlaylist = JsonConvert.SerializeObject(StreamerPlaylist);
                    using (BinaryWriter writer = new BinaryWriter(File.Open(dialog.FileName, FileMode.OpenOrCreate)))
                    {
                        writer.Write(stringPlaylist);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private ICommand _loadStreamerPlaylistFromFileCommand;
        public ICommand LoadStreamerPlaylistFromFileCommand
        {
            get
            {
                return _loadStreamerPlaylistFromFileCommand ?? (_loadStreamerPlaylistFromFileCommand = new Command((arg) => LoadStreamerPlaylistFromFile()));
            }
        }
        public void LoadStreamerPlaylistFromFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Text Files(Text Files(*.bin)|*.bin|All(*.*)|*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string stringPlaylist = "";
                    using (BinaryReader reader = new BinaryReader(File.Open(openFileDialog.FileName, FileMode.OpenOrCreate)))
                    {
                        stringPlaylist = reader.ReadString();
                    }
                    StreamerPlaylist.Clear();
                    ObservableCollection<Song> tmp = JsonConvert.DeserializeObject<ObservableCollection<Song>>(stringPlaylist);
                    foreach (var item in tmp)
                    {
                        StreamerPlaylist.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private ICommand _showPlayerSettingsCommand;
        public ICommand ShowPlayerSettingsCommand
        {
            get
            {
                return _showPlayerSettingsCommand ?? (_showPlayerSettingsCommand = new Command((arg) => ShowPlayerSettings()));
            }
        }

        private static void ShowPlayerSettings()
        {
            PlayerSettingsWindow tmp = new PlayerSettingsWindow();
            tmp.ShowDialog();
        }

        private void EndSongCheck(object sender, YoutubePlayerState e)
        {
            if (e == YoutubePlayerState.ended)
            {
                PlayNextSong();
            }
        }

        public Song CreateSongById(string idOrLink)
        {
            string id = GetIdByUrl(idOrLink);
            try
            {
                dynamic YouTubeInfo = JsonConvert.DeserializeObject(GetPostWeb.Request($"https://www.youtube.com/oembed?format=json&url=https://www.youtube.com/watch?v={ id }", "GET", "", null));
                return new Song()
                {
                    Index = 3,
                    Id = id,
                    YoutubeLink = $"https://www.youtube.com/watch?v={ id }",
                    Title = YouTubeInfo["title"].ToString(),
                    Image = new BitmapImage(new Uri(YouTubeInfo["thumbnail_url"].ToString())),
                    IsSelected = false
                };
            }
            catch (Exception e)
            {
                return null;
            }

        }
        private void StreamerReindexSongs(object sender, NotifyCollectionChangedEventArgs e)
        {
            int i = 1;
            foreach (Song item in StreamerPlaylist)
            {
                item.Index = i;
                i++;
            }
        }
        private void ChatReindexSongs(object sender, NotifyCollectionChangedEventArgs e)
        {
            int i = 1;
            foreach (Song item in ChatPlayList)
            {
                item.Index = i;
                i++;
            }
        }

        public void DeleteStreamerSongByIndex(int index)
        {
            StreamerPlaylist.RemoveAt(index - 1);
        }
        public void AddSong(Song song)
        {
            if (song != null)
            {
                StreamerPlaylist.Add(song);
            }
        }

        public void CopyStreamerSongLinkByIndex(int index)
        {
            string link = StreamerPlaylist.First(song => song.Index == index).YoutubeLink;
            Clipboard.SetText(link);
        }
        public void PlayStreamerSongByIndex(int index)
        {
            Song tmp = StreamerPlaylist.First(song => song.Index == index);
            if (CurrentSong != tmp)
            {
                CurrentSong = tmp;
                LastStreamerSong = CurrentSong;
                foreach (var item in StreamerPlaylist)
                {
                    item.IsSelected = false;
                }
                foreach (var item in ChatPlayList)
                {
                    item.IsSelected = false;
                }
                CurrentSong.IsSelected = true;

            }
            else
            {
                StartStopPlayer();
            }
        }

        public void DeleteChatSongByIndex(int index)
        {
            ChatPlayList.RemoveAt(index - 1);
        }
        public void CopyChatSongLinkByIndex(int index)
        {
            string link = ChatPlayList.First(song => song.Index == index).YoutubeLink;
            Clipboard.SetText(link);
        }
        public void PlayChatSongByIndex(int index)
        {
            Song tmp = ChatPlayList.First(song => song.Index == index);

            if (CurrentSong != tmp)
            {
                CurrentSong = tmp;
                LastChatSong = CurrentSong;
                foreach (var item in ChatPlayList)
                {
                    item.IsSelected = false;
                }
                foreach (var item in StreamerPlaylist)
                {
                    item.IsSelected = false;
                }
                CurrentSong.IsSelected = true;

            }
            else
            {
                StartStopPlayer();
            }

        }

        public void AddToStreamerPlaylistFromChatPlaylistByIndex(int index)
        {
            string id = ChatPlayList.First(song => song.Index == index).Id;
            AddSong(CreateSongById(id));
        }

        public static string GetIdByUrl(string id)
        {
            if (id == null)
            {
                return "";
            }
            if (id.Length < 11)
            {
                return "";
            }
            else if (id.Length == 11)
            {
                return id;
            }
            string substr = "watch?v=";
            int index = id.LastIndexOf(substr);
            if (index == -1)
            {
                return "";
            }
            return id.Substring(index + substr.Length);
        }

        //out methods
        public string AddChatSong(string idOrYoutubeLink, string viewer)
        {
            string resp = "";
            //_player.Dispatcher.Invoke()
            //App.Current.Dispatcher.Invoke(() =>
            //{
            _player.Dispatcher.Invoke(() =>
            {
                Song song = CreateSongById(idOrYoutubeLink);
                if (song != null)
                {
                    try
                    {
                        song.Image.Freeze();
                    }
                    catch
                    {

                    }
                    song.Viewer = viewer;
                    ChatPlayList.Add(song);
                    int qeuePosition;
                    if (ChatPlayList.Count == 1)
                    {
                        qeuePosition = 0;
                    }
                    else if (LastChatSong == null)
                    {
                        qeuePosition = ChatPlayList.Count;
                    }
                    else
                    {
                        qeuePosition = ChatPlayList.Count - LastChatSong.Index;
                    }
                    resp = $"Song {song.Title} . Was added, you position in qeue : #{qeuePosition}";
                }
                else
                {
                    resp = "";
                }
            });

            //});
            return resp;
        }

        public string AddFirstChatSong(string idOrYoutubeLink, string viewer)
        {
            string resp = "";
            //App.Current.Dispatcher.Invoke(() =>
            //{
            _player.Dispatcher.Invoke(() =>
            {

                Song song = CreateSongById(idOrYoutubeLink);
                if (song != null)
                {
                    try
                    {
                        song.Image.Freeze();
                    }
                    catch (Exception e)
                    {

                    }

                    song.Viewer = viewer;
                    int qeuePosition = 0;
                    if (LastChatSong == null)
                    {
                        ChatPlayList.Add(song);
                    }
                    else
                    {
                        ChatPlayList.Insert(LastChatSong.Index, song);
                    }

                    resp = $"Song {song.Title} . Was added out of turn! you position in qeue : #{qeuePosition}";
                }
                else
                {
                    resp = "";
                }
            });
            // });
            return resp;
        }

        public string GetCurrentSongInfo()
        {
            string resp;
            string customer = CurrentSong.Viewer ?? " Broadcaster ";
            if (CurrentSong != null)
            {
                resp = $"Current song : ' {CurrentSong.Title} '.    \n Link : {CurrentSong.YoutubeLink} .      \n Customer : {customer}";
            }
            else
            {
                resp = "Playlist empty!";
            }
            return resp;
        }


        //GetPlaylist in strings 
        public string GetStreamerPlaylistInfo()
        {
            string result = "";
            int i = 1;
            foreach (var song in StreamerPlaylist)
            {
                result += $"{i}) ' {song.Title} ' ||| Link : {song.YoutubeLink} ->>>";
                i++;
            }
            return result;
        }

        public string GetChatPlaylistInfo()
        {
            string result = "";
            int i = 1;
            foreach (var song in ChatPlayList)
            {
                result += $"{i}) ' {song.Title} ' ||| Link : {song.YoutubeLink} ->>>";
                i++;
            }
            return result;
        }



    }
}
