namespace GodotModules
{
    public class GodotFileManager
    {
        public string GetProjectPath() => OS.HasFeature("standalone")
            ? System.IO.Directory.GetParent(OS.GetExecutablePath())!.FullName
            : ProjectSettings.GlobalizePath("res://");

        public string ReadFile(string path) 
        {
            var file = new File();
            var error = file.Open($"res://{path}", File.ModeFlags.Read);
            if (error != Godot.Error.Ok) 
            {
                System.Console.WriteLine(error);
                return "";
            }
            var content = file.GetAsText();
            file.Close();
            return content;
        }

        public bool LoadDir(string path, Action<Directory, string> action)
        {
            var dir = new Directory();

            if (dir.Open($"res://{path}") != Error.Ok)
            {
                System.Console.WriteLine($"Failed to open {path}");
                return false;
            }

            dir.ListDirBegin(true);
            var fileName = dir.GetNext();
            
            while (fileName != "")
            {
                action(dir, fileName);
                fileName = dir.GetNext();
            }

            dir.ListDirEnd();
            return true;
        }
    }
}