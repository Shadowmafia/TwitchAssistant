using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBot.ChatFilter;

namespace TwitchBot.FilterSystem.MessageFilters
{
    class BadWordsFilter : BaseFilter
    {
        public List<string> BadWords = new List<string>()
        {
            "хуй",
            "залупа"
        };

        protected override bool FilterCheckAction()
        {
            string message = this._message.Message.ToLower();

            foreach (var word in BadWords)
            {
                if (message.Contains(word))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
