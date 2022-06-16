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
            Sprite = CreateSprite(containerSize);
        }

        public void SetParent(Node parent)
        {
            Sprite.GetParent()?.RemoveChild(Sprite);
            parent.AddChild(Sprite);
        }

        public Item Clone(Vector2 containerSize)
        {
            var newItem = new Item(Name, Count);
            newItem.Sprite = CreateSprite(containerSize);
            return newItem;
        }

        private Sprite CreateSprite(Vector2 containerSize)
        {
            var sprite = new Sprite();
            sprite.Texture = Items.Sprites[Name];
            sprite.Position = containerSize / 2; // inv size
            sprite.Scale = (containerSize - new Vector2(10, 10)) / sprite.Texture.GetSize(); // item size
            return sprite;
        }
    }
}