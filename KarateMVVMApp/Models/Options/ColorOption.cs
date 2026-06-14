namespace KarateMVVMApp.Models.Options
{
    public class ColorOption
    {
        public string Name { get; set; }
        public string HexValue { get; set; }

        public ColorOption(string name, string hexValue)
        {
            Name = name;
            HexValue = hexValue;
        }
    }
}
