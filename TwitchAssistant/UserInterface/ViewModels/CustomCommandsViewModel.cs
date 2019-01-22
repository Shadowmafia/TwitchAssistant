using DataClasses.Enums;
using DateBaseController;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tools.MVVMBaseClasses;
using TwitchBot.CommandsSystem;
using TwitchBot.CommandsSystem.Commands;
using TwitchBot.CommandsSystem.CommandsFunctional;

namespace TwitchAssistant.UserInterface.ViewModels
{
    class CustomCommandsViewModel : ViewModelBase
    {
        public ObservableCollection<CustomBotCommand> Commands { get; set; } = CommandsController.CustomCommandsList;

        private ICommand _addCustomCommand;
        public ICommand AddCustomCommand
        {
            get
            {
                return _addCustomCommand ?? (_addCustomCommand = new Command((arg) => AddCommand()));
            }
        }

        private void AddCommand()
        {
            int newCommandId = Commands.Count;
            CustomBotCommand command = new CustomBotCommand()
            {
                Id= newCommandId,
                Name = "YouCommandName",
                Message = true,
                Whisp = false,
                IsEnabled = true,            
                Description = "You command description",
                IsUserLevelErrorResponse = true,
                UserLevel = TwitchRangs.Unfollower,
                GlobalCooldown = new TimeSpan(0,0,5),
                IsGlobalCooldown = false,
                IsGlobalErrorResponse = false,
                IsWhispErrors = false,
                IsChatErrors = false,
                Action = "SendCustomCommandMessage",
                ResponseMessage = "You message"
            };
            command.SetNewAction(CustomCommandsFunctional.Actions[command.Action]);
            Commands.Add(command);
        }

        public void DeleteCommandById(int id)
        {
            var command = Commands.First(t => t.Id == id);
            if (command != null)
            {
                Commands.Remove(command);
                AssistantDb.Instance.CustomCommands.Delete(command.Id);
                AssistantDb.Instance.SaveChanges();
            }
        }
    }
}
