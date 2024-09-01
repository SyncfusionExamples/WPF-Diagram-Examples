using Microsoft.Win32;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Theming;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
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

namespace WpfApp7
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
            diagram.Theme = new OfficeTheme();

            (diagram.Info as IGraphInfo).MenuItemClickedEvent += MainWindow_MenuItemClickedEvent;

            NodeViewModel node = new NodeViewModel()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                MinHeight = 50,
                MinWidth = 50,
                OffsetX = 200,
                OffsetY = 200,
                Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) },
                Constraints = NodeConstraints.Default | NodeConstraints.AspectRatio,
            };

            NodeViewModel node1 = new NodeViewModel()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                MinHeight = 50,
                MinWidth = 50,
                OffsetX = 400,
                Constraints = NodeConstraints.Default | NodeConstraints.AspectRatio,
                OffsetY = 400,
                Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) },
            };

            CustomDiagramMenuItem mi = new CustomDiagramMenuItem()
            {
                Content = "Cut",
                Command = (diagram.Info as IGraphInfo).Commands.Cut,
                Icon = @"pack://application:,,,/Syncfusion.SfDiagram.wpf;component/Resources/Cut.png",
                IsEnabled = true,
            };
            CustomDiagramMenuItem mi1 = new CustomDiagramMenuItem()
            {
                Content = "Copy",
                Command = (diagram.Info as IGraphInfo).Commands.Copy,
                IsEnabled = true,
                Icon = @"pack://application:,,,/Syncfusion.SfDiagram.wpf;component/Resources/Copy.png",
            };
            CustomDiagramMenuItem mi2 = new CustomDiagramMenuItem()
            {
                Content = "Paste",
                Command = (diagram.Info as IGraphInfo).Commands.Paste,
                Icon = @"pack://application:,,,/Syncfusion.SfDiagram.wpf;component/Resources/Paste.png",
                IsEnabled = false,
            };
            CustomDiagramMenuItem mi3 = new CustomDiagramMenuItem()
            {
                Content = "Select All",
                Command = (diagram.Info as IGraphInfo).Commands.SelectAll,
                IsEnabled = true,
                Icon = @"pack://application:,,,/Syncfusion.SfDiagram.wpf;component/Resources/Select-all.png",
            };

            diagram.Menu = new DiagramMenu();
            (diagram.Menu as DiagramMenu).MenuItems = new ObservableCollection<DiagramMenuItem>();

            diagram.Menu.MenuItems.Add(mi);
            diagram.Menu.MenuItems.Add(mi1);
            diagram.Menu.MenuItems.Add(mi2);
            diagram.Menu.MenuItems.Add(mi3);

            (diagram.Nodes as NodeCollection).Add(node);
            (diagram.Nodes as NodeCollection).Add(node1);
        }

        private void MainWindow_MenuItemClickedEvent(object sender, MenuItemClickedEventArgs args)
        {
            if(args.Item.Content.ToString() == "Copy" || args.Item.Content.ToString() == "Cut")
            {
                (diagram.Menu.MenuItems[2] as CustomDiagramMenuItem).IsEnabled = true;
            }
        }
    }

    public class CustomDiagramMenuItem : DiagramMenuItem
    {
        private bool _IsEnabled = false;

        public bool IsEnabled
        {
            get
            {
                return _IsEnabled;
            }
            set
            {
                if(value != _IsEnabled)
                {
                    _IsEnabled = value;
                    OnPropertyChanged("IsEnabled");
                }
            }
        }
    }

    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return (visibility == Visibility.Visible);
        }
    }
}
