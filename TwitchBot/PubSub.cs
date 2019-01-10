using System;
using System.Timers;
using TwitchLib.Client.Events;
using TwitchLib.PubSub;
using TwitchLib.PubSub.Events;

namespace TwitchBot
{
    public class PubSub
    {
        private static TwitchPubSub client;
        private static readonly Timer _streamOnlineTimer;
        static PubSub()
        {
            _streamOnlineTimer = new Timer();
            _streamOnlineTimer.Elapsed += Tick;
            _streamOnlineTimer.Interval = 1000;
        }

        public PubSub()
        {
            client = new TwitchPubSub();

            client.OnPubSubServiceConnected += OnPubSubServiceConnected;
            client.OnListenResponse += OnListenResponse;
            client.OnStreamUp += OnStreamUp;
            client.OnStreamDown += OnStreamDown;
            client.ListenToVideoPlayback(TwitchApiController.Channel.Name);
            client.Connect();
        }

        public void Disconnect()
        {
            client.Disconnect();
            TwitchBotGlobalObjects.IsStreamOnline = false;
            _streamOnlineTimer.Stop();
        }
        private static void Tick(object sender, ElapsedEventArgs e)
        {
            _streamOnlineTimer.Stop();
            if (TwitchBotGlobalObjects.StreamUpTime != null)
            {
                TwitchBotGlobalObjects.StreamUpTime = new TimeSpan(TwitchBotGlobalObjects.StreamUpTime.Value.Hours, TwitchBotGlobalObjects.StreamUpTime.Value.Minutes, TwitchBotGlobalObjects.StreamUpTime.Value.Seconds + 1);
            }
            _streamOnlineTimer.Start();
        }

        private static async void OnPubSubServiceConnected(object sender, EventArgs e)
        {
            // SendTopics accepts an oauth optionally, which is necessary for some topics
            client.SendTopics();
            bool isOnline = await TwitchApiController.CheckStreamOnline();
            if (isOnline)
            {
                TwitchBotGlobalObjects.IsStreamOnline = true;
                TimeSpan? upTime = await TwitchApiController.GetStreamUpTime();
                if (upTime != null)
                {
                    TwitchBotGlobalObjects.StreamUpTime = new TimeSpan(upTime.Value.Hours, upTime.Value.Minutes, upTime.Value.Seconds);
                }
                _streamOnlineTimer.Start();
            }
        }
        private static void OnListenResponse(object sender, OnListenResponseArgs e)
        {
            if (!e.Successful)
                throw new Exception($"Failed to listen! Response: {e.Response}");
        }
        private static async void OnStreamUp(object sender, OnStreamUpArgs e)
        {
            TwitchBotGlobalObjects.IsStreamOnline = true;
            TimeSpan? upTime = await TwitchApiController.GetStreamUpTime();
            if (upTime != null)
            {
                TwitchBotGlobalObjects.StreamUpTime = new TimeSpan(upTime.Value.Hours, upTime.Value.Minutes, upTime.Value.Seconds);
            }
            _streamOnlineTimer.Start();
        }
        private static void OnStreamDown(object sender, OnStreamDownArgs e)
        {
            TwitchBotGlobalObjects.IsStreamOnline = false;
            _streamOnlineTimer.Stop();
        }

    }

}
