using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace AddNewPage_Sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SelectedDiagram _seletedDiagram;
        public SelectedDiagram SelectedDiagram
        {
            get { return _seletedDiagram; }
            set
            {
                _seletedDiagram = value;
                _seletedDiagram.Tool = Tool.ContinuesDraw;
            }
        }

        public ObservableCollection<SelectedDiagram> Diagrams { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Diagrams = new ObservableCollection<SelectedDiagram>();

            SelectedDiagram = new SelectedDiagram() { Name = "DiagramPage" + (Diagrams.Count + 1) };
           
            tab.ItemsSource = Diagrams;
           
            tree.ItemsSource = Diagrams;

            Diagrams.Add(SelectedDiagram);

            tab.SelectedItemChangedEvent += Tab_SelectedItemChangedEvent;
        }

        private void Tab_SelectedItemChangedEvent(object sender, SelectedItemChangedEventArgs e)
        {
            SelectedDiagram = (e.NewSelectedItem as TabItemExt).Content as SelectedDiagram;
            (e.NewSelectedItem as TabItemExt).Header = SelectedDiagram.Name;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Diagrams.Add(new SelectedDiagram() { Name = "DiagramPage" + (Diagrams.Count + 1) });
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Diagrams.Remove(SelectedDiagram);
        }
    }

    public class SelectedDiagram : SfDiagram
    {
        public SelectedDiagram()
        {
            Connectors = new ObservableCollection<ConnectorViewModel>();
        }
    }
}
