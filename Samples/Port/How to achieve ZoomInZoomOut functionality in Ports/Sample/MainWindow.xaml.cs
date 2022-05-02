using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Simple_SfDiagram_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<object, object> values = new Dictionary<object, object>();
        public MainWindow()
        {
            InitializeComponent();
            Diagram.Nodes = new NodeCollection();


            NodeViewModel node = new NodeViewModel()
            {
                OffsetX = 100,
                OffsetY = 100,
                UnitHeight = 100,
                UnitWidth = 100,
                Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) },
                Ports = new PortCollection()
                {
                    new CustomNodePort()
                    {
                        UnitHeight = 10,
                        UnitWidth = 10,
                        NodeOffsetX = 1,
                        NodeOffsetY = 1,
                        InitialHeight = 10,
                        InitialWidth = 10,
                        Shape = new RectangleGeometry(){ Rect = new Rect(10 , 10, 10, 10) },
                    }                    
                },
            };

            (Diagram.Nodes as NodeCollection).Add(node);

            (Diagram.Info as IGraphInfo).ViewPortChangedEvent += MainWindow_ViewPortChangedEvent;

        }

        private void MainWindow_ViewPortChangedEvent(object sender, ChangeEventArgs<object, ScrollChanged> args)
        {
            foreach (NodeViewModel node in Diagram.Nodes as NodeCollection)
            {
                foreach (CustomNodePort nodeport in node.Ports as PortCollection)
                {
                    // This code is to change the height and width of NodePort with respect to the zoomin/ zoomout values.
                    nodeport.UnitHeight = nodeport.InitialHeight * args.NewValue.CurrentZoom;
                    nodeport.UnitWidth = nodeport.InitialWidth * args.NewValue.CurrentZoom;
                }
            }
        }
    }


    public class CustomNodePort : NodePortViewModel
    {
        private double _initialheight;

        // This property is to store the inintial height of the port
        public double InitialHeight
        {
            get
            {
                return _initialheight;
            }
            set
            {
                if(value != _initialheight)
                {
                    _initialheight = value;
                    OnPropertyChanged("InitialHeight");
                }
            }
        }


        private double _initialwidth;

        // This property is to store the inintial width of the port
        public double InitialWidth
        {
            get
            {
                return _initialwidth;
            }
            set
            {
                if (value != _initialwidth)
                {
                    _initialwidth = value;
                    OnPropertyChanged("InitialWidth");
                }
            }
        }
    }

}
