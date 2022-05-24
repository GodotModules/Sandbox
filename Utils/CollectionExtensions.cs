namespace GodotModules 
{
    public static class CollectionExtensions 
    {
        public static string PrintFull(this object v) => Newtonsoft.Json.JsonConvert.SerializeObject(v, Newtonsoft.Json.Formatting.Indented);

        public static void ForEach<T>(this IEnumerable<T> value, Action<T> action)
        {
            foreach (var element in value)
                action(element);
        }
    }
}