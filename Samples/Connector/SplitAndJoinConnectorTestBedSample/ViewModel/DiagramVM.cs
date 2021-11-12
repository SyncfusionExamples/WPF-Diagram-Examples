using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInteraction_Stencil
{
    public class DiagramVM : DiagramViewModel
    {
        public DiagramVM()
        {
            this.Nodes = new NodeCollection();
            this.Connectors = new ConnectorCollection();
            this.SymbolDroppingCommand = new Command(OnSymbolDroppingCommandExecute);
            this.DropCommand = new Command(OnDropCommandExecute);

            NodeViewModel node = new NodeViewModel()
            {
                OffsetX = 100,
                OffsetY = 300,
                UnitHeight = 100,
                UnitWidth = 100,
                Shape = App.Current.Resources["Rectangle"],

                Ports = new PortCollection()
                {
                    new NodePortViewModel()
                    {
                        NodeOffsetX = 1,
                        NodeOffsetY = 0,
                        ID = "In1"
                    },
                    new NodePortViewModel()
                    {
                        NodeOffsetX = 1,
                        NodeOffsetY = 0.5,
                        ID="In2"
                    },
                    new NodePortViewModel()
                    {
                        NodeOffsetX = 1,
                        NodeOffsetY = 1,
                        ID="In3"
                    },
                    new NodePortViewModel()
                    {
                        NodeOffsetY = 0,
                        NodeOffsetX = 0.5,
                        ID = "In4"
                    },
                },
            };



            NodeViewModel node1 = new NodeViewModel()
            {
                OffsetX = 500,
                OffsetY = 300,
                UnitHeight = 100,
                UnitWidth = 100,
                Shape = App.Current.Resources["Rectangle"],

                Ports = new PortCollection()
                {
                    new NodePortViewModel()
                    {
                        NodeOffsetX = 0,
                        NodeOffsetY = 1,
                        ID="Out1",
                    },
                    new NodePortViewModel()
                    {
                        NodeOffsetX = 1,
                        NodeOffsetY = 0.5,
                        ID = "Out2",
                    },
                },
            };

            NodeViewModel node2 = new NodeViewModel()
            {
                OffsetX = 500,
                OffsetY = 450,
                UnitHeight = 100,
                UnitWidth = 100,
                Shape = App.Current.Resources["Triangle"],

                Ports = new PortCollection()
                {
                    new NodePortViewModel()
                    {
                        NodeOffsetX = 0,
                        NodeOffsetY = 1,
                    },
                    new NodePortViewModel()
                    {
                        NodeOffsetX = 0.5,
                        NodeOffsetY = 0,
                    },
                },
            };

            (this.Nodes as NodeCollection).Add(node);
            (this.Nodes as NodeCollection).Add(node1);
            (this.Nodes as NodeCollection).Add(node2);

            ConnectorViewModel conn = new ConnectorViewModel()
            {
                SourceNode = node,
                TargetNode = node1,
                SourcePortID = "In2",
                TargetPortID = "Out1",
            };

            (this.Connectors as ConnectorCollection).Add(conn);

        }

        private void OnSymbolDroppingCommandExecute(object obj)
        {

        }
        private void OnDropCommandExecute(object obj)
        {

        }
    }
}
