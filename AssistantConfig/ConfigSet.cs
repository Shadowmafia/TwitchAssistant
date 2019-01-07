using System.IO;
using Newtonsoft.Json;

namespace AssistantConfig
{
    public static class ConfigSet
    {

        //Путь к файлу конфигов
        private static readonly string ConfigFilePath = "AsistantTemproryFiles/Config.bin";
        public static readonly string PlayerSaveFilePath = "AsistantTemproryFiles/PlayerSavePoint.bin";
        public static Config Config;
       
        static ConfigSet()
        {
            Config = new Config();     
            //CefSettings settings = new CefSettings();
            //settings.CachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\CEF";
            //Cef.Initialize(settings);

            string path = $@"AsistantTemproryFiles";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (!File.Exists(ConfigFilePath))
            {
                //true debug
                Config.DefaultConfig(false);
                SaveConfig();
            }
            else
            {
                ReadConfig();
            }

            //FileInfo someFileInfo = new FileInfo(filePath);
            //long fileByteSize = someFileInfo.Length;
        }

        //Сохранение конфигов в файл
        public static void SaveConfig()
        {
            string stringConfig = JsonConvert.SerializeObject(Config);
            using (BinaryWriter writer = new BinaryWriter(File.Open(ConfigFilePath, FileMode.OpenOrCreate)))
            {
                writer.Write(stringConfig);
            }
        }

        //Чтение конфигов из конфиг файла 
        public static void ReadConfig()
        {
            string stringConfig = "";
            using (BinaryReader reader = new BinaryReader(File.Open(ConfigFilePath, FileMode.OpenOrCreate)))
            {
                stringConfig = reader.ReadString();
            }
            Config = JsonConvert.DeserializeObject<Config>(stringConfig);
        }
    }
}
