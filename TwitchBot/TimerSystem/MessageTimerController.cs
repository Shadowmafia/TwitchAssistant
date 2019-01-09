using System;
using System.Threading;
using System.Timers;
using DataClasses.Enums;
using DateBaseController;
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
                foreach (var timer in DbRepository.Instance.GetMessageTimers())
                {
                    if (timer.IsEnabled&& timer.LastShow <= DateTime.Now.Subtract(new TimeSpan(timer.Interval)))
                    {
                        TwitchBotGlobalObjects.Bot.SendMessage(timer.Message);
                        Thread.Sleep(600);
                        timer.LastShow = DateTime.Now;
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
