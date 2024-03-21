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

namespace RestrictNodeFromContainer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            diagram.Groups = new GroupCollection();
            diagram.Nodes = new NodeCollection();
            (diagram.Info as IGraphInfo).NodeChangedEvent += MainWindow_NodeChangedEvent;
            (diagram.Info as IGraphInfo).ItemAdded += MainWindow_ItemAdded;
            CustomNodeVM node1 = new CustomNodeVM()
            {
                ID = "Node1",
                UnitWidth = 100,
                UnitHeight = 100,
                OffsetX = 250,
                OffsetY = 260,
                //Specify shape to the Node from built-in Shape Dictionary
                Shape = this.Resources["Rectangle"],
                             
            };

            //Add Node to Nodes property of the Diagram
            (diagram.Nodes as NodeCollection).Add(node1);


            CustomNodeVM node2 = new CustomNodeVM()
            {
                ID = "Node2",
                UnitWidth = 100,
                UnitHeight = 100,
                OffsetX = 380,
                OffsetY = 380,
                //Specify shape to the Node from built-in Shape Dictionary
                Shape = this.Resources["Rectangle"],
    
            };

            //Add Node to Nodes property of the Diagram
            (diagram.Nodes as NodeCollection).Add(node2);


            ObservableCollection<GroupViewModel> groups = new ObservableCollection<GroupViewModel>();
            ContainerViewModel container = new ContainerViewModel()
            {
                Nodes = new ObservableCollection<CustomNodeVM>() { node1, node2 },
                UnitHeight = 300,
                UnitWidth = 300,
                OffsetX = 300,
                OffsetY = 300,          
            };
            container.Header = new ContainerHeaderViewModel()
            {
                UnitHeight = 30,
                
                Annotation = new AnnotationEditorViewModel()
                {
                    Content = "Container Header",
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.Black,
                    ReadOnly = true,
                },
            };
            groups.Add(container);
            diagram.Groups = groups;
        }

        private void MainWindow_ItemAdded(object sender, ItemAddedEventArgs args)
        {
            if (args.Item is ContainerViewModel)
            {
                var container = (ContainerViewModel)args.Item;
                if ((container.Nodes as IEnumerable<object>).Any())
                {
                    foreach (var item in container.Nodes as IEnumerable<object>)
                    {
                        var node = item as CustomNodeVM;
                        //setting parent container to the nodes.
                        node.ParentContainer = container;
                    }
                }
            }
        }

        private void MainWindow_NodeChangedEvent(object sender, ChangeEventArgs<object, NodeChangedEventArgs> args)
        {
            if (args.Item is CustomNodeVM && !(args.Item is ContainerViewModel))
            {
                var node = args.Item as CustomNodeVM;
                if (node.ParentContainer != null && args.NewValue.InteractionState == NodeChangedInteractionState.Dragging)
                {
                    ContainerViewModel parentcontainer = node.ParentContainer as ContainerViewModel;
                    Rect parentContainerBounds = (parentcontainer.Info as IContainerInfo).Bounds;
                    //Setting Node’s parent container bounds region value to BoundaryConstraint property.
                    args.BoundaryConstraints = new Rect(parentContainerBounds.Left + 5, parentContainerBounds.Top + 45,
                    parentContainerBounds.Width - 10, parentContainerBounds.Height - 50);
                }
            }
        }
    }
    public class CustomNodeVM : NodeViewModel
    {
        private ContainerViewModel parentContainer = null;

        /// <summary>
        /// Gets or sets the Parent Container of the Node.
        /// </summary>
        public ContainerViewModel ParentContainer
        {
            get
            {
                return parentContainer;
            }
            set
            {
                if (parentContainer != value)
                {
                    parentContainer = value;
                    OnPropertyChanged("ParentContainer");
                }
            }
        }
    }
}
