
using System.ComponentModel;
using TwitchAssistant.Player;
using TwitchAssistant.Player.PlayerSettings;

namespace TwitchAssistant
{
    public static class GlobalObjects 
    {
        public static event PropertyChangedEventHandler StaticPropertyChanged;
        private static void OnStaticPropertyChanged(string propertyName)
        {
            var handler = StaticPropertyChanged;
            if (handler != null)
            {
                handler(null, new PropertyChangedEventArgs(propertyName));
            }
        }


        public static MyPlayer Player { get; set; }
      
        static GlobalObjects()
        {
   
        }
    }
 
}
