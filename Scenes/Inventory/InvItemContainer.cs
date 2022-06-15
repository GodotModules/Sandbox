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
                // need to also check if there is an item in the cursor at this time

                if (SceneInventory.CursorItem != null)
                {
                    SceneInventory.CursorItem.SetParent(this);
                    Item = SceneInventory.CursorItem.Clone();

                    SceneInventory.CursorItem = null;
                }

                // does not have any item
                return;
            }

            if (Item != null) 
            {
                // need to also check if there is an item in the cursor at this time
                
                // change parent of item sprite to cursor item
                Item.SetParent(SceneInventory.CursorItemParent);
                SceneInventory.CursorItem = Item;
            }
        }
    }
}
