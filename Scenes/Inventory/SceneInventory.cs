namespace GodotModules
{
    public class SceneInventory : Control
    {
        [Export] protected readonly NodePath NodePathCursorItem;
        [Export] protected readonly NodePath NodePathCanvasLayer;

        public static Node2D CursorItemParent { get; private set; }
        public static Item CursorItem { get; set; }
        private CanvasLayer _canvasLayer;

        public override void _Ready()
        {
            _canvasLayer = GetNode<CanvasLayer>(NodePathCanvasLayer);
            CursorItemParent = GetNode<Node2D>(NodePathCursorItem);

            Items.Init();

            var inventory = Prefabs.UIInventory.Instance<Inventory>();
            inventory.Setup(5, 5);
            _canvasLayer.AddChild(inventory);
            inventory.SetItem(5, 5, "Item1");
            inventory.SetItem(4, 4, "Item2");
        }

        public override void _PhysicsProcess(float delta)
        {
            var offset = new Vector2(10, 10);
            CursorItemParent.Position = GetGlobalMousePosition() + offset;
        }
    }
}
