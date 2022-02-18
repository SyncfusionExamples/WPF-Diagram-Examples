using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Theming;
using System;
using System.Collections.Generic;
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

namespace Simple_WPF_Diagram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            //Diagram.Theme = new OfficeTheme();

            Diagram.Nodes = new NodeCollection();

            NodeViewModel node = new NodeViewModel()
            {
                OffsetX = 200,
                OffsetY = 200,
                UnitHeight = 100,
                UnitWidth = 100,
                Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) },
                Content = "3",
            };

            NodeViewModel node1 = new NodeViewModel()
            {
                OffsetX = 600,
                OffsetY = 200,
                UnitHeight = 100,
                UnitWidth = 100,
                Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) },
                Content = "7",
            };

            (Diagram.Nodes as NodeCollection).Add(node);
            (Diagram.Nodes as NodeCollection).Add(node1);

            (Diagram.Info as IGraphInfo).ItemUnSelectedEvent += MainWindow_ItemUnSelectedEvent;
        }

        private void MainWindow_ItemUnSelectedEvent(object sender, DiagramEventArgs args)
        {
            MessageBox.Show(((Diagram.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>).Count().ToString());
        }
    }

    public class InheritanceDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NumberTemplate { get; set; }
        public DataTemplate LargeNumberTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            // Null value can be passed by IDE designer
            if (item == null) return null;

            var num = Convert.ToInt32((string)item);

            // Select one of the DataTemplate objects, based on the 
            // value of the selected item in the ComboBox.
            if (num < 5)
            {
                return NumberTemplate;
            }
            else
            {
                return LargeNumberTemplate;
            }
        }
    }
}
