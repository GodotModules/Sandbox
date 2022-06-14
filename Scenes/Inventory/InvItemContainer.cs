namespace GodotModules
{
    public class InvItemContainer : PanelContainer
    {
        private Sprite _itemSprite;
        private bool _hasItem;

        public void AddItem(string name)
        {
            InitSprite(name);
        }

        private void InitSprite(string name)
        {
            var texture = Items.Sprites[name];

            var sprite = new Sprite();

            var itemSize = RectSize - new Vector2(10, 10);

            sprite.Texture = texture;
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
                InitSprite("Item");
                return;
            }

            if (_hasItem) 
            {
                // change parent of item sprite to cursor item
                _itemSprite.GetParent().RemoveChild(_itemSprite);
                SceneInventory.CursorItem.AddChild(_itemSprite);

                _hasItem = false;
            }
        }
    }
}
