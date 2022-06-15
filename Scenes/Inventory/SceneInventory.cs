namespace GodotModules
{
    public class SceneInventory : Control
    {
        [Export] protected readonly NodePath NodePathCursorItem;
        [Export] protected readonly NodePath NodePathCanvasLayer;

        public static Item CursorIte { get; private set; }
        public static Node2D CursorItem { get; private set; }
        private CanvasLayer _canvasLayer;

        public override void _Ready()
        {
            _canvasLayer = GetNode<CanvasLayer>(NodePathCanvasLayer);
            CursorItem = GetNode<Node2D>(NodePathCursorItem);

            Items.Init();

            var inventory = Prefabs.UIInventory.Instance<Inventory>();
            inventory.Setup(5, 5);
            _canvasLayer.AddChild(inventory);
        }

        public override void _PhysicsProcess(float delta)
        {
            var offset = new Vector2(10, 10);
            CursorItem.Position = GetGlobalMousePosition() + offset;
        }
    }
}
