﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataClasses.Enums;
using DateBaseController.Models.CommandsModels;
using TwitchLib.Client.Models;

namespace TwitchBot.CommandsSystem.Commands
{
    class CustomBotCommand : BotCommand<CustomBotCommand>
    {
        public CustomBotCommand(BaseCommand commandFromDataBase) : base(commandFromDataBase)
        {
        }

        public string ResponseMessage { get; set; }
   
    }
}
