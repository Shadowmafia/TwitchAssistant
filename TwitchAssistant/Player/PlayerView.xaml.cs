using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TwitchAssistant.Player
{
    /// <summary>
    /// Логика взаимодействия для PlayerView.xaml
    /// </summary>
    public partial class PlayerView : UserControl
    {
        public PlayerView()
        {                     
            InitializeComponent();
            this.DataContext = new MyPlayer(this);
        }

        private void DeleteSongStreamerPlaylist(object sender, RoutedEventArgs e)
        {
           int index = Int32.Parse(((Button)sender).Tag.ToString());
           GlobalObjects.Player.DeleteStreamerSongByIndex(index);
        }
        private void DeleteSongChatPlaylist(object sender, RoutedEventArgs e)
        {
            MenuItem mItem = (MenuItem)sender;
            var tag = mItem.Tag;
            int index = Int32.Parse(tag.ToString());
            GlobalObjects.Player.DeleteChatSongByIndex(index);
        }

        private void CopySongLinkStreamerPlaylist(object sender, RoutedEventArgs e)
        {
            int index = Int32.Parse(((Button)sender).Tag.ToString());
            GlobalObjects.Player.CopyStreamerSongLinkByIndex(index);
        }
        private void CopySongLinkChatPlaylist(object sender, RoutedEventArgs e)
        {
            MenuItem mItem = (MenuItem)sender;
            var tag = mItem.Tag;
            int index = Int32.Parse(tag.ToString());
            GlobalObjects.Player.CopyChatSongLinkByIndex(index);
        }

        private void PlayStreamerSong(object sender, RoutedEventArgs e)
        {
            int index = Int32.Parse(((Button)sender).Tag.ToString());           
            GlobalObjects.Player.PlayStreamerSongByIndex(index);
        }
        private void PlayStreamerSongDocPanel(object sender, MouseButtonEventArgs e)
        {
            int index = Int32.Parse(((DockPanel)sender).Tag.ToString());
            GlobalObjects.Player.PlayStreamerSongByIndex(index);
        }

        private void PlayChatSong(object sender, RoutedEventArgs e)
        {
            int index = Int32.Parse(((Button)sender).Tag.ToString());
            GlobalObjects.Player.PlayChatSongByIndex(index);
        }
        private void PlayChatSongDocPanel(object sender, MouseButtonEventArgs e)
        {
            int index = Int32.Parse(((DockPanel)sender).Tag.ToString());
            GlobalObjects.Player.PlayChatSongByIndex(index);
        }
        private void AddToStreamerPlaylist(object sender, RoutedEventArgs e)
        {
            int index = Int32.Parse(((Button)sender).Tag.ToString());
            GlobalObjects.Player.AddToStreamerPlaylistFromChatPlaylistByIndex(index);

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
            GlobalObjects.Player.AddSong(GlobalObjects.Player.CreateSongById(SongLingOrId.Text));
            AddSongsDialog.IsOpen = false;
            SongLingOrId.Text = "";         
        }


        private void ShowClearStreamerPlaylistDialog(object sender, RoutedEventArgs e)
        {
            ClearStreamerPlaylistDialog.IsOpen = true;
        }
        private void ClearStreamerPlaylist(object sender, RoutedEventArgs e)
        {
            GlobalObjects.Player.ClearStreamerPlayList();
            ClearStreamerPlaylistDialog.IsOpen = false;
        }

        private void ShowClearChatPlaylistDialog(object sender, RoutedEventArgs e)
        {
            ClearChatPlayListDialog.IsOpen = true; ;
        }
        private void ClearChatPlaylist(object sender, RoutedEventArgs e)
        {
            GlobalObjects.Player.ClearChatPlayList();
            ClearChatPlayListDialog.IsOpen = false; 
        }

    }
}
