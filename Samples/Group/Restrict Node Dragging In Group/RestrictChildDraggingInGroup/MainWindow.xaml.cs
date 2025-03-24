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

namespace RestrictChildDraggingInGroup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            NodeViewModel parentNode = new NodeViewModel()
            {
                UnitWidth = 300,
                UnitHeight = 300,
                OffsetX = 660,
                OffsetY = 400,
                Annotations = new AnnotationCollection()
                {
                   new AnnotationEditorViewModel()
                    {
                        Content = "Parent Node",
                        Foreground = new SolidColorBrush(Colors.White),
                    }
                },
                Shape = new RectangleGeometry() { Rect = new Rect(0, 0, 10, 10) },
                ShapeStyle = this.Resources["parentStyle"] as Style
            };
            
            NodeViewModel childNode = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 100,
                OffsetX = 660,
                OffsetY = 330,
                Annotations = new AnnotationCollection()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content = "Child Node",
                        Foreground = new SolidColorBrush(Colors.White),
                    }
                },
                Shape = new RectangleGeometry() { Rect = new Rect(0, 0, 10, 10) },
                ShapeStyle = this.Resources["childStyle"] as Style
            };

            ObservableCollection<GroupViewModel> groups = new ObservableCollection<GroupViewModel>();
            GroupViewModel group = new GroupViewModel()
            {
                OffsetX = 900,
                Nodes = new ObservableCollection<NodeViewModel>()
                {
                    parentNode, 
                    childNode
                },
            };

            groups.Add(group);
            Diagram.Groups = groups;
        }
    }

    public class CustomDiagram : SfDiagram
    {
        //Method to return the selector for diagram
        protected override Selector GetSelectorForItemOverride(object item)
        {
            //Assigning custom selector to the diagram
            CustomSelector selector = new CustomSelector();
            return selector;
        }
    }

    public class CustomSelector : Selector
    {
        //Method to decide whether the Selection should be done in PointerDown.
        protected override void PointerSelection(PointerSelectionArgs args)
        {
            if (args.PointerMode == PointerMode.Down)
            {
                //Check whether if we select the Node or not
                if (args.Source is NodeViewModel)
                {
                    NodeViewModel node = args.Source as NodeViewModel;
                    //Check whether the parent group of the parent is null or not
                    if (node.ParentGroup != null)
                    {
                        //Ensure the IsSelected Property of the Parent Group of the Node.
                        if ((node.ParentGroup as GroupViewModel).IsSelected)
                        {
                            //Clear the Selection if we select the group
                            ClearSelection(Element: args.Source);
                        }
                        else
                        {
                            Selection(element: args.Source);
                        }
                    }
                    else
                    {
                        Selection(element: args.Source);
                    }
                }
                else if (args.Source is ConnectorViewModel)
                {
                    Selection(element: args.Source);
                }
                else if (args.Source is SfDiagram)
                {
                    //Clear the selelction if we select the diagram 
                    ClearSelection(Element: args.Source);
                }
            }
        }
    }

}
