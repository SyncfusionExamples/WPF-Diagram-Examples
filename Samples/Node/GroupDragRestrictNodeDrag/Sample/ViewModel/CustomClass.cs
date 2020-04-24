using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GroupDragRestrictNodeDrag
{
    public class CustomClass:DiagramViewModel
    {
        public CustomClass()
        {
            ItemSelectedCommand = new Command(OnItemSelectedCommandExecute);

            //Initialize the Nodes, connectors and groups Collection 
            this.Nodes = new ObservableCollection<NodeViewModel>();
            this.Connectors = new ObservableCollection<ConnectorViewModel>();
            this.Groups = new ObservableCollection<GroupViewModel>();

            //creating nodes
            NodeViewModel sourceNode = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 100,
                OffsetX = 100,
                OffsetY = 100,
                Shape = App.Current.Resources["Rectangle"],
            };

            NodeViewModel targetNode = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 100,
                OffsetX = 300,
                OffsetY = 300,
                Shape = App.Current.Resources["Rectangle"],
            };

            GroupViewModel group = new GroupViewModel()
            {
                Nodes = new ObservableCollection<NodeViewModel>()
                {
                    sourceNode,
                    targetNode,
                },
            };

            (this.Groups as ObservableCollection<GroupViewModel>).Add(group);
        }

        private void OnItemSelectedCommandExecute(object parameter)
        {
            if (this != null)
            {
                if ((parameter as ItemSelectedEventArgs).Item is IGroup)
                {
                   foreach (var item in (((parameter as ItemSelectedEventArgs).Item as IGroup).Nodes) as ObservableCollection<NodeViewModel>)
                    {
                        (item as INode).Constraints = NodeConstraints.Default;
                    }
                }
                else if ((parameter as ItemSelectedEventArgs).Item is INode)
                {
                    ((parameter as ItemSelectedEventArgs).Item as INode).Constraints = NodeConstraints.Default 
                        & ~(NodeConstraints.Draggable | NodeConstraints.InheritDraggable);
                }
            }
        }
    }
}
