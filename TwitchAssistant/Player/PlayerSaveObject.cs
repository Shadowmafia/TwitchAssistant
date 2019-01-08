using System.Collections.ObjectModel;

namespace TwitchAssistant.Player
{
    internal class PlayerSaveObject
    {

        public ObservableCollection<Song> StreamerPlayList { get; set; } = new ObservableCollection<Song>();
        public Song LastStreamerSong{ get; set; }

        public ObservableCollection<Song> ChatPlayList{ get; set; }=new  ObservableCollection<Song>();
        public Song LastChatSong { get; set; }
       
    }
}
