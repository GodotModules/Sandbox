using Newtonsoft.Json;
using Environment = System.Environment;
using Directory = System.IO.Directory;
using Path = System.IO.Path;
using File = System.IO.File;

namespace GodotModules
{
    public class SystemFileManager
    {
        public readonly string GameDataPath = Path.Combine
        (
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
            "Godot Modules"
        );

        public T WriteConfig<T>(string pathToFile) where T : new() => 
            WriteConfig(pathToFile, new T());

        public T WriteConfig<T>(string pathToFile, T data)
        {
            var path = GetConfigPath(pathToFile);
            var contents = JsonConvert.SerializeObject(data, Formatting.Indented);
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            File.WriteAllText(path, contents);
            return data;
        }

        public T ReadConfig<T>(string pathToFile)
        {
            var path = GetConfigPath(pathToFile);
            return File.Exists(path) ? JsonConvert.DeserializeObject<T>(File.ReadAllText(path)) : default;
        }

        public bool ConfigExists(string pathToFile) => 
            File.Exists(GetConfigPath(pathToFile));

        private string GetConfigPath(string pathToFile) => 
            $"{GameDataPath}{Path.DirectorySeparatorChar}{pathToFile}.json";
    }
}