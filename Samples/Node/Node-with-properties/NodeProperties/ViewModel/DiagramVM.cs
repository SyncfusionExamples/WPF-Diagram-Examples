using NodeProperties.Utility;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Theming;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NodeProperties.ViewModel
{
    
    public class DiagramVM : DiagramViewModel
    {       
        public DiagramVM()
        {
            NodeViewModel node = new NodeViewModel();
            node.UnitWidth = 100;
            node.UnitHeight = 100;
            node.OffsetX = 300;
            node.OffsetY = 300;
            node.Shape = new RectangleGeometry() { Rect = new Rect(100, 100, 100, 100) };
            (this.Nodes as NodeCollection).Add(node);            
            ItemSelectedCommand = new Command(OnItemSelected);
        }

        private object _vselecteditem;
        public object SelectedItem
        {
            get { return _vselecteditem; }
            set
            {
                _vselecteditem=value;
                OnPropertyChanged("SelectedItem");
            }
        }
        private void OnItemSelected(object obj)
        {
            var args = obj as ItemSelectedEventArgs;
            SelectedItem = args.Item as NodeViewModel;
        }
    }
}