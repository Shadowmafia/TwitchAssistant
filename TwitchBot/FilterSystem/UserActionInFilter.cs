using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client.Models;

namespace TwitchBot.FilterSystem
{
    public class UserActionInFilter
    {
        public ChatMessage User { get; set; }
        public int CountExecuting { get; set; }
        public int CountWarning { get; set; }
    }
}
