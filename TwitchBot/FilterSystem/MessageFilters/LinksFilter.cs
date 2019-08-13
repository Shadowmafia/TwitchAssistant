using System.Linq;
using TwitchBot.ChatFilter;

namespace TwitchBot.FilterSystem
{
    public class LinksFilter : BaseFilter
    {
        protected override bool FilterCheckAction()
        {
            return _message.Message.Contains("https://") || _message.Message.Contains("http://");
        }
    }
}
