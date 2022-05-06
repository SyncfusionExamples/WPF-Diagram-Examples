using Syncfusion.UI.Xaml.Diagram;
using System.Windows;

namespace LogicCircuitSimulation.ViewModel
{
    public class AndGateViewModel : GateViewModel
    {
        public AndGateViewModel() : base()
        {
            this.ContentTemplate = App.Current.Resources[Constants.AndGate] as DataTemplate;

            // to add more ports
           /* this.Ports = new PortCollection()
            {
                new NodePortViewModel(),
                new NodePortViewModel(),
                new NodePortViewModel(),
                new NodePortViewModel(),

            };*/
        }

        protected override void GenerateLogic()
        {
            if (this.Info is INodeInfo nodeInfo && nodeInfo.InConnectors != null)
            {
                int? state = null;
              //  (this.Ports[0] as IPortInfo).InConnectors
                foreach (WireViewModel connector in nodeInfo.InConnectors)
                {
                    if (state.HasValue)
                    {
                        state = (int)connector.State & state.Value;
                    }
                    else
                    {
                        state = (int)connector.State;
                    }
                }

                if (state.HasValue)
                    this.State = (BinaryState)state;
            }

            //if (this.Info is INodeInfo nodeInfo && nodeInfo.InConnectors != null)
            //{
            //    int? state = null;
            //    foreach (WireViewModel connector in nodeInfo.InConnectors)
            //    {
            //        if (state.HasValue)
            //        {
            //            state = (int)connector.State & state.Value;
            //        }
            //        else
            //        {
            //            state = (int)connector.State;
            //        }
            //    }

            //    if (state.HasValue)
            //        this.State = (BinaryState)state;
            //}
        }
    }
}
