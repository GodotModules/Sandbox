namespace GodotModules
{
    public class Inventory : Node
    {
        [Export] protected readonly NodePath NodePathGridContainer;

        private int _rows, _columns;
        private Dictionary<Pos, InvItemContainer> _items = new();

        public void Setup(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
        }

        public override void _Ready()
        {
            var gridContainer = GetNode<GridContainer>(NodePathGridContainer);
            gridContainer.Columns = _columns;

            for (int r = 0; r < _rows; r++)
            {
                for (int c = 0; c < _columns; c++)
                {
                    var invItemContainer = Prefabs.UIInvItemContainer.Instance<InvItemContainer>();

                    _items.Add(new Pos(r, c), invItemContainer);

                    gridContainer.AddChild(invItemContainer);
                }
            }

            _items[new Pos(_rows - 1, _columns - 1)].AddItem("Item");
        }
    }

    public struct Pos 
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Pos(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
