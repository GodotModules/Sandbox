namespace GodotModules
{
    public class InvItemContainer : PanelContainer
    {
        public Item Item { get; private set; }
        public Pos Pos { get; set; }
        
        private Sprite _itemSprite;
        private bool _hasItem;

        public void SetItem(string name)
        {
            Item = new Item(name);
            InitSprite(name);
        }

        private void InitSprite(string name)
        {
            var sprite = new Sprite();

            var itemSize = RectSize - new Vector2(10, 10);

            sprite.Texture = Items.Sprites[name];
            sprite.Position = RectSize / 2; // inv size
            sprite.Scale = itemSize / sprite.Texture.GetSize(); // item size

            AddChild(sprite);
            _itemSprite = sprite;
            _hasItem = true;
        }

        private void _on_InvItemContainer_gui_input(InputEvent @event)
        {
            if (@event is not InputEventMouseButton mouseButton || mouseButton.ButtonIndex != (int)ButtonList.Left || !mouseButton.Pressed)
                return;

            // left click
            if (!_hasItem)
            {
                InitSprite("Item2");
                return;
            }

            if (_hasItem) 
            {
                GD.Print(Pos);
                // change parent of item sprite to cursor item
                _itemSprite.GetParent().RemoveChild(_itemSprite);
                SceneInventory.CursorItem.AddChild(_itemSprite);

                _hasItem = false;
            }
        }
    }

    public class Item 
    {
        public string Name { get; set; }
        public int Count { get; set; }

        //private Sprite _sprite;

        public Item(string name, int count = 1)
        {
            Name = name;
            Count = count;
        }

        /*public void SetParent(Node parent)
        {
            _sprite.GetParent()?.RemoveChild(_sprite);
            parent.AddChild(_sprite);
        }*/

        public Item Clone(Item item)
        {
            return new Item(Name, Count);
        }
    }
}
