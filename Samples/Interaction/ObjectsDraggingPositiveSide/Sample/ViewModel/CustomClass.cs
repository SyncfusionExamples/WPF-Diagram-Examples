using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ObjectDraggingPositiveSide
{
    public class CustomClass : DiagramViewModel
    {
        public CustomClass()
        {
            SelectorChangedCommand = new Command(OnSelectorChangedCommandExecute);

            //Initialize the Nodes and connectors Collection 
            this.Nodes = new ObservableCollection<NodeViewModel>();
            this.Connectors = new ObservableCollection<ConnectorViewModel>();

            //Initialize the scroll settings
            this.ScrollSettings = new ScrollSettings()
            {
                DragLimit = ScrollLimit.Limited,
                EditableArea = new Rect(0,0,double.PositiveInfinity,double.PositiveInfinity),
            };

            //Initialize the rulers
            this.HorizontalRuler = new Ruler();
            this.VerticalRuler = new Ruler() { Orientation = Orientation.Vertical };

            //creating source node
            NodeViewModel simpleNode = new NodeViewModel()
            {
                UnitWidth = 100,
                UnitHeight = 50,
                OffsetX = 150,
                OffsetY = 300,
                Shape = App.Current.Resources["Rectangle"],
            };

            ConnectorViewModel connector = new ConnectorViewModel()
            {
                SourcePoint = new Point(200,200),
                TargetPoint = new Point(400,400),
            };

            connector.Constraints |= ConnectorConstraints.Default | ConnectorConstraints.Draggable;

            //Adding the nodes and connectors into Collection
            (this.Nodes as ObservableCollection<NodeViewModel>).Add(simpleNode);
            (this.Connectors as ObservableCollection<ConnectorViewModel>).Add(connector);
        }

        private void OnSelectorChangedCommandExecute(object parameter)
        {
            if (this != null)
            {
                //Changing the cursor to indicate the dragging restriction 
                (parameter as SelectorChangedEventArgs).BlockCursor = Cursors.No;
                //Enabling the dragging limit
                (parameter as SelectorChangedEventArgs).Block = true;
            }
        }
    }
}
