namespace GodotModules
{
    public class Inventory : Node
    {
        [Export] protected readonly NodePath NodePathGridContainer;

        private int _rows, _columns;
        private Dictionary<Pos, InvItemContainer> _itemContainers = new();

        public void Setup(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
        }

        public override void _Ready()
        {
            var gridContainer = GetNode<GridContainer>(NodePathGridContainer);
            gridContainer.Columns = _columns;

            for (int r = 1; r <= _rows; r++)
            {
                for (int c = 1; c <= _columns; c++)
                {
                    var invItemContainer = Prefabs.UIInvItemContainer.Instance<InvItemContainer>();
                    invItemContainer.Pos = new Pos(r, c);

                    _itemContainers.Add(new Pos(r, c), invItemContainer);

                    gridContainer.AddChild(invItemContainer);
                }
            }

            _itemContainers[new Pos(_rows, _columns)].SetItem("Item1");
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

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var other = (Pos)obj;

            if (other.X == X && other.Y == Y)
                return true;

            return false;
        }

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => $"X: {X}, Y: {Y}";
    }
}
