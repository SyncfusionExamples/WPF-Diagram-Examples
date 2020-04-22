using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleNodePorts
{
    public class PortViewModel:DiagramViewModel
    {
        public PortViewModel()
        {
            //Initialize the node collection
            this.Nodes = new ObservableCollection<NodeViewModel>();

            //Enable the port visiblity.
            this.PortVisibility = PortVisibility.Visible;

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
                        NodeOffsetX = 0,
                        NodeOffsetY = 0,
                    },
                    new NodePortViewModel()
                    {
                        NodeOffsetX = 0.5,
                        NodeOffsetY = 0,
                    },
                    new NodePortViewModel()
                    {
                        NodeOffsetX = 1,
                        NodeOffsetY = 0,
                    },
                    new NodePortViewModel()
                    {
                        NodeOffsetX = 0,
                        NodeOffsetY = 0.5,
                    },
                    new NodePortViewModel()
                    {
                        NodeOffsetX = 1,
                        NodeOffsetY = 0.5,
                    },
                    new NodePortViewModel()
                    {
                        NodeOffsetX = 0,
                        NodeOffsetY = 1,
                    },
                    new NodePortViewModel()
                    {
                        NodeOffsetX = 0.5,
                        NodeOffsetY = 01,
                    },
                    new NodePortViewModel()
                    {
                        NodeOffsetX = 1,
                        NodeOffsetY = 1,
                    },

                }
            };

            //Adding node into node collection.
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(node);
        }
    }
}
