namespace GodotModules 
{
    public static class Prefabs 
    {
        public readonly static PackedScene UIInventory = Load("Inventory/Inventory");
        public readonly static PackedScene UIInvItemContainer = Load("Inventory/InvItemContainer");
        public readonly static PackedScene UIHotkey = Load("Hotkeys/UIHotkey");

        private static PackedScene Load(string prefab) => ResourceLoader.Load<PackedScene>($"res://Scenes/{prefab}.tscn");
    }
}
