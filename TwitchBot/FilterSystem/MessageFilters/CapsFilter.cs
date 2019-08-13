using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBot.ChatFilter;

namespace TwitchBot.FilterSystem
{
    class CapsFilter : BaseFilter
    {
        protected override bool FilterCheckAction()
        {
            int capsCount = 0;

            foreach (var c in this._message.Message)
            {
                if (Char.IsUpper(c))
                {
                    capsCount++;
                }
            }

            return capsCount >= this._message.Message.Length / 2;
        }
    }
}
