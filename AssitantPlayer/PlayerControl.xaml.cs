using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AssitantPlayer
{
    /// <summary>
    /// Логика взаимодействия для PlayerView.xaml
    /// </summary>
    public partial class PlayerControl : UserControl
    {
        public PlayerControl()
        {                     
            InitializeComponent();
            MyPlayer.Instance.SetPlayerControl(this);
            this.DataContext = MyPlayer.Instance;
        }

        private void DeleteSongStreamerPlaylist(object sender, RoutedEventArgs e)
        {
           int index = Int32.Parse(((Button)sender).Tag.ToString());
            MyPlayer.Instance.DeleteStreamerSongByIndex(index);
        }
        private void DeleteSongChatPlaylist(object sender, RoutedEventArgs e)
        {
            MenuItem mItem = (MenuItem)sender;
            var tag = mItem.Tag;
            int index = Int32.Parse(tag.ToString());
            MyPlayer.Instance.DeleteChatSongByIndex(index);
        }

        private void CopySongLinkStreamerPlaylist(object sender, RoutedEventArgs e)
        {
            int index = Int32.Parse(((Button)sender).Tag.ToString());
            MyPlayer.Instance.CopyStreamerSongLinkByIndex(index);
        }
        private void CopySongLinkChatPlaylist(object sender, RoutedEventArgs e)
        {
            MenuItem mItem = (MenuItem)sender;
            var tag = mItem.Tag;
            int index = Int32.Parse(tag.ToString());
            MyPlayer.Instance.CopyChatSongLinkByIndex(index);
        }

        private void PlayStreamerSong(object sender, RoutedEventArgs e)
        {
            int index = Int32.Parse(((Button)sender).Tag.ToString());
            MyPlayer.Instance.PlayStreamerSongByIndex(index);
        }
        private void PlayStreamerSongDocPanel(object sender, MouseButtonEventArgs e)
        {
            int index = Int32.Parse(((Grid)sender).Tag.ToString());
            MyPlayer.Instance.PlayStreamerSongByIndex(index);
        }

        private void PlayChatSong(object sender, RoutedEventArgs e)
        {
            int index = Int32.Parse(((Button)sender).Tag.ToString());
            MyPlayer.Instance.PlayChatSongByIndex(index);
        }
        private void PlayChatSongDocPanel(object sender, MouseButtonEventArgs e)
        {
            int index = Int32.Parse(((Grid)sender).Tag.ToString());
            MyPlayer.Instance.PlayChatSongByIndex(index);
        }
        private void AddToStreamerPlaylist(object sender, RoutedEventArgs e)
        {
            int index = Int32.Parse(((Button)sender).Tag.ToString());
            MyPlayer.Instance.AddToStreamerPlaylistFromChatPlaylistByIndex(index);

        }


        private void OpenContextMenu(object sender, RoutedEventArgs e)
        {
            Button tmp =  (sender as Button);
            ContextMenu contextMenu = tmp.ContextMenu;
            contextMenu.PlacementTarget = tmp;
            contextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Top;
            contextMenu.IsOpen = true;
        }


        private void ShowAddSongDialog(object sender, RoutedEventArgs e)
        {
            AddSongsDialog.IsOpen = true;
        }
        private void AddNewSong(object sender, RoutedEventArgs e)
        {
            MyPlayer.Instance.AddSong(MyPlayer.Instance.CreateSongById(SongLingOrId.Text));
            AddSongsDialog.IsOpen = false;
            SongLingOrId.Text = "";         
        }


        private void ShowClearStreamerPlaylistDialog(object sender, RoutedEventArgs e)
        {
            ClearStreamerPlaylistDialog.IsOpen = true;
        }
        private void ClearStreamerPlaylist(object sender, RoutedEventArgs e)
        {
            MyPlayer.Instance.ClearStreamerPlayList();
            ClearStreamerPlaylistDialog.IsOpen = false;
        }

        private void ShowClearChatPlaylistDialog(object sender, RoutedEventArgs e)
        {
            ClearChatPlayListDialog.IsOpen = true; ;
        }
        private void ClearChatPlaylist(object sender, RoutedEventArgs e)
        {
            MyPlayer.Instance.ClearChatPlayList();
            ClearChatPlayListDialog.IsOpen = false; 
        }

    }
}
