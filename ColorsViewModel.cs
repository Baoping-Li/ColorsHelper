using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace ColorsHelper
{
    public class BrushItem
    {
        public Brush Brush { get; set; }
        public string Key { get; set; }
    }
    public class ColorItem
    {
        public Brush Color { get; set; }
        public string Key { get; set; }
    }
    public class ColorsViewModel
    {
        private List<BrushItem>  _brushes = new List<BrushItem>();
        private List<ColorItem>  _colors = new List<ColorItem>();

        public List<BrushItem> Brushes => _brushes;
        public List<ColorItem> Colors => _colors;
        public ColorsViewModel()
        {
            foreach (var prop in typeof(SystemColors).GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public))
            {
                if (typeof(Brush).IsAssignableFrom(prop.PropertyType))
                    _brushes.Add(new BrushItem() { Brush = prop.GetValue(null) as Brush, Key = prop.Name });
                if (prop.PropertyType == typeof(Color))
                    _colors.Add(new ColorItem() { Color = new SolidColorBrush((Color)prop.GetValue(null)), Key = prop.Name });
            }
        }
    }
}
