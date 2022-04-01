using ListBoxToDiagram.Utility;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ListBoxToDiagram.Viewmodel
{
   public class DiagramVM:DiagramViewModel
    {       
        public DiagramVM()
        {
            this.HorizontalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler();
            this.VerticalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler()
            {
                Orientation = Orientation.Vertical
            };
            this.SnapSettings = new SnapSettings()
            {
                SnapConstraints = SnapConstraints.All,
            };
            DragEnterCommand = new Command(OnDragEnter);
            DropCommand = new Command(OnDropCommand);
        }
        private void OnDragEnter(object obj)
        {
            var args = obj as ItemDropEventArgs;
            // args.Source have the data which is dragged for drop.
            if (args.Source is DataObject)
            {
                object dataObject = (args.Source as DataObject).GetData(typeof(DragObject<TreeViewItem>));
                TreeViewItem treeViewItem = (dataObject as DragObject<TreeViewItem>).Source;
                //Convert the unknown treeviewitem to diagram known object(NodeViewModel). 
                args.Source = new NodeViewModel()
                {
                    UnitHeight = 50,
                    UnitWidth = 50,
                };
            }
        }

        //Method to execute DropCommand.
        private void OnDropCommand(object obj)
        {
            if (obj is ItemDropEventArgs && (obj as ItemDropEventArgs).Source is INode)
            {
                INode item = (obj as ItemDropEventArgs).Source as INode;
                //Getting position of dropped node.
                double offsetX = item.OffsetX;
                double offsetY = item.OffsetY;
            }
        }
    }
}
