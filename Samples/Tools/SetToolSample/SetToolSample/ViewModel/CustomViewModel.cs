using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetCursorKBSample
{
    public class CustomViewModel : DiagramViewModel
    {
        public CustomViewModel()
        {
            //Set the port visibility as visible
            this.PortVisibility = PortVisibility.Visible;

            this.Constraints |= GraphConstraints.AllowPan;

            //Initialize the nodes and connectors collection
            this.Nodes = new ObservableCollection<NodeViewModel>();
            this.Connectors = new ObservableCollection<ConnectorViewModel>();

            //Create the source node
            NodeViewModel sourceNode = new NodeViewModel()
            {
                OffsetX = 200,
                OffsetY = 200,
                UnitHeight = 100,
                UnitWidth = 100,
                Ports = new PortCollection()
                {
                    new NodePortViewModel()
                    {
                        NodeOffsetX = 1,
                        NodeOffsetY = 1,
                    }
                }
            };

            //Create the source node
            NodeViewModel targetNode = new NodeViewModel()
            {
                OffsetX = 400,
                OffsetY = 400,
                UnitHeight = 100,
                UnitWidth = 100,
            };

            //Create the connector.
            ConnectorViewModel connector = new ConnectorViewModel()
            {
                SourceNode = sourceNode,
                TargetNode = targetNode,
            };

            //Add the nodes into nodes collection.
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(sourceNode);
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(targetNode);

            //Add the connectors into connectors collection.
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(connector);
        }
    }
}
