using System.Collections.ObjectModel;
using System.Windows;

namespace FlowchartCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class SymbolCollection : ObservableCollection<object>
    {
    }
}
