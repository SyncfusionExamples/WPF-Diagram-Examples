using System.Windows;

namespace LogicCircuitSimulation.ViewModel
{
    public class InputViewModel : BaseGateViewModel
    {
        public InputViewModel() : base()
        {
            this.RefreshTemplate();
        }

        protected override void RefreshTemplate()
        {
            var resourceName = this.State == BinaryState.Zero ? Constants.DefaultToggleSwitch : Constants.ActiveToogleSwitch;
            this.ContentTemplate = App.Current.Resources[resourceName] as DataTemplate;
        }
    }
}
