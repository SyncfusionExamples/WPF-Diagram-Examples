using BringToView.Utility;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BringToView.ViewModel
{
  public class DiagramVM:DiagramViewModel
    {
        NodeViewModel node3, node4;
        private ICommand _ViewCommand;
        private ICommand _CenterCommand;

        public ICommand ViewCommand
        {
            get { return _ViewCommand; }
            set { _ViewCommand = value; }
        }

        public ICommand CenterCommand
        {
            get { return _CenterCommand; }
            set { _CenterCommand = value; }
        }
        public DiagramVM()
        {
            Connectors = new ConnectorCollection();
            Nodes = new NodeCollection();

            this.ScrollSettings = new ScrollSettings();

            ViewCommand = new Command(OnView);
            CenterCommand = new Command(OnCenter);

            NodeViewModel node1 = Addnode(200, 200, "ViewNode","Rectangle", "NodeStyle");
            NodeViewModel node2 = Addnode(400, 400, "ViewNode", "Rectangle", "NodeStyle");

            node3 = Addnode(1200, 900, "Node3","Ellipse", "ViewNodeStyle");
            node4 = Addnode(1400, 1800, "Node4","Ellipse","CenterNodeStyle");

            ConnectorViewModel cvm = new ConnectorViewModel()
            {
                SourceNode = node1,
                TargetNode = node2
            };
            (Connectors as ConnectorCollection).Add(cvm);

            //ConnectorViewModel cvm1 = new ConnectorViewModel()
            //{
            //    SourceNode = node3,
            //    TargetNode = node4
            //};
            //(Connectors as ConnectorCollection).Add(cvm1);

        }
        private NodeViewModel Addnode(double x, double y,string content,string shape,string style)
        {
            NodeViewModel node1 = new NodeViewModel()
            {
                OffsetX = x,
                OffsetY = y,
                UnitHeight = 100,
                UnitWidth = 100,
                Annotations = new AnnotationCollection()
               {
                   new AnnotationEditorViewModel()
                   {
                       Content=content
                   }
               },
                Shape=App.Current.Resources[shape],

                ShapeStyle=App.Current.Resources[style] as Style,

            };
            (Nodes as NodeCollection).Add(node1);
            return node1;
        }

        private void OnView(object obj)
        {
            this.ScrollSettings.ScrollInfo.BringIntoViewport((node3.Info as INodeInfo).Bounds);
        }

        private void OnCenter(object obj)
        {
            this.ScrollSettings.ScrollInfo.BringIntoCenter((node4.Info as INodeInfo).Bounds);
        }
    }
}
