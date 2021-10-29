using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace UndoRedo_HistoryManager
{
    /// <summary>
    /// Custom class for diagram view model.
    /// </summary>
    public class HistoryViewModel: DiagramViewModel
    {
        //Initialize the commands for buttons used in the property panel.
        public ICommand ChangeNodeColorCommand { get; set; }
        public ICommand UndoCommand { get; set; }
        public ICommand RedoCommand { get; set; }
        public ICommand RestrictUndoRedoCommand { get; set; }
        public HistoryViewModel()
        {
            //Initialize the nodes and connectors.
            this.Nodes = new ObservableCollection<NodeVm>();
            this.Connectors = new ConnectorCollection();

            //Initialize the rulers.
            this.HorizontalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler();
            this.VerticalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler()
            {
                Orientation = Orientation.Vertical,
            };

            //Initialize the history manager.
            this.HistoryManager = new HistoryManagerViewModel();

            //Creating the NodeViewModel
            NodeVm redNode = new NodeVm()
            {
                UnitWidth = 100,
                UnitHeight = 100,
                OffsetX = 100,
                OffsetY = 160,
                //Specify shape to the Node from built-in Shape Dictionary
                Shape = App.Current.Resources["Ellipse"],
                Fill = new SolidColorBrush(Colors.LightPink),
            };

            NodeVm blackNode = new NodeVm()
            {
                UnitWidth = 100,
                UnitHeight = 100,
                OffsetX = 250,
                OffsetY = 160,
                //Specify shape to the Node from built-in Shape Dictionary
                Shape = App.Current.Resources["Ellipse"],
                Fill = new SolidColorBrush(Colors.LightBlue),
            };

            NodeVm greenNode = new NodeVm()
            {
                UnitWidth = 100,
                UnitHeight = 100,
                OffsetX = 400,
                OffsetY = 160,
                //Specify shape to the Node from built-in Shape Dictionary
                Shape = App.Current.Resources["Ellipse"],
                Fill = new SolidColorBrush(Colors.LightGray),
            };

            //Add Node to Nodes property of the Diagram
            (this.Nodes as ObservableCollection<NodeVm>).Add(redNode);
            (this.Nodes as ObservableCollection<NodeVm>).Add(blackNode);
            (this.Nodes as ObservableCollection<NodeVm>).Add(greenNode);

            //Enable the undo constraint
            this.Constraints = GraphConstraints.Default | GraphConstraints.Undoable;

            //Initialize the command to execute the undo redo values
            ChangeNodeColorCommand = new Command(OnChangeNodeColorCommandExecute);
            UndoCommand = new Command(OnUndoCommandExecute);
            RedoCommand = new Command(OnRedoCommandExecute);
            RestrictUndoRedoCommand = new Command(OnRestrictUndoRedoCommandExecute);
            
            //Initialize the selected items.
            this.SelectedItems = new SelectorViewModel()
            {
            };

            //Restrict the quick command and rotator command.
            (this.SelectedItems as SelectorViewModel).SelectorConstraints &= ~(SelectorConstraints.QuickCommands | SelectorConstraints.Rotator);
        }

        /// <summary>
        /// Method to change the nodes color.
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        private void OnChangeNodeColorCommandExecute(object parameter)
        {
            CompositeTransactions start = new CompositeTransactions();
            start.State = TransactionState.Start;
            this.HistoryManager.BeginComposite(this.HistoryManager, start);
            foreach (NodeVm node in ((IEnumerable<object>)(this.SelectedItems as SelectorViewModel).Nodes))
            {
                if (node is NodeVm)
                {
                    (node as NodeVm).Fill = new SolidColorBrush(Colors.CornflowerBlue);
                }
            }
            CompositeTransactions end = new CompositeTransactions();
            end.State = TransactionState.End;
            this.HistoryManager.EndComposite(this.HistoryManager, end);
        }

        /// <summary>
        /// Method to execute the undo command.
        /// </summary>
        /// <param name="parameter">command paramter.</param>
        private void OnUndoCommandExecute(object parameter)
        {
            (this.Info as IGraphInfo).Commands.Undo.Execute(null);
        }

        /// <summary>
        /// Method to execute the redo command.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        private void OnRedoCommandExecute(object parameter)
        {
            (this.Info as IGraphInfo).Commands.Redo.Execute(null);
        }

        /// <summary>
        /// Method to restict the undo redo command from history manager.
        /// </summary>
        /// <param name="parameter"></param>
        private void OnRestrictUndoRedoCommandExecute(object parameter)
        {
            if (this != null)
            {
                if ((bool)parameter)
                {
                    NodeVm selectedNode = ((IEnumerable<object>)(this.SelectedItems as SelectorViewModel).Nodes).FirstOrDefault() as NodeVm;
                    if(selectedNode != null)
                        selectedNode.CanLogmultipleselect = false;
                }
                else
                {
                    NodeVm selectedNode = ((IEnumerable<object>)(this.SelectedItems as SelectorViewModel).Nodes).FirstOrDefault() as NodeVm;
                    if (selectedNode != null)
                        selectedNode.CanLogmultipleselect = true;
                }
            }
        }
    }

    /// <summary>
    /// Custom class for history manager of diagram.
    /// </summary>
    public class HistoryManagerViewModel : Syncfusion.UI.Xaml.Diagram.HistoryManager
    {
        public HistoryManagerViewModel()
        {
        }

        /// <summary>
        /// Overidden method to execute the Undo command.
        /// </summary>
        /// <param name="data">Undo data values.</param>
        /// <returns>Returns the undo data.</returns>
        public object Undo(object data)
        {
            return data;
        }

        /// <summary>
        /// Overidden method to execute the Redo command.
        /// </summary>
        /// <param name="data">Redo data values.</param>
        /// <returns>Returns the undo data.</returns>
        public object Redo(object data)
        {
            return data;
        }

        public override bool CanLogData(IUndoRedo source, object data)
        {
            return base.CanLogData(source, data);
        }
    }

    /// <summary>
    /// Custom node view model to change the node fill color.
    /// </summary>
    public class NodeVm : NodeViewModel, IUndoRedo
    {
        public NodeState _mCollectionData;

        public NodeVm()
        {
            _mCollectionData = new NodeState(Fill);
        }

        private Brush _mFill = new SolidColorBrush(Colors.Black);

        public Brush Fill
        {
            get
            {
                return _mFill;
            }
            set
            {
                if (_mFill != value)
                {
                    _mFill = value;
                    OnPropertyChanged("Fill");
                }
            }
        }

        private bool AllowToLogData(string name)
        {
            if (name == "Fill")
            {
                return true;
            }

            return false;
        }

        private bool canlog = true;

        public bool CanLogmultipleselect
        {
            get
            {
                return canlog;
            }
            set
            {
                if (value != canlog)
                {
                    canlog = value;
                    OnPropertyChanged("CanLogmultipleselect");
                }
            }
        }

        public void LogData(object data)
        {
            var info = this.Info as INodeInfo;
            if (info != null && info.Graph != null)
            {
                info.Graph.HistoryManager.LogData(this, data);
            }
        }

        private void OnFillChanged()
        {
            Style s = new Style(typeof(Path));
            if (Fill != null)
            {
                s.Setters.Add(new Setter(Path.FillProperty, Fill));
                s.Setters.Add(new Setter(Path.StretchProperty, Stretch.Fill));
            }
            ShapeStyle = s;
        }

        protected override void OnPropertyChanged(string name)
        {
            var info = this.Info as INodeInfo;
            if (info != null && info.Graph != null && info.Graph.HistoryManager != null && AllowToLogData(name))
            {

                if (info.Graph.HistoryManager.CanLogData(info.Graph.HistoryManager, _mCollectionData))
                {
                    LogData(_mCollectionData);
                }
            }
            base.OnPropertyChanged(name);
            switch (name)
            {
                case "Fill":
                    OnFillChanged();
                    _mCollectionData.Fill = this.Fill;
                    break;
            }
        }

        public UndoRedoState State { get; set; }

        public bool CanRedo(object data)
        {
            if (State == UndoRedoState.Idle)
            {
                return true;
            }
            return false;
        }

        public bool CanUndo(object data)
        {
            if (State == UndoRedoState.Idle)
            {
                return true;
            }
            return false;
        }

        public object Undo(object data)
        {
            if (data is NodeState)
            {
                return RevertTo(data);
            }
            else
                return data;
        }

        public object Redo(object data)
        {
            if (data is NodeState)
            {
                return RevertTo(data);
            }
            else
                return data;
        }

        public object RevertTo(object data)
        {
            if (data is NodeState)
            {
                var current = GetData();
                NodeState toState = (NodeState)data;
                if (toState.Fill != this.Fill)
                {
                    this.Fill = toState.Fill;
                }

                return current;
            }
            return data;
        }

        public object GetData()
        {
            return _mCollectionData;
        }
    }


    internal static class NodeConstants
    {
        public const string Fill = "Fill";
    }

    public struct NodeState
    {
        private Brush _mFill;
        public Brush Fill
        {
            get
            {
                return _mFill;
            }
            set
            {
                if (_mFill != value)
                {
                    _mFill = value;
                }
            }
        }
        public NodeState(Brush fill)
        {
            _mFill = fill;
        }
    }

    /// <summary>
    /// Custom class for SfDiagram
    /// </summary>
    public class ViewModelDiagram : SfDiagram
    {
        protected override bool CanLogHistoryEntry(LogDataArgs item)
        {
            if (item.Item is NodeVm)
            {
                if (!(item.Item as NodeVm).CanLogmultipleselect)
                {
                    return false;
                }
                return base.CanLogHistoryEntry(item);
            }
            else
                return base.CanLogHistoryEntry(item);
        }
    }
}
