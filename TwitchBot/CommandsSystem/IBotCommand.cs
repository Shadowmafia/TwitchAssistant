using TwitchLib.Client.Models;

namespace TwitchBot.CommandsSystem
{
    public interface IBotCommand
    {
        string Name { get; set; }
        int Id { get; }
        string Description { get; set; }
        bool IsEnabled { get; set; }
        bool Whisp { get; set; }
        bool Message { get; set; }

        void ExecuteCommand(ChatMessage viewer, string commandBody = null);
    }
}
