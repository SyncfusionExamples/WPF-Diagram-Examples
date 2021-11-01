using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Stencil;
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

namespace ConnectorSegment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
            Style Groupshapestyle = new Style(typeof(Path));

        public MainWindow()
        {
            InitializeComponent();


            (diagram.Info as IGraphInfo).ItemSelectedEvent += MainWindow_ItemSelectedEvent;
            NodeViewModel Node1 = CreateNode(750, 250, "Rectangle", "Shape1");
            NodeViewModel Node2 = CreateNode(700, 400, "RoundedRectangle", "shape2");
            CreateGroup(Node1, Node2);

            BpmnNodeViewModel bpmnEvent = new BpmnNodeViewModel()
            {
                OffsetX = 300,
                OffsetY = 250,
                UnitHeight = 70,
                UnitWidth = 100,
                Type = BpmnShapeType.Event,
                Annotations = new AnnotationCollection()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = "BPMN shape 1",
                    },
                },
            };

            BpmnGroupViewModel bpmngroup = new BpmnGroupViewModel()
            {
                OffsetX = 300,
                OffsetY = 300,
                UnitWidth = 300,
                UnitHeight = 250,
                Shape = this.Resources["Rectangle"],
                ShapeStyle = this.Resources["GroupShapeStyle"] as Style,
                Annotations = new AnnotationCollection()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = "BPMN group1",
                    },
                },
                Nodes = new ObservableCollection<NodeViewModel>()
                {
                    bpmnEvent,
                },
            };
            // Add the group into the Group's collection.
            (diagram.Groups as GroupCollection).Add(bpmngroup);
            //(diagram.Info as IGraphInfo).ItemDropEvent += MainWindow_ItemDropEvent;

            //diagram.PreviewSettings = new PreviewSettings()
            //{
            //    PreviewMode = PreviewMode.Preview,
            //};

            Groupshapestyle.Setters.Add(new Setter(Path.StrokeProperty, Brushes.Black));
            Groupshapestyle.Setters.Add(new Setter(Path.StrokeThicknessProperty, 1d));
            Groupshapestyle.Setters.Add(new Setter(Path.StretchProperty, Stretch.Fill));
            Groupshapestyle.Setters.Add(new Setter(Path.FillProperty, Brushes.White));
            Groupshapestyle.Setters.Add(new Setter(Path.StrokeDashArrayProperty, new DoubleCollection() { 5, 1, 5 }));

        }

        private NodeViewModel CreateNode(double offsetx, double offsety, string shape, string content)
        {
            NodeViewModel node = new NodeViewModel()
            {
                UnitHeight = 60,
                UnitWidth = 100,
                OffsetX = offsetx,
                OffsetY = offsety,
                Shape = App.Current.MainWindow.Resources["Rectangle"],
            };
            if (content != "")
            {
                node.Annotations = new AnnotationCollection()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = content,
                    },
                };
            }

           (diagram.Nodes as NodeCollection).Add(node);

            return node;
        }

        private void CreateGroup(NodeViewModel node1, NodeViewModel node2)
        {
            GroupViewModel group = new GroupViewModel()
            {
                UnitHeight = 400,
                UnitWidth = 400,
                IsContainer = true,
                OffsetX = 800,
                OffsetY = 300,
                Shape = App.Current.MainWindow.Resources["Rectangle"],
                ShapeStyle = App.Current.MainWindow.Resources["GroupContainerStyle"] as Style,
                Nodes = new NodeCollection()
                {
                    node1,
                    node2
                },
                Annotations = new AnnotationCollection()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = "Group 1",
                    },
                },
            };

            (diagram.Groups as GroupCollection).Add(group);
        }
        //private void MainWindow_ItemDropEvent(object sender, ItemDropEventArgs args)
        //{
        //    if (args.ItemSource == Cause.Stencil && args.Source is INode && (args.Source as INode).Key.ToString() == "Basic Shapes")
        //    {
        //        args.Cancel = true;
        //    }
        //}

        private void MainWindow_ItemSelectedEvent(object sender, DiagramEventArgs args)
        {
            if ((args as ItemSelectedEventArgs).Item is IGroup)
            {
                if (((args as ItemSelectedEventArgs).Item as IGroup).IsContainer)
                {
                    //(this.SelectedItems as SelectorViewModel).SelectorConstraints = (this.SelectedItems as SelectorViewModel).SelectorConstraints.Remove(SelectorConstraints.Rotator);
                    IsContainer.IsChecked = true;
                }
                else
                {
                    IsContainer.IsChecked = false;
                }
            }
            else
            {
                IsContainer.IsChecked = false;
            }
            //if (args.Item is NodeViewModel)
            //{
            //    //if ((args.Item as NodeViewModel).Constraints.Contains(NodeConstraints.InheritRestrictNegativeAxisDragDrop))
            //    {
            //        InheritRestrictNodeDraggingConstraint.IsChecked = true;
            //    }

            //    //if (!(args.Item as NodeViewModel).Constraints.Contains(NodeConstraints.InheritRestrictNegativeAxisDragDrop))
            //    {
            //        InheritRestrictNodeDraggingConstraint.IsChecked = false;
            //    }

            //    //if ((args.Item as NodeViewModel).Constraints.Contains(NodeConstraints.InheritRestrictNegativeAxisDragDrop))
            //    {
            //        RestrictNodeDraggingConstraint.IsChecked = true;
            //    }

            //    //if (!(args.Item as NodeViewModel).Constraints.Contains(NodeConstraints.InheritRestrictNegativeAxisDragDrop))
            //    {
            //        RestrictNodeDraggingConstraint.IsChecked = false;
            //    }
            //}
            //else if (args.Item is ConnectorViewModel)
            //{
            //    //if ((args.Item as ConnectorViewModel).Constraints.Contains(ConnectorConstraints.InheritRestrictNegativeAxisDragDrop))
            //    {
            //        InheritRestrictConnectorDragging.IsChecked = true;
            //    }

            //    //if (!(args.Item as ConnectorViewModel).Constraints.Contains(ConnectorConstraints.InheritRestrictNegativeAxisDragDrop))
            //    {
            //        InheritRestrictConnectorDragging.IsChecked = false;
            //    }

            //    //if ((args.Item as ConnectorViewModel).Constraints.Contains(ConnectorConstraints.InheritRestrictNegativeAxisDragDrop))
            //    {
            //        RestrictConnectorDraggingConstraint.IsChecked = true;
            //    }

            //    //if (!(args.Item as ConnectorViewModel).Constraints.Contains(ConnectorConstraints.InheritRestrictNegativeAxisDragDrop))
            //    {
            //        RestrictConnectorDraggingConstraint.IsChecked = false;
            //    }

            //    if ((args.Item as ConnectorViewModel).Constraints.Contains(ConnectorConstraints.Draggable))
            //    {
            //        ConnectorDraggable.IsChecked = true;
            //    }

            //    if (!(args.Item as ConnectorViewModel).Constraints.Contains(ConnectorConstraints.Draggable))
            //    {
            //        ConnectorDraggable.IsChecked = false;
            //    }
            //}
        }

        private void GraphRestrictConstraint_Checked(object sender, RoutedEventArgs e)
        {
            //diagram.Constraints |= GraphConstraints.RestrictNegativeAxisDragDrop;
        }

        private void GraphRestrictConstraint_Unchecked(object sender, RoutedEventArgs e)
        {
            //diagram.Constraints = GraphConstraints.Default & ~GraphConstraints.RestrictNegativeAxisDragDrop;
        }

        private void dragLabel_Checked(object sender, RoutedEventArgs e)
        {
            foreach(object obj in (diagram.SelectedItems as SelectorViewModel).Nodes as ObservableCollection<object>)
            {
                if (obj is NodeViewModel && (obj as NodeViewModel).Annotations != null)
                {
                    foreach (object annotation in (obj as NodeViewModel).Annotations as ObservableCollection<IAnnotation>)
                    {
                        (annotation as IAnnotation).Constraints = AnnotationConstraints.Draggable | AnnotationConstraints.Resizable | AnnotationConstraints.Selectable;
                    }
                }
            }
        }

        private void ConnectorDraggable_Checked(object sender, RoutedEventArgs e)
        {
            if (diagram != null)
            {
                foreach (object obj in (diagram.SelectedItems as SelectorViewModel).Connectors as ObservableCollection<object>)
                {
                    if (obj is ConnectorViewModel)
                    {
                        (obj as ConnectorViewModel).Constraints |= ConnectorConstraints.Draggable;
                    }
                }
            }
        }

        private void ConnectorDraggable_Unchecked(object sender, RoutedEventArgs e)
        {
            if (diagram != null)
            {
                foreach (object obj in (diagram.SelectedItems as SelectorViewModel).Connectors as ObservableCollection<object>)
                {
                    if (obj is ConnectorViewModel)
                    {
                        (obj as ConnectorViewModel).Constraints &= ~ConnectorConstraints.Draggable;
                    }
                }
            }
        }

        private void StencilShowPreview_Checked(object sender, RoutedEventArgs e)
        {
            stencil.Constraints |= StencilConstraints.ShowPreview;
        }

        private void StencilShowPreview_Unchecked(object sender, RoutedEventArgs e)
        {
            stencil.Constraints &= ~StencilConstraints.ShowPreview;
        }

        private void dragLimit_Checked(object sender, RoutedEventArgs e)
        {
            if (diagram != null)
            {
                diagram.ScrollSettings = new ScrollSettings()
                {
                    DragLimit = ScrollLimit.Limited,
                    ScrollableArea = new Rect(-500, -500, 1500, 1500),
                };
            }
        }

        private void dragLimit_Unchecked(object sender, RoutedEventArgs e)
        {
            diagram.ScrollSettings = null;
           
        }


        private void InheritRestrictNodeDraggingConstraint_Checked(object sender, RoutedEventArgs e)
        {
            if (diagram != null)
            {
                foreach (object obj in (diagram.SelectedItems as SelectorViewModel).Nodes as ObservableCollection<object>)
                {
                    if (obj is NodeViewModel)
                    {
                        //(obj as NodeViewModel).Constraints |= NodeConstraints.InheritRestrictNegativeAxisDragDrop;
                    }
                }
            }
        }

        private void InheritRestrictNodeDraggingConstraint_Unchecked(object sender, RoutedEventArgs e)
        {
            if (diagram != null)
            {
                foreach (object obj in (diagram.SelectedItems as SelectorViewModel).Nodes as ObservableCollection<object>)
                {
                    if (obj is NodeViewModel)
                    {
                        //(obj as NodeViewModel).Constraints &= ~NodeConstraints.InheritRestrictNegativeAxisDragDrop;
                    }
                }
            }
        }

        private void RestrictNodeDraggingConstraint_Checked(object sender, RoutedEventArgs e)
        {
            if (diagram != null)
            {
                foreach (object obj in (diagram.SelectedItems as SelectorViewModel).Nodes as ObservableCollection<object>)
                {
                    if (obj is NodeViewModel)
                    {
                        //(obj as NodeViewModel).Constraints |= NodeConstraints.InheritRestrictNegativeAxisDragDrop;
                    }
                }
            }
        }

        private void RestrictNodeDraggingConstraint_Unchecked(object sender, RoutedEventArgs e)
        {
            if (diagram != null)
            {
                foreach (object obj in (diagram.SelectedItems as SelectorViewModel).Nodes as ObservableCollection<object>)
                {
                    if (obj is NodeViewModel)
                    {
                        //(obj as NodeViewModel).Constraints &= ~NodeConstraints.InheritRestrictNegativeAxisDragDrop;
                    }
                }
            }
        }

        private void InheritRestrictConnectorDragging_Checked(object sender, RoutedEventArgs e)
        {
            if (diagram != null)
            {
                foreach (object obj in (diagram.SelectedItems as SelectorViewModel).Connectors as ObservableCollection<object>)
                {
                    if (obj is ConnectorViewModel)
                    {
                        //(obj as ConnectorViewModel).Constraints |= ConnectorConstraints.InheritRestrictNegativeAxisDragDrop;
                    }
                }
            }
        }

        private void InheritRestrictConnectorDragging_Unchecked(object sender, RoutedEventArgs e)
        {
            if (diagram != null)
            {
                foreach (object obj in (diagram.SelectedItems as SelectorViewModel).Connectors as ObservableCollection<object>)
                {
                    if (obj is ConnectorViewModel)
                    {
                        //(obj as ConnectorViewModel).Constraints &= ~ConnectorConstraints.InheritRestrictNegativeAxisDragDrop;
                    }
                }
            }
        }

        private void RestrictConnectorDraggingConstraint_Checked(object sender, RoutedEventArgs e)
        {
            if (diagram != null)
            {
                foreach (object obj in (diagram.SelectedItems as SelectorViewModel).Connectors as ObservableCollection<object>)
                {
                    if (obj is ConnectorViewModel)
                    {
                        //(obj as ConnectorViewModel).Constraints |= ConnectorConstraints.InheritRestrictNegativeAxisDragDrop;
                    }
                }
            }
        }

        private void RestrictConnectorDraggingConstraint_Unchecked(object sender, RoutedEventArgs e)
        {
            if (diagram != null)
            {
                foreach (object obj in (diagram.SelectedItems as SelectorViewModel).Connectors as ObservableCollection<object>)
                {
                    if (obj is ConnectorViewModel)
                    {
                        //(obj as ConnectorViewModel).Constraints &= ~ConnectorConstraints.InheritRestrictNegativeAxisDragDrop;
                    }
                }
            }
        }

        private void Group_Click(object sender, RoutedEventArgs e)
        {
            IGraphInfo graphinfo = diagram.Info as IGraphInfo;

            //UnGroups the selected group elements.
            graphinfo.Commands.Group.Execute(null);
        }

        private void UnGroup_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void IsContainer_Checked(object sender, RoutedEventArgs e)
        {
            if (diagram != null && diagram.SelectedItems != null)
            {
                foreach (GroupViewModel group in (diagram.SelectedItems as SelectorViewModel).Groups as IEnumerable<object>)
                {
                    group.IsContainer = true;
                    group.Shape = App.Current.MainWindow.Resources["Rectangle"];
                    group.ShapeStyle = this.Groupshapestyle as Style;
                }
            }
        }

        private void IsContainer_Unchecked(object sender, RoutedEventArgs e)
        {
            if (diagram != null && diagram.SelectedItems != null)
            {
                foreach (GroupViewModel group in (diagram.SelectedItems as SelectorViewModel).Groups as IEnumerable<object>)
                {
                    group.IsContainer = false;
                }
            }
        }
    }

    public class SymbolCollection : ObservableCollection<Object>
    {
    }

    public class CustomStencil : Stencil
    {
        //Virtual method to customize the preview of dragging the symbol from a stencil.
        //protected override void PrepareDragDropPreview()
        //{
        //    this.SymbolPreview = new ContentPresenter()
        //    {
        //        Content = new Rectangle()
        //        {
        //            Width = 50,
        //            Height = 50,
        //            Fill = new SolidColorBrush(Colors.SteelBlue)
        //        }
        //    };
        //}
    }
}

