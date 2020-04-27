using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
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
        public MainWindow()
        {
            InitializeComponent();
            ObservableCollection<NodeViewModel>  nodes = new ObservableCollection<NodeViewModel>();
            AnnotationEditorViewModel a2 = new AnnotationEditorViewModel()
            {
                Content = "hi",
                UnitHeight = 60,
                UnitWidth = 60,
                Constraints = AnnotationConstraints.Default,
            };
            ObservableCollection<ConnectorViewModel> connectors = new ObservableCollection<ConnectorViewModel>();
            NodeViewModel n2 = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 100,
                OffsetX = 100,
                OffsetY = 100,
                Shape = new RectangleGeometry() { Rect = new Rect(0, 0, 10, 10) },
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
            NodeViewModel n3 = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 100,
                OffsetX = 300,
                OffsetY = 300,
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
    }
}