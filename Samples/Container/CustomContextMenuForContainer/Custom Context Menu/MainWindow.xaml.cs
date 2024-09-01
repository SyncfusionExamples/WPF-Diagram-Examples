using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Layout;
using Syncfusion.Windows.Shared;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CustomContextMenu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            (diagram.Info as IGraphInfo).LayoutUpdated += OnLayoutUpdated;

            diagram.Groups = new GroupCollection();
            DiagramMenuItem createContainerMenuItem = new DiagramMenuItem()
            {
                Content = "Container",
                Command = new DelegateCommand(OnExecutingCreateContainerCommand, CanExecuteCreateContainer),
            };
            diagram.Menu.MenuItems.Add(createContainerMenuItem);

            DiagramMenuItem updateLayoutMenuItem = new DiagramMenuItem()
            {
                Content = "Update Layout",
                Command = new DelegateCommand(OnExecutingUpdateLayoutCommand, CanExecuteUpdateLayout),
            };
            diagram.Menu.MenuItems.Add(updateLayoutMenuItem);
        }

        private void OnLayoutUpdated(object sender, LayoutUpdatedEventArgs args)
        {
            if (args.Status == DiagramStatus.Completed)
            {
                this.diagram.ResetContainerPosition();
            }
        }

        public void OnExecutingCreateContainerCommand(object parameter)
        {
            this.diagram.CreateContainer("container");
        }

        public bool CanExecuteCreateContainer(object param)
        {
            //Node selection count.
            int selectedNodesCount = ((diagram.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>).Count();
            if (selectedNodesCount > 0)
            {
                return true;
            }

            return false;
        }

        public void OnExecutingUpdateLayoutCommand(object parameter)
        {
            if (diagram.LayoutManager == null || diagram.LayoutManager.Layout == null)
            {
                diagram.LayoutManager = new LayoutManager()
                {
                    Layout = new DirectedTreeLayout()
                    {
                        HorizontalSpacing = 50,
                        VerticalSpacing = 50,
                        Orientation = TreeOrientation.LeftToRight,
                        Type = LayoutType.Hierarchical,
                        AvoidSegmentOverlapping = false,
                    },
                };
            }

            diagram.LayoutManager.Layout.UpdateLayout();
        }

        public bool CanExecuteUpdateLayout(object param)
        {
            if (diagram.Nodes is NodeCollection nodes && nodes.Count > 0)
            {
                return true;
            }

            return false;
        }
    }

    public class CustomContainerVM : ContainerViewModel
    {

    }

    public static class DiagramUtility
    {
        public static void CreateContainer(this SfDiagram diagram, string name)
        {
            var selectedItems = diagram.SelectedItems as ISelector;
            var selectedNodes = (selectedItems.Nodes as IEnumerable<object>).OfType<NodeViewModel>();
            var selectedConnectors = (selectedItems.Connectors as IEnumerable<object>).OfType<ConnectorViewModel>();
            var nodesToAdd = new ObservableCollection<NodeViewModel>(selectedNodes);
            var connectorsToAdd = new ObservableCollection<ConnectorViewModel>(selectedConnectors);

            var selectionBounds = (selectedItems.Info as ISelectorInfo).Bounds;
            selectionBounds.Inflate(30, 20);

            var offsetX = selectionBounds.X + selectionBounds.Width * 0.5;
            var offsetY = selectionBounds.Y + selectionBounds.Height * 0.5;

            var containerHeader = new ContainerHeaderViewModel()
            {
                UnitHeight = 40,
                Annotation = new AnnotationEditorViewModel()
                {
                    Content = name,
                    FontSize = 18,
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#343434"))
                },
            };

            // To make header not overlap with the child nodes.
            selectionBounds.Height += containerHeader.UnitHeight;
            offsetY -= containerHeader.UnitHeight * 0.5;

            var container = new CustomContainerVM()
            {
                Name = name,
                Nodes = nodesToAdd,
                Connectors = connectorsToAdd,
                UnitWidth = selectionBounds.Width,
                UnitHeight = selectionBounds.Height,
                OffsetX = offsetX,
                OffsetY = offsetY
            };

            //Create dotted lines
            var containerStyle = new Style(typeof(Path));
            containerStyle.Setters.Add(new Setter(Path.FillProperty, Brushes.Transparent));
            containerStyle.Setters.Add(new Setter(Path.StretchProperty, Stretch.Fill));
            containerStyle.Setters.Add(new Setter(Path.StrokeProperty, Brushes.Black));
            containerStyle.Setters.Add(new Setter(Path.StrokeDashArrayProperty, new DoubleCollection() { 2, 3 }));

            container.ShapeStyle = containerStyle;

            container.Header = containerHeader;

            (diagram.Groups as GroupCollection).Add(container);
        }

        public static void ResetContainerPosition(this SfDiagram diagram)
        {
            if (diagram.Groups is GroupCollection groups && groups.Count > 0)
            {
                var containers = groups.OfType<IContainer>().ToList();
                if (containers.Count > 0)
                {
                    foreach (var container in containers)
                    {
                        var nodesToAdd = new ObservableCollection<NodeViewModel>();
                        if (container.Nodes is ObservableCollection<NodeViewModel> nodes && nodes.Count > 0)
                        {
                            var bounds = Rect.Empty;

                            while (nodes.Count > 0)
                            {
                                var node = nodes[0];
                                bounds.Union(((INodeInfo)node.Info).Bounds);
                                nodesToAdd.Add(node);
                                nodes.RemoveAt(0);
                            }

                            bounds.Inflate(30, 20);
                            var offsetX = bounds.X + bounds.Width * 0.5;
                            var offsetY = bounds.Y + bounds.Height * 0.5;
                            if (container.Header != null && !double.IsNaN(container.Header.UnitHeight))
                            {
                                bounds.Height += container.Header.UnitHeight;
                                offsetY -= container.Header.UnitHeight * 0.5;
                            }

                            container.OffsetX = offsetX;
                            container.OffsetY = offsetY;
                            container.UnitWidth = bounds.Width;
                            container.UnitHeight = bounds.Height;

                            foreach (var node in nodesToAdd)
                            {
                                (container.Nodes as ObservableCollection<NodeViewModel>).Add(node);
                            }
                        }
                    }
                }
            }
        }
    }
}

