using System.Collections.ObjectModel;
using DateBaseController.Models;
using TwitchLib.Api.V5.Models.Channels;

namespace TwitchBot.CoinSystem
{
    public class ChanelData
    {
        public ObservableCollection<Viewer> Watchers { get; set; }
        public ObservableCollection<ChannelFollow> Followers { get; set; }
        public ChanelData()
        {
            Watchers = new ObservableCollection<Viewer>();
            Followers = new ObservableCollection<ChannelFollow>();
        }
    }

}
