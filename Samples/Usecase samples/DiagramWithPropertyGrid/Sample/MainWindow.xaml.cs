using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace tesing_node
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DelegateCommand GetDrawTypeCommand { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            ObservableCollection<CustomNodeViewModel> nodes = new ObservableCollection<CustomNodeViewModel>();
            AnnotationEditorViewModel a2 = new AnnotationEditorViewModel()
            {
                Content = "hi",
                UnitHeight = 60,
                UnitWidth = 60,
                Constraints = AnnotationConstraints.Default,
            };
            ObservableCollection<ConnectorViewModel> connectors = new ObservableCollection<ConnectorViewModel>();
            CustomNodeViewModel n2 = new CustomNodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 100,
                OffsetX = 100,
                OffsetY = 100,
                Shape = new RectangleGeometry() { Rect = new Rect(0, 0, 10, 10) },
                FillColor = new SolidColorBrush(Colors.SkyBlue),
                StrokeColor = new SolidColorBrush(Colors.Green),
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    a2,
                },
            };
            AnnotationEditorViewModel a3 = new AnnotationEditorViewModel()
            {
                Content = "hello",
                UnitHeight = 60,
                UnitWidth = 60,
                Constraints = AnnotationConstraints.Default,
            };
            CustomNodeViewModel n3 = new CustomNodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 100,
                OffsetX = 300,
                OffsetY = 300,
                FillColor = new SolidColorBrush(Colors.SkyBlue),
                StrokeColor = new SolidColorBrush(Colors.Green),
                Shape = new RectangleGeometry() { Rect = new Rect(0, 0, 10, 10) },
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    a3,
                },
            };
            ConnectorViewModel c1 = new ConnectorViewModel()
            {
                SourceNode = n2,
                TargetNode = n3,
            };
            nodes.Add(n2);
            nodes.Add(n3);
            diag.Nodes = nodes;
            connectors.Add(c1);
            diag.Connectors = connectors;
            (diag.Info as IGraphInfo).ItemSelectedEvent += MainWindow_ItemSelectedEvent;
            (diag.Info as IGraphInfo).AnnotationChanged += MainWindow_AnnotationChanged;
            (diag.Info as IGraphInfo).ItemAdded += MainWindow_ItemAdded;
        }

        private void MainWindow_ItemAdded(object sender, ItemAddedEventArgs args)
        {
            if (args.Item is CustomNodeViewModel)
            {
                (args.Item as CustomNodeViewModel).FillColor = new SolidColorBrush(Colors.Red);
                (args.Item as CustomNodeViewModel).StrokeColor = new SolidColorBrush(Colors.Black);
            }
        }

        private void MainWindow_AnnotationChanged(object sender, ChangeEventArgs<object, AnnotationChangedEventArgs> args)
        {
            if (args.Item is IAnnotation)
            {
                propertyGrid1.SelectedObject = args.Item;
            }
        }

        void MainWindow_ItemSelectedEvent(object sender, DiagramEventArgs args)
        {
            if (args.Item is INode)
            {
                propertyGrid1.SelectedObject = args.Item;
            }
            else if (args.Item is IConnector)
            {
                propertyGrid1.SelectedObject = args.Item;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (diag.Tool == Tool.MultipleSelect)
            {
                diag.Tool |= Tool.DrawOnce;
            }
            diag.DefaultConnectorType = ConnectorType.Line;
            diag.DrawingTool = DrawingTool.Rectangle;

        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (diag.Tool == Tool.MultipleSelect)
            {
                diag.Tool |= Tool.DrawOnce;
            }
            diag.DefaultConnectorType = ConnectorType.Line;
            diag.DrawingTool = DrawingTool.Connector;
        }
    }

    public class CustomNodeViewModel : NodeViewModel
    {
        private Brush fill;

        private Brush stroke;


        // Fill property
        public Brush FillColor
        {
            get { return fill; }
            set
            {
                fill = value;
                OnPropertyChanged("FillColor");
            }
        }

        // Stroke property
        public Brush StrokeColor
        {
            get { return stroke; }
            set
            {
                stroke = value;
                OnPropertyChanged("StrokeColor");
            }
        }
    }
}