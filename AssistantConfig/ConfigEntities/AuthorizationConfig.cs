
using DataClasses.ConfigDataClasses;

namespace AssistantConfig.ConfigEntities
{
    public  class AuthorizationConfig
    {
        public AuthorizationInstance StreamerAuth { get; set; } = new AuthorizationInstance();
        public AuthorizationInstance BotAuth { get; set; } = new AuthorizationInstance();
    }
}
