using System;
using System.Threading;
using System.Windows;
using AssistantConfig;
using AssitantPlayer;
using DataClasses.Enums;
using DateBaseController;
using DateBaseController.ModelsRepositoryes;
using TwitchBot.CoinSystem;
using TwitchBot.CommandsSystem;
using TwitchBot.TimerSystem;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;

namespace TwitchBot
{
    public class Bot 
    {
        private readonly ViewersController _viewersController;
        public TwitchClient Client { get;}
        public string Channel { get; set; }
        public string BotName { get; }


        public Bot()
        {
            BotName = "tmpName";
            ViewerRepository load = AssistantDb.Instance.Viewers;
            _viewersController = new ViewersController();

            try
            {
                ConnectionCredentials credentials;
                if (ConfigSet.Config.BotConfig.IsDualMode)
                {

                    BotName = ConfigSet.Config.BotConfig.BotName;
                    credentials = new ConnectionCredentials(BotName,ConfigSet.Config.Auth.BotAuth.Tokens.AccessToken);
                    BotName = "";
                }
                else
                {
                    credentials = new ConnectionCredentials(BotName,ConfigSet.Config.Auth.StreamerAuth.Tokens.AccessToken);
                    BotName = "@" + ConfigSet.Config.BotConfig.BotName + " : ";
                }
                Client = new TwitchClient();
                Client.Initialize(credentials, ConfigSet.Config.BotConfig.StreamName);
                Client.OnConnected += Client_OnConnected;
                Client.OnConnectionError += Client_OnConnectedError;
                Client.OnDisconnected += Client_onDisconnected;

                Client.OnUserJoined += OnUserJoined;
                Client.OnUserLeft += OnUserLeft;
                Client.OnNewSubscriber += OnNewSubscriber;

                Client.OnJoinedChannel += OnJoinedChannel;
                Client.OnMessageReceived += OnMessageReceived;

                Client.Connect();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Bot constructor ex : incorrect bot name");
            }      
            
        }

 
        private void Client_onDisconnected(object sender, OnDisconnectedEventArgs e)
        {
            MessageTimerController.Instance.Stop();
            CoinSystem.CoinSystem.Instance.Stop();
            TwitchBotGlobalObjects.TwitchBotConnectedState = TwitchBotConnectedState.Disconnected;
            MessageBox.Show("AutoDisconnect");
        }
        public void Disconnected()
        {
            MessageTimerController.Instance.Stop();
            CoinSystem.CoinSystem.Instance.Stop();
            Client.Disconnect();
            TwitchBotGlobalObjects.TwitchBotConnectedState = TwitchBotConnectedState.Disconnected;
            MessageBox.Show($"Disconnected");
            try
            {
                MyPlayer.Instance.OnSongChanged -= ChangedSongNotify;
            }
            catch
            {
                // ignored
            }
        }
        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            //Todo:Debubug
             MessageTimerController.Instance.Start();
            //Todo:=============================
            CoinSystem.CoinSystem.Instance.Start();
            Client.ChangeChatColor(ConfigSet.Config.BotConfig.StreamName, ConfigSet.Config.BotConfig.BotColor.Color);
            TwitchBotGlobalObjects.TwitchBotConnectedState = TwitchBotConnectedState.Connected;
            Channel = e.AutoJoinChannel;
        }
        private void OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            MyPlayer.Instance.OnSongChanged += ChangedSongNotify;
            Client.SendMessage(e.Channel, $"{BotName} Joined to chanel !");
        }
        private static void ChangedSongNotify(object sender, Song e)
        {
            if (ConfigSet.Config.PlayerConfig.CurrentSongNotify)
            {
                TwitchBotGlobalObjects.Bot.SendMessage(MyPlayer.Instance.GetCurrentSongInfo());
            }
        }
        private void Client_OnConnectedError(object sender, OnConnectionErrorArgs e)
        {
            MessageTimerController.Instance.Stop();
            TwitchBotGlobalObjects.TwitchBotConnectedState = TwitchBotConnectedState.Reconnect;
            MessageBox.Show($"Bot cant connect to chanel");
            Client.Connect();
        }

        private void OnUserJoined(object sender, OnUserJoinedArgs e)
        {       
            try
            {
                _viewersController.OnUserJoined(sender, e);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Bot on user joined ex : "+exception.Message);
            }
        }
        private void OnUserLeft(object sender, OnUserLeftArgs e)
        {
            _viewersController.OnUserLeft(sender, e);
        }

        private void OnNewSubscriber(object sender, OnNewSubscriberArgs e)
        {
            MessageBox.Show("New subscriber!!!");
        }
   
    

        private void OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {                        
            if (e.ChatMessage.Message[0] == '!')
            {
                string commandName;
                string commandBody;
               
               
                int index = e.ChatMessage.Message.IndexOf(' ');
                if (index == -1)
                {
                    commandName = e.ChatMessage.Message;
                    commandBody = null;
                }
                else
                {
                    commandName = e.ChatMessage.Message.Substring(0, index);
                    commandBody = e.ChatMessage.Message.Substring(index + 1);
                }

                ThreadPool.QueueUserWorkItem((o) =>
                {
                    bool result = DefaultCommandsSet.Commands.ExecuteCommandByName(e.ChatMessage, commandName, commandBody);
                });
            }                    
        }

        public void SendMessage(string message)
        {
            Client.SendMessage(Channel,BotName+message);
        }

        public void WhispMessage(string user, string message)
        {
            Client.SendWhisper(user,BotName+message);
        }
    }

}
