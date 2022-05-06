using Syncfusion.UI.Xaml.Diagram;
using System.Windows;

namespace LogicCircuitSimulation.ViewModel
{
    public class OrGateViewModel : GateViewModel
    {
        public OrGateViewModel() : base()
        {
            this.ContentTemplate = App.Current.Resources[Constants.OrGate] as DataTemplate;
        }

        protected override void GenerateLogic()
        {
            int? state = null;
            if (this.Info is INodeInfo nodeInfo && nodeInfo.InConnectors != null)
            {
                foreach (WireViewModel connector in nodeInfo.InConnectors)
                {
                    if (state.HasValue)
                    {
                        state = (int)connector.State | state.Value;
                    }
                    else
                    {
                        state = (int)connector.State;
                    }
                }

                if (state.HasValue)
                    this.State = (BinaryState)state;
            }
        }
    }
}
