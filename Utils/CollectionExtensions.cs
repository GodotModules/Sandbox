namespace GodotModules 
{
    public static class CollectionExtensions 
    {
        public static string PrintFull(this object v) => Newtonsoft.Json.JsonConvert.SerializeObject(v, Newtonsoft.Json.Formatting.Indented);
    }
}