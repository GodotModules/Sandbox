namespace GodotModules
{
    public class InvItemContainer : Control
    {
        public Item Item { get; private set; }
        public Pos Pos { get; set; }

        private Inventory _inventory { get; set; }

        public void Setup(Inventory inventory)
        {
            _inventory = inventory;
        }

        public void SetItem(string name)
        {
            Item = new Item(name);
            Item.InitSprite(RectSize);
            AddChild(Item.Sprite);
        }

        public Item CloneItem() => Item.Clone(RectSize);

        public void RemoveItem()
        {
            Item?.Sprite.QueueFree();
            Item = null;
        }

        private void _on_InvItemContainer_gui_input(InputEvent @event)
        {
            if (@event is not InputEventMouseButton mouseButton || mouseButton.ButtonIndex != (int)ButtonList.Left || !mouseButton.Pressed)
                return;

            // left click

            if (Item == null) // There is no item here
            {
                if (SceneInventory.CursorItem != null) // There is nothing in this inv slot and we are holding something in our hand
                {
                    // take the item from the cursor and put it in the inventory slot
                    var item = SceneInventory.CursorItem.Clone(RectSize);
                    Item = item;
                    item.SetParent(this);

                    // remove the item from the cursor
                    SceneInventory.CursorItem.Sprite.QueueFree();
                    SceneInventory.CursorItem = null;
                    return;
                }

                // does not have any item
                return;
            }

            if (Item != null) // There is an item here
            {
                if (SceneInventory.CursorItem != null) // There is an item in this inventory slot and an item on the cursor
                {
                    // Get clone of current item and remove current item
                    var curItem = CloneItem();
                    RemoveItem();

                    // put the item from the cursor to this inventory slot
                    var cursorItem = SceneInventory.CursorItem.Clone(RectSize);
                    Item = cursorItem;
                    cursorItem.SetParent(this);

                    // put clone of current item to cursor
                    SceneInventory.CursorItem.Sprite.QueueFree();
                    SceneInventory.CursorItem = curItem;
                    curItem.SetParent(SceneInventory.CursorItemParent);
                    return;
                }

                // there is no item attached to the cursor right now
                // put the item on the cursor
                var item = CloneItem();
                SceneInventory.CursorItem = item;
                item.SetParent(SceneInventory.CursorItemParent);

                // keep track of the position we clicked on
                SceneInventory.PickedPos = Pos;

                // remove the item from the inv slot
                RemoveItem();
                return;
            }
        }
    }
}
