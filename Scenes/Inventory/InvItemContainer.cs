namespace GodotModules
{
    public class InvItemContainer : PanelContainer
    {
        public Item Item { get; private set; }
        public Pos Pos { get; set; }
        
        private bool _hasItem;

        public void SetItem(string name)
        {
            Item = new Item(name);
            Item.InitSprite(RectSize);
            AddChild(Item.Sprite);
            _hasItem = true;
        }

        private void _on_InvItemContainer_gui_input(InputEvent @event)
        {
            if (@event is not InputEventMouseButton mouseButton || mouseButton.ButtonIndex != (int)ButtonList.Left || !mouseButton.Pressed)
                return;

            // left click
            if (!_hasItem)
            {
                // need to also check if there is an item in the cursor at this time

                

                // does not have any item
                return;
            }

            if (_hasItem) 
            {
                // need to also check if there is an item in the cursor at this time
                
                // change parent of item sprite to cursor item
                Item.SetParent(SceneInventory.CursorItemParent);
                SceneInventory.CursorItem = Item;
                _hasItem = false;
            }
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public Sprite Sprite { get; set; }

        public Item(string name, int count = 1)
        {
            Name = name;
            Count = count;
        }

        public void InitSprite(Vector2 containerSize)
        {
            Sprite = new Sprite();
            Sprite.Texture = Items.Sprites[Name];
            Sprite.Position = containerSize / 2; // inv size
            Sprite.Scale = (containerSize - new Vector2(10, 10)) / Sprite.Texture.GetSize(); // item size
        }

        public void SetParent(Node parent)
        {
            Sprite.GetParent()?.RemoveChild(Sprite);
            parent.AddChild(Sprite);
        }

        public Item Clone()
        {
            var newItem = new Item(Name, Count);
            newItem.Sprite = Sprite;
            return newItem;
        }
    }
}
