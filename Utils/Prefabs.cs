namespace GodotModules 
{
    public static class Prefabs 
    {
        public readonly static PackedScene UIHotkey = Load("Hotkeys/UIHotkey");

        private static PackedScene Load(string prefab) => ResourceLoader.Load<PackedScene>($"res://Scenes/{prefab}.tscn");
    }
}
