namespace StyleLibrary
{
    using System.Collections.Generic;
    using System.Linq;

    public class Item
    {
        public static ICollection<Item> Data { get; } = Enumerable.Range(1, 5).Select(i => new Item { Key = "Key" + i, Value = "Value" + i }).ToArray();

        public string Key { get; set; }

        public string Value { get; set; }
    }
}
