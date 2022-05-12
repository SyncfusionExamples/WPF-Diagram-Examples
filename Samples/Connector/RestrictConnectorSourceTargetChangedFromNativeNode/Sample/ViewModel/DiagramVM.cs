using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeCreation
{
    public class DiagramVM : DiagramViewModel
    {
        public DiagramVM()
        {
            //Adding Commands
            ConnectorSourceChangedCommand = new DelegateCommand(OnConnectorSourceChangedCommand);
            ConnectorTargetChangedCommand = new DelegateCommand(OnConnectorTargetChangedCommand);
            //Creating nodes
            NodeViewModel SourceNode = new NodeViewModel()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 200,
                OffsetY = 200,
                Shape = App.Current.MainWindow.Resources["Rectangle"],
            };

            NodeViewModel TargetNode = new NodeViewModel()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 400,
                OffsetY = 400,
                Shape = App.Current.MainWindow.Resources["Rectangle"],
            };

            //Adding nodes into node's collection
            (this.Nodes as NodeCollection).Add(SourceNode);
            (this.Nodes as NodeCollection).Add(TargetNode);

            //Creating connector.
            ConnectorViewModel connector = new ConnectorViewModel()
            {
                SourceNode = SourceNode,
                TargetNode = TargetNode,
            };

            //Adding connectors into connector's collection
            (this.Connectors as ConnectorCollection).Add(connector);
        }

        //Method to execute ConnectorTargetChangedCommand.
        private void OnConnectorTargetChangedCommand(object parameter)
        {
            var args = parameter as ChangeEventArgs<object, ConnectorChangedEventArgs>;
            if (args.NewValue.Node == null && args.OldValue.Node != null)
            {
                NodeViewModel node = args.OldValue.Node as NodeViewModel;
                //Reset target node when it is changed to null from target node.
                (args.Item as ConnectorViewModel).TargetNode = node;
            }
        }

        //Method to execute ConnectorSourceChangedCommand.
        private void OnConnectorSourceChangedCommand(object parameter)
        {
            var args = parameter as ChangeEventArgs<object, ConnectorChangedEventArgs>;
            if (args.NewValue.Node == null && args.OldValue.Node != null)
            {
                NodeViewModel node = args.OldValue.Node as NodeViewModel;
                //Reset source node when it is changed to null from source node.
                (args.Item as ConnectorViewModel).SourceNode = node;
            }
        }
    }
}
