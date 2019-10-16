using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace ColorsHelper
{
    public class FontsViewModel : INotifyPropertyChanged
    {
        private int fontSize = 11;

        public event PropertyChangedEventHandler PropertyChanged;

        public int FontSize
        {
            get { return fontSize; }
            private set { fontSize = Math.Max(1, value); PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontSize))); }
        }

        public ICommand SmallerCommand => EditingCommands.DecreaseFontSize;
        public ICommand BiggerCommand => EditingCommands.IncreaseFontSize;

        CommandBinding commandBindingSmaller, commandBindingBigger;

        public FontsViewModel()
        {
            commandBindingSmaller = new CommandBinding(SmallerCommand, (s, e) => FontSize -= 1, (s, e) => e.CanExecute = true);
            commandBindingBigger = new CommandBinding(BiggerCommand, (s, e) => FontSize += 1, (s, e) => e.CanExecute = true);
            commandBindingSmaller.Executed += (s, e) => FontSize -= 1;
            commandBindingSmaller.PreviewExecuted += (s, e) => FontSize -= 1;
        }
    }
}
