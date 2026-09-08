//using GroupSnapping.ViewModel;
using Syncfusion.UI.Xaml.Diagram;
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

namespace GroupSnapping
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            diagram.Nodes = new NodeCollection();
            diagram.Connectors = new ConnectorCollection();
            diagram.Groups = new GroupCollection();

            ObservableCollection<GroupViewModel> groups = new ObservableCollection<GroupViewModel>();
            NodeViewModel node = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 100,
                OffsetX = 100,
                OffsetY = 100,
                Shape = new RectangleGeometry() { Rect = new Rect(0, 0, 10, 10) },
                //ShapeStyle = App.Current.Resources["shapestyle"] as Style
            };
            (node.Ports as PortCollection).Add(new NodePortViewModel());
            NodeViewModel node1 = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 100,
                OffsetX = 200,
                OffsetY = 200,
                Shape = new RectangleGeometry() { Rect = new Rect(0, 0, 10, 10) },
                //ShapeStyle = App.Current.Resources["shapestyle"] as Style
            };

           
            GroupViewModel group = new GroupViewModel()
            {
                Nodes = new ObservableCollection<NodeViewModel>()
                {
                    node,
                    node1
                },
            };

            groups.Add(group);



            NodeViewModel node2 = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 100,
                OffsetX = 500,
                OffsetY = 500,
                Shape = new RectangleGeometry() { Rect = new Rect(0, 0, 10, 10) },
                //ShapeStyle = App.Current.Resources["shapestyle"] as Style
            };
            (node2.Ports as PortCollection).Add(new NodePortViewModel());
            NodeViewModel node3 = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 100,
                OffsetX = 600,
                OffsetY = 600,
                Shape = new RectangleGeometry() { Rect = new Rect(0, 0, 10, 10) },
                //ShapeStyle = App.Current.Resources["shapestyle"] as Style
            };

           
            GroupViewModel group1 = new GroupViewModel()
            {
                Nodes = new ObservableCollection<NodeViewModel>()
                {
                    node2,
                    node3
                },
            };

            groups.Add(group1);
            diagram.Groups = groups;

            NodeViewModel nodeviewmodel = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 100,
                OffsetX = 600,
                OffsetY = 350,
                Shape = new RectangleGeometry() { Rect = new Rect(0, 0, 10, 10) },
                //ShapeStyle = App.Current.Resources["shapestyle"] as Style
            };
            (nodeviewmodel.Ports as PortCollection).Add(new NodePortViewModel());
            (diagram.Nodes as NodeCollection).Add(nodeviewmodel);
        }
    }

    /// <summary>
    /// Represents the calss to convert soid color to Color value.
    /// </summary>
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                var converter = new System.Windows.Media.BrushConverter();

                Brush brush = (Brush)converter.ConvertFromString(value.ToString());

                return (brush as SolidColorBrush).Color;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                var converter = new System.Windows.Media.BrushConverter();
                var brush = (Brush)converter.ConvertFromString(value.ToString());
                return brush;
            }
            return value;
        }
    }
}
