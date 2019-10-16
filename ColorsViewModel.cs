using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
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
    public class ColorsViewModel : INotifyPropertyChanged
    {
        private byte _colorR = 0x33;
        private byte _colorG = 0x99;
        private byte _colorB = 0xff;
        private bool _expanded;
        private bool _colorEnabled;
        private ICommand _colorsVisibilityCommand;
        private GridLength _colorRowHeight = GridLength.Auto;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ColorsVisibilityCommand => _colorsVisibilityCommand ?? (_colorsVisibilityCommand = new BasicCommand(switchVisibility));
        private void switchVisibility()
        {
            ColorsEnabled = !ColorsEnabled;
        }

        public bool ColorsEnabled
        {
            get => _colorEnabled;
            set
            {
                _colorEnabled = value;
                OnPropertyChanged(nameof(SplitterVisibility));
                OnPropertyChanged(nameof(ColorRowHeight));
                OnPropertyChanged(nameof(MaxRowHeight));
            }
        }

        public Visibility SplitterVisibility => (_colorEnabled && _expanded)? Visibility.Visible : Visibility.Collapsed;

        public int MaxRowHeight => !_colorEnabled ? 0 : int.MaxValue;

        public bool IsExpanded
        {
            get => _colorEnabled && _expanded;
            set
            {
                _expanded = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SplitterVisibility));
                OnPropertyChanged(nameof(ColorRowHeight));
                OnPropertyChanged(nameof(MaxRowHeight));
            }
        }

        public GridLength ColorRowHeight
        {
            get
            {
                if (!_colorEnabled)
                    return new GridLength(0);
                if (!_expanded)
                    return GridLength.Auto;
                return _colorRowHeight;
            }
            set
            {
                _colorRowHeight = value;
                OnPropertyChanged();
            }
        }

        public List<BrushItem> Brushes { get; } = new List<BrushItem>();
        public List<ColorItem> Colors { get; } = new List<ColorItem>();

        public byte colorR { get { return _colorR; } set { _colorR = value; OnPropertyChanged(); OnPropertyChanged(nameof(RGBColor)); } }
        public byte colorG { get { return _colorG; } set { _colorG = value; OnPropertyChanged(); OnPropertyChanged(nameof(RGBColor)); } }
        public byte colorB { get { return _colorB; } set { _colorB = value; OnPropertyChanged(); OnPropertyChanged(nameof(RGBColor)); } }

        public SolidColorBrush RGBColor => new SolidColorBrush(Color.FromRgb(_colorR, _colorG, _colorB));

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ColorsViewModel()
        {
            foreach (var prop in typeof(SystemColors).GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public))
            {
                if (typeof(Brush).IsAssignableFrom(prop.PropertyType))
                    Brushes.Add(new BrushItem() { Brush = prop.GetValue(null) as Brush, Key = prop.Name });
                if (prop.PropertyType == typeof(Color))
                    Colors.Add(new ColorItem() { Color = new SolidColorBrush((Color)prop.GetValue(null)), Key = prop.Name });
            }
        }
    }
}
