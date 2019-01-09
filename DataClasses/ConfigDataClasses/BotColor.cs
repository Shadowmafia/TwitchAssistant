using System.Windows.Media;
using TwitchLib.Client.Enums;

namespace DataClasses.ConfigDataClasses
{
    public class BotColor
    {
       
        public Brush RbgColor { get; set; }
        public ChatColorPresets Color  { get; set; }
    }
}