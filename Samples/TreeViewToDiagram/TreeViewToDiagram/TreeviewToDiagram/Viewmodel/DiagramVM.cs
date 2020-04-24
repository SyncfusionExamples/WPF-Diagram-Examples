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
            DragEnterCommand = new Command(OnDragEnter);            
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
    }
}
