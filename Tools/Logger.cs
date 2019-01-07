using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class Logger : SingletonBaseTemplate<Logger>
    {
        private Logger() { }
        private static object loglocker = new object();

        public void WriteLog(string message, string fileName)
        {

            string logMessage = $"{DateTime.Now} {message}";
            string path = $@".\Logs";
            logMessage += "\r\n";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            lock (loglocker)
            {
                File.AppendAllText(Path.Combine(path, $"{fileName}_Log.txt"), logMessage);
            };

        }
    }
}
