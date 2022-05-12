using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Syncfusion.UI.Xaml.Diagram;

namespace SelectedItems_SfDiagram
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

    //To Represent the view model class for SfDiagram to bind SelectedItems property of SfDiagram view to ViewModel.
    public class CustomVM : INotifyPropertyChanged
    {
        public CustomVM()
        {
        }

        private SelectorViewModel selectedItems = new SelectorViewModel()
        {
            Nodes = new ObservableCollection<NodeViewModel>(),
            Connectors = new ObservableCollection<ConnectorViewModel>(),
        };

        /// <summary>
        /// Gets or sets the SelectedItems which is to bind the custom SelectorViewModel to SelectedItems property of SfDiagram view.
        /// </summary>
        public SelectorViewModel SelectedItems
        {
            get { return selectedItems; }
            set
            {
                if (selectedItems != value)
                {
                    selectedItems = value;
                    OnPropertyChanged("SelectedItems");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this,new PropertyChangedEventArgs(name));
            }
        }
    }
}
