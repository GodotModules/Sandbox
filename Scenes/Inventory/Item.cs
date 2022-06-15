namespace GodotModules 
{
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