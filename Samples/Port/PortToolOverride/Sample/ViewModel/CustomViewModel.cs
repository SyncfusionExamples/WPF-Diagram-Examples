using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace PortToolOverrideSample
{
    public class CustomViewModel: DiagramViewModel
    {
        public CustomViewModel()
        {
            //Set the port visibility as visible
            this.PortVisibility = PortVisibility.Visible;

            //Initialize the nodes and connectors collection
            this.Nodes = new ObservableCollection<NodeViewModel>();
            this.Connectors = new ObservableCollection<ConnectorViewModel>();

            //Create the node
            NodeViewModel node = new NodeViewModel()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 200,
                OffsetY = 200,
                //Create the ports collection
                Ports = new PortCollection()
                {
                    new NodePortViewModel()
                    {
                        NodeOffsetX = 0.5,
                        NodeOffsetY = 0.5,
                    },
                }
            };

            //Adding node into node collection.
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(node);
        }
    }
}
