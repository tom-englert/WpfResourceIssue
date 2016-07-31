namespace StyleLibrary
{
    using System.Windows;
    using System.Windows.Controls;

    public class Control1 : Control
    {
        static Control1()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Control1), new FrameworkPropertyMetadata(typeof(Control1)));
        }
    }

    public static class ResourceKeys
    {
        public static ResourceKey CorporateColor = new ComponentResourceKey(typeof(ResourceKeys), "CorporateColor");
        public static ResourceKey CorporateStyle = new ComponentResourceKey(typeof(ResourceKeys), "CorporateStyle");
    }
}