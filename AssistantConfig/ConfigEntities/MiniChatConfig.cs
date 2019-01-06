using System.Windows.Media;
using DataClasses.ConfigDataClasses;

namespace AssistantConfig.ConfigEntities
{ 
    public class MiniChatConfig
    {
        public bool Topmost { set; get; }
        public bool IsLocked { set; get; }
        public bool ChatSettingsAndInput { set; get; }
        public bool ChatUsage { set; get; }
        public Color BgColor { set; get; }
        public Point ChatPosition { set; get; }
        public Size ChatSize { set; get; }
    }
}
