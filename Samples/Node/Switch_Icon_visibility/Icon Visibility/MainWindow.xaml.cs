using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Layout;
using Syncfusion.UI.Xaml.Diagram.Serializer;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using Syncfusion.UI.Xaml.Diagram.Theming;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Simple_SfDiagram_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Diagram.Nodes = new NodeCollection();
            NodeViewModel node = new NodeViewModel()
            {
                UnitHeight = 50,
                UnitWidth = 200,
                OffsetX = 200,
                OffsetY = 200,
                Content = new CustomContent() { ShowLock = Visibility.Visible },
            };
            
            (Diagram.Nodes as NodeCollection).Add(node);
        }

        private void collapse_Click(object sender, RoutedEventArgs e)
        {
            (((Diagram.Nodes as NodeCollection).ElementAt(0) as NodeViewModel).Content as CustomContent).ShowLock = Visibility.Collapsed;
        }
    }

    public class CustomContent : INotifyPropertyChanged
    {
        private Visibility _showlock = Visibility.Visible;

        public Visibility ShowLock
        {
            get 
            { 
                return _showlock; 
            }
            set 
            { 
                _showlock = value;
                OnPropertyChanged("ShowLock");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
