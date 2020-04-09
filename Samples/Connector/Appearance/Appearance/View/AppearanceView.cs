using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectorAppearance
{
    public class AppearanceView : SfDiagram
    {
        /// <summary>
        /// Override the validate connection
        /// </summary>
        /// <param name="args">Gets args value</param>
        protected override void ValidateConnection(ConnectionParameter args)
        {
            // set the target node and target port
            if (args.TargetPort == null && args.TargetNode != null)
            {
                if (args.TargetNode is NodeViewModel)
                {
                    NodeViewModel node = args.TargetNode as NodeViewModel;
                    if (node.Ports != null && (node.Ports as ObservableCollection<IPort>).Count() > 0)
                    {
                        args.TargetPort = (node.Ports as ObservableCollection<IPort>)[0];
                    }
                }
            }
        }
    }
}
