using Selectionmode.Utility;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Selectionmode.ViewModel
{
    public class DiagramVM : DiagramViewModel
    {
        public ICommand SelectButtonCommand { get; set; }
        NodeViewModel node1;
        public DiagramVM()
        {
            node1 = new NodeViewModel() { UnitWidth = 100, UnitHeight = 100, OffsetX = 200, OffsetY = 300 };
            NodeViewModel node2 = new NodeViewModel() { UnitWidth = 100, UnitHeight = 100, OffsetX = 400, OffsetY = 500 };
            NodeViewModel node3 = new NodeViewModel() { UnitWidth = 100, UnitHeight = 100, OffsetX = 300, OffsetY = 600 };

            (Nodes as NodeCollection).Add(node1);
            (Nodes as NodeCollection).Add(node2);
            (Nodes as NodeCollection).Add(node3);

            ConnectorViewModel connector = new ConnectorViewModel() { SourceNode = node1, TargetNode = node2 };

            (Connectors as ConnectorCollection).Add(connector);

            SelectButtonCommand = new Command(OnSelectedButton);
        }

        private void OnSelectedButton(object obj)
        {
            var button = obj as RadioButton;
            if (button.Name == "SelectNode1")
            {
                node1.IsSelected = true;
            }
            else if (button.Name == "Select")
            {
                this.SingleSelectionMode = SingleSelectionMode.Select;
            }
            else if (button.Name == "ToggleSelect")
            {
                this.SingleSelectionMode = SingleSelectionMode.ToggleSelection;
            }
            else if(button.Name == "Default")
            {
                this.MultipleSelectionMode = MultipleSelectionMode.Default;
            }
            else if(button.Name == "None")
            {
                this.MultipleSelectionMode = MultipleSelectionMode.None;
            }
            else if(button.Name == "HoldKeyAndTap")
            {
                this.MultipleSelectionMode = MultipleSelectionMode.HoldKeyAndTap;
            }
            else if(button.Name == "JustTap")
            {
                this.MultipleSelectionMode = MultipleSelectionMode.JustTap;
            }
            else if(button.Name == "RubberBandCompleteIntersect")
            {
                this.MultipleSelectionMode = MultipleSelectionMode.RubberBandCompleteIntersect;
            }
            else if(button.Name == "RubberBandPartialIntersect")
            {
                this.MultipleSelectionMode = MultipleSelectionMode.RubberBandPartialIntersect;
            }
        }
    }
}