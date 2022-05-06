using Syncfusion.UI.Xaml.Diagram;

namespace LogicCircuitSimulation.ViewModel
{
    public interface IGate
    {
        BinaryState State { get; set; }
    }

    public abstract class BaseGateViewModel : NodeViewModel, IGate
    {
        private BinaryState state = BinaryState.Zero;


        public BaseGateViewModel() : base()
        {
            this.Constraints = NodeConstraints.Default & ~NodeConstraints.Connectable;
        }

        public BinaryState State
        {
            get
            {
                return state;
            }

            set
            {
                if (state != value)
                {
                    state = value;
                    this.OnPropertyChanged("State");
                }
            }
        }

        public SimpleEventHandler StateChanged;

        public void ResetState()
        {
            this.GenerateLogic();
        }

        protected override void OnPropertyChanged(string name)
        {
            base.OnPropertyChanged(name);
            if (name == "State")
            {
                if (StateChanged != null)
                {
                    StateChanged.Invoke(this);
                }

                this.RefreshTemplate();
            }
        }

        protected virtual void GenerateLogic()
        {

        }

        protected virtual void RefreshTemplate()
        {

        }
    }

    public abstract class GateViewModel : BaseGateViewModel
    {
        public GateViewModel() : base()
        {

        }

        internal void InitEvents(WireViewModel connector)
        {
            connector.StateChanged += OnInputStateChanged;
        }

        internal void DisposeEvents(WireViewModel connector)
        {
            connector.StateChanged -= OnInputStateChanged;
        }

        private void OnInputStateChanged(IGroupable inputWire)
        {
            this.GenerateLogic();
        }
    }


    public delegate void SimpleEventHandler(IGroupable sender);
}
