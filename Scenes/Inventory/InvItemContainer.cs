namespace GodotModules
{
    public class InvItemContainer : PanelContainer
    {
        public Item Item { get; private set; }
        public Pos Pos { get; set; }

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

            if (Item == null)
            {
                if (SceneInventory.CursorItem != null)
                {
                    // There is nothing in this inv slot and we are holding something in our hand

                    // take the item from the cursor and put it in the inventory slot
                    var item = SceneInventory.CursorItem.Clone(RectSize);
                    Item = item;
                    item.SetParent(this);

                    // remove the item from the cursor
                    SceneInventory.CursorItem.Sprite.QueueFree();
                    SceneInventory.CursorItem = null;
                }

                // does not have any item
                return;
            }

            if (Item != null) 
            {
                // need to also check if there is an item in the cursor at this time

                // put the item on the cursor
                var item = Item.Clone(RectSize);
                SceneInventory.CursorItem = item;
                item.SetParent(SceneInventory.CursorItemParent);

                // remove the item from the inv slot
                Item.Sprite.QueueFree();
                Item = null;
            }
        }
    }
}
