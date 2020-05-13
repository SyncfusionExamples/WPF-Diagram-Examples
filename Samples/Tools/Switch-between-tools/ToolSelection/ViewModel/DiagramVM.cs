using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ToolSelection.Utility;

namespace ToolSelection.ViewModel
{
   public class DiagramVM :DiagramViewModel
    {       
        public bool _draw = false;
        public bool _zoompan = false;
        public bool _multipleselect = false;
        public bool _none = false;
        public bool _singleselect = false;

        public ICommand SelectButtonCommand { get; set; }
        public DiagramVM()
        {
            Nodes = new NodeCollection();
            Connectors = new ConnectorCollection();

            NodeViewModel node = new NodeViewModel();
            node.Shape = new RectangleGeometry() { Rect = new System.Windows.Rect(100, 100, 100, 100) };
            node.ShapeStyle = App.Current.Resources["NodeStyle"] as Style;
            node.UnitHeight = 100;
            node.UnitWidth = 100;
            node.OffsetX = 300;
            node.OffsetY = 500;
            (Nodes as NodeCollection).Add(node);


            NodeViewModel node1 = new NodeViewModel();
            node1.Shape = new EllipseGeometry() { RadiusX =100,RadiusY=100 };
            node1.ShapeStyle = App.Current.Resources["NodeStyle"] as Style;
            node1.UnitHeight = 100;
            node1.UnitWidth = 100;
            node1.OffsetX = 500;
            node1.OffsetY = 500;
            (Nodes as NodeCollection).Add(node1);

            SelectButtonCommand = new Command(OnSelectedButton);

        }

        private void OnSelectedButton(object obj)
        {
            var button = obj as RadioButton;
            if(button.Name== "SingleSelect")
            {
                _singleselect = true;
                _multipleselect = false;
                _zoompan = false;
                _none = false;
                _draw = false;
            }
            else if(button.Name == "MultipleSelect")
            {
                _multipleselect = true;
                _zoompan = false;
                _none = false;
                _draw = false;
                _singleselect = false;
            }
            else if (button.Name == "Draw")
            {
                _draw = true;
                _zoompan = false;
                _none = false;
                _singleselect = false;
                _multipleselect = false;
            }            
            else if (button.Name == "ZoomPan")
            {
                _zoompan = true;
                _singleselect = false;
                _none = false;
                _draw = false;
                _multipleselect = false;
            }
            else if (button.Name == "None")
            {
                _none = true;
                _singleselect = false;
                _multipleselect = false;
                _draw = false;
                _zoompan = false;
            }

        }
      
    }
}
