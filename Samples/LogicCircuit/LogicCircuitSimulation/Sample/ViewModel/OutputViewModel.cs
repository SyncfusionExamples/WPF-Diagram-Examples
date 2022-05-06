using Syncfusion.UI.Xaml.Diagram;
using System.Windows;

namespace LogicCircuitSimulation.ViewModel
{
    public class OutputViewModel : GateViewModel
    {
        public OutputViewModel() : base()
        {
            this.RefreshTemplate();
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
        }

        protected override void RefreshTemplate()
        {
            var resourceName = this.State == BinaryState.Zero ? Constants.DefaultLightBulb : Constants.ActiveLightBulb;
            this.ContentTemplate = App.Current.Resources[resourceName] as DataTemplate;
        }
    }
}
