using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram;
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
using System.Collections.ObjectModel;
using System.IO.Packaging;

namespace Zoomtest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Diagram.DefaultConnectorType = ConnectorType.Line;
            Diagram.HorizontalRuler = new Ruler();
            Diagram.VerticalRuler = new Ruler()
            {
                Orientation = Orientation.Vertical,
            };

            

            Diagram.Nodes = new NodeCollection();
            Diagram.Connectors = new ConnectorCollection();

            NodeViewModel node = new NodeViewModel()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 150,
                OffsetY = 150,
                Shape = this.Resources["Rectangle"],
                ID = "node",
                Annotations = new ObservableCollection<IAnnotation>
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = "Node",
                    }
                }
            };
            (Diagram.Nodes as NodeCollection).Add(node);
            NodeViewModel node1 = new NodeViewModel()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 500,
                OffsetY = 100,
                Shape = this.Resources["Rectangle"],
                ID = "node",
                Annotations = new ObservableCollection<IAnnotation>
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = "Node1",
                    }
                }
            };
            (Diagram.Nodes as NodeCollection).Add(node1);
            NodeViewModel node2 = new NodeViewModel()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 250,
                OffsetY = 300,
                Shape = this.Resources["Rectangle"],
                ID = "node",
                Annotations = new ObservableCollection<IAnnotation>
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = "Node2",
                    }
                }
            };
            (Diagram.Nodes as NodeCollection).Add(node2);

            NodeViewModel node3 = new NodeViewModel()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 500,
                OffsetY = 500,
                ID = "node1",
                Shape = this.Resources["Rectangle"],
                Annotations = new ObservableCollection<IAnnotation>
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = "Node3"
                    }
                }
            };
            (Diagram.Nodes as NodeCollection).Add(node3);

            ConnectorViewModel connector1 = new ConnectorViewModel()
            {
                SourceNode = node,
                TargetNode = node1,
            };
            (Diagram.Connectors as ConnectorCollection).Add(connector1);


            NodeViewModel node4 = new NodeViewModel()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 700,
                OffsetY = 400,
                Shape = this.Resources["Rectangle"],
                ID = "node",
                Annotations = new ObservableCollection<IAnnotation>
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = "Node4",
                    }
                }
            };
            (Diagram.Nodes as NodeCollection).Add(node4);
            NodeViewModel node5 = new NodeViewModel()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 700,
                OffsetY = 700,
                Shape = this.Resources["Rectangle"],
                ID = "node",
                Annotations = new ObservableCollection<IAnnotation>
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = "Node5",
                    }
                }
            };
            (Diagram.Nodes as NodeCollection).Add(node5);
            ConnectorViewModel connector2 = new ConnectorViewModel()
            {
                SourceNode = node3,
                TargetNode = node4,
            };
            (Diagram.Connectors as ConnectorCollection).Add(connector2);
            ConnectorViewModel connector3 = new ConnectorViewModel()
            {
                SourceNode = node3,
                TargetNode = node5,
            };
            (Diagram.Connectors as ConnectorCollection).Add(connector3);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Diagram.SelectedItems is ISelector && Diagram.SelectedItems != null)
            {
                IGraphInfo graphinfo = Diagram.Info as IGraphInfo;
                var SelectedItemBounds = ((Diagram.SelectedItems as SelectorViewModel).Info as ISelectorInfo).Bounds;
                graphinfo.Commands.FitToPage.Execute(
                new FitToPageParameter()
                {
                    Region = Region.Custom,
                    Margin = new Thickness(50),
                    FocusArea = new Rect(SelectedItemBounds.X, SelectedItemBounds.Y, SelectedItemBounds.Width, SelectedItemBounds.Height),
                    FitToPage = FitToPage.FitToPage

                });
            }
        }
    }
}
