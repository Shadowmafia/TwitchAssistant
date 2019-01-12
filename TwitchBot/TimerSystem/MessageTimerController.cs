using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using DataClasses.Enums;
using DateBaseController;
using DateBaseController.Models;
using Tools;
using Timer = System.Timers.Timer;

namespace TwitchBot.TimerSystem
{
    public class MessageTimerController: SingletonBaseTemplate<MessageTimerController>
    {
       
        private readonly Timer _timer;

        public bool IsStarted { get; private set; }

        private MessageTimerController()
        {
            _timer = new Timer();
            _timer.Elapsed += SendMessages;
            _setInterval();
        }

        private void SendMessages(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
        
            if (TwitchBotGlobalObjects.TwitchBotConnectedState == TwitchBotConnectedState.Connected)
            {
               List<MessageTimer> timers = AssistantDb.Instance.Timers.GetAll() as List<MessageTimer>;
                for (int i = 0; i < timers.Count; i++)
                {
                    if (timers[i].IsEnabled && timers[i].LastShow <= DateTime.Now.Subtract(new TimeSpan(timers[i].Interval)))
                    {
                        TwitchBotGlobalObjects.Bot.SendMessage(timers[i].Message);
                        timers[i].LastShow = DateTime.Now;
                        Thread.Sleep(200);                    
                    }
                }

            }
         
            if (IsStarted)
            {
                _timer.Start();
            }
        }

        private void _setInterval()
        {
            _timer.Interval = 1000;
        }


        public void Start()
        {

            IsStarted = true;
            _timer.Start();
        }

        public void Stop()
        {
            IsStarted = false;
            _timer.Stop();
        }

    }
}
