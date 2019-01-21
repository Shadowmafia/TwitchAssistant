using DataClasses.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateBaseController.Models.CommandsModels
{
    public class BaseCommand
    {
        //Basic bot command fields....
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public bool Whisp { get; set; }
        public bool Message { get; set; }

        public bool IsUserLevelErrorResponse { get; set; }
        public TwitchRangs UserLevel { get; set; }

        public long GlobalCooldown { get; set; }
        public bool IsGlobalCooldown { get; set; }
        public bool IsGlobalErrorResponse { get; set; }

        public bool IsWhispErrors { get; set; }
        public bool IsChatErrors { get; set; }
        /*
        public long UserCooldown { get; set; }
        public bool IsUserCooldown { get; set; }
        public bool IsUserErrorResponse { get; set; }
        */
        public string Action { get; set; }
    }
}
