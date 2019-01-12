using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DateBaseController;
using DateBaseController.Models;
using Tools.MVVMBaseClasses;

namespace TwitchAssistant.UserInterface.ViewModels
{
    public class TimersControlViewModel:ViewModelBase
    {
        public ObservableCollection<MessageTimer> MessageTimers { get; set; }
        public TimersControlViewModel()
        {
            MessageTimers= new ObservableCollection<MessageTimer>();
            LoadTimersFromDb();
        }

        public void LoadTimersFromDb()
        {
            MessageTimers.Clear();

            foreach (var timer in AssistantDb.Instance.Timers.GetAll())
            {
                MessageTimers.Add(timer);
            }
        }

        private ICommand _addTimerCommand;
        public ICommand AddTimerCommand
        {
            get
            {
                return _addTimerCommand ?? (_addTimerCommand = new Command((arg) => AddTimer()));
            }
        }
        private void AddTimer()
        {
            MessageTimer newTimer = new MessageTimer()
            {
                Message = "Enter you message and enble timer(Togle button ->)!",
                IsEnabled = false,
                Interval = new TimeSpan(0,0,10).Ticks,
                LastShow = DateTime.Now
            };
            MessageTimers.Add(newTimer);
            AssistantDb.Instance.Timers.Add(newTimer);
            SaveChange();
        }

        public void SaveChange()
        {
            AssistantDb.Instance.SaveChanges();
        }

        public void DeleteTimerById(int id)
        {
            var timer = MessageTimers.First(t => t.Id == id);
            if (timer != null)
            {
                MessageTimers.Remove(timer);
                AssistantDb.Instance.Timers.Delete(timer.Id);
                AssistantDb.Instance.SaveChanges();
            }          
        }
    }
}

