namespace GodotModules
{
    public class InvItemContainer : PanelContainer
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
                    // EXPLANATION:
                    // Lets say we have green item on cursor
                    // and there is a red item for this inventory slot
                    
                    // The red item should go back to where we previously picked up the other item
                    // and the green item should be dropped onto this inventory slot
                    var prevItem = Item.Clone(RectSize);
                    _inventory.ItemContainers[SceneInventory.PickedPos].Item = prevItem;
                    prevItem.SetParent(_inventory.ItemContainers[SceneInventory.PickedPos]);

                    // Remove this old red item since the clone of the red item was moved to the previous slot
                    Item.Sprite.QueueFree();
                    Item = null;

                    // put the item from the cursor to this inventory slot
                    var cursorItem = SceneInventory.CursorItem.Clone(RectSize);
                    Item = cursorItem;
                    cursorItem.SetParent(this);

                    // remove the item from the cursor
                    SceneInventory.CursorItem.Sprite.QueueFree();
                    SceneInventory.CursorItem = null;
                    return;
                }

                // put the item on the cursor
                var item = Item.Clone(RectSize);
                SceneInventory.CursorItem = item;
                item.SetParent(SceneInventory.CursorItemParent);

                // keep track of the position we clicked on
                SceneInventory.PickedPos = Pos;

                // remove the item from the inv slot
                Item.Sprite.QueueFree();
                Item = null;
                return;
            }
        }
    }
}
