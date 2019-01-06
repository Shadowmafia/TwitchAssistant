using DataClasses.ConfigDataClasses;

namespace AssistantConfig.ConfigEntities
{
    public class BotConfig
    {
        public bool IsDualMode { get; set; }
        public string BotName { get; set; }
        public string StreamName { get; set; }
        public BotColor BotColor { get; set; }
        static BotConfig()
        { 
            
        }
    }
}
