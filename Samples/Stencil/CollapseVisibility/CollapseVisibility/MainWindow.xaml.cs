using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace StencilDiagramElements
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int nodeIndex = 0;
        private int connectorIndex = 0;
        List<Point> segmentPoints;
        public MainWindow()
        {
            InitializeComponent();

            diagram.Constraints |= GraphConstraints.Bridging;
            diagram.Loaded += OnDiagramLoaded;


            (diagram.Info as IGraphInfo).ItemAdding += OnItemAdding;
           
            (diagram.Info as IGraphInfo).ItemDropEvent += MainWindow_ItemDropEvent;
            (diagram.Info as IGraphInfo).NodeChangedEvent += MainWindow_NodeChangedEvent;
        }

        
        //Method to update relative position of parent node to child node.
        private void MainWindow_NodeChangedEvent(object sender, ChangeEventArgs<object, NodeChangedEventArgs> args)
        {
            if (args.NewValue.InteractionState == NodeChangedInteractionState.Dragged)
            {
                var parentnode  = args.Item as CustomNodeViewModel;
                if (parentnode.ChildNodes != null && parentnode.ChildNodes.Count > 0)
                {
                    foreach(CustomNodeViewModel childNode in parentnode.ChildNodes as IEnumerable<object>)
                    {
                        childNode.OffsetX = parentnode.OffsetX;
                        childNode.OffsetY = parentnode.OffsetY;
                    }
                }
            }
        }

        //Method to add dropped node as child as intersected node.
        private void MainWindow_ItemDropEvent(object sender, ItemDropEventArgs args)
        {
            if (args.Source is CustomNodeViewModel && !(args.Target is SfDiagram))
            {
                var childNode = args.Source as CustomNodeViewModel;
                foreach (var node in args.Target as IEnumerable<object>)
                {
                    if (node is CustomNodeViewModel)
                    {
                        var parentNode = node as CustomNodeViewModel;
                        //Add dropped node as child of intersected node
                        parentNode.ChildNodes.Add(childNode as CustomNodeViewModel);
                        //Enable selection for parnet and disable selection for child which is collapsed.
                        childNode.NodeVisibility = Visibility.Collapsed;
                        childNode.IsSelected = false;
                        parentNode.IsSelected = true;
                    }
                }
            }
        }

        private void OnDiagramLoaded(object sender, RoutedEventArgs e)
        {
            (diagram.SelectedItems as ISelector).SelectorConstraints = SelectorConstraints.Default & ~(SelectorConstraints.Resizer | SelectorConstraints.Rotator | SelectorConstraints.QuickCommands);
        }

        private void OnItemAdding(object sender, ItemAddingEventArgs args)
        {
            if (args.Item is CustomNodeViewModel node)
            {
                nodeIndex++;
                node.Constraints &= ~NodeConstraints.Connectable;
                node.UnitWidth = 100;
                node.UnitHeight = 100;
                node.Shape = App.Current.MainWindow.Resources["Rectangle"];
                
                node.Annotations = new AnnotationCollection()
                {
                    new AnnotationEditorViewModel() {
                        Content = "Node" + nodeIndex,
                        Offset= new Point(0.5, 0),
                        VerticalAlignment=VerticalAlignment.Top,
                        Margin= new Thickness(0,15,0,0)
                    }
                };
                node.ContentTemplate = null;
                node.Content = null;
            }
        }

       
    }

    public class CustomNodeViewModel: NodeViewModel
    {
        private Visibility nodeVisibility = Visibility.Visible;
        public Visibility NodeVisibility
        {
            get
            {
                return nodeVisibility;
            }
                
            set
            {
                if (nodeVisibility != value)
                {
                    nodeVisibility = value;
                    OnPropertyChanged("NodeVisibility");
                }
            }
        }

        public List<CustomNodeViewModel> ChildNodes = new List<CustomNodeViewModel>();
        public CustomNodeViewModel()
        {
            this.Constraints |= NodeConstraints.AllowDrop;
        }
    }

    public class CustomNodeCollection : ObservableCollection<CustomNodeViewModel>
    {

    }

    public class CustomConnectorViewModel : ConnectorViewModel
    {
        public ConnectorModel Connector { get; set; }
    }

    public class CustomConnectorCollection: ObservableCollection<CustomConnectorViewModel>
    {

    }

    public class ConnectorModel
    {
        public int Text { get; set; }
    }
}
