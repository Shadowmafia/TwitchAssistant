using DataClasses.Enums;

namespace AssistantConfig.ConfigEntities
{
    public class PlayerConfig
    {
        public bool ChatPlaylistOn { get; set; }
        public bool IsUsingCoinSystem { get; set; }
       
        public int SongPrice { get; set; }
        public int FirstSongPrice { get; set; }
        public int SkipSongPrice { get; set; }

        public TwitchRangs MinRequestRang { get; set; }

        public int UnfollowRequestsPerHours { get; set; }
        public int FollowerRequestsPerHours { get; set; }
        public int SubscriberRequestsPerHours { get; set;}

        public bool CurrentSongNotify { get; set; }
        public bool Autoplay { get; set; }

    }
}
