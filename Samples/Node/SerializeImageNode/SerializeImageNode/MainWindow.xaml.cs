using Microsoft.Win32;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SyncFusionDiagramTest
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {

        string svgDir = AppDomain.CurrentDomain.BaseDirectory + "EC_Nodes\\Images\\component-1-fill.png";



        CustomNodeViewModel AddTestImageNode(Point offset,string imagekey)
        {

            //port 추가
            PortCollection ports = new PortCollection();

            NodePortViewModel port = new NodePortViewModel()
            {
                NodeOffsetX = 0,
                NodeOffsetY = 0.5
            };

            port.Constraints = PortConstraints.Connectable | PortConstraints.ConnectionDirection;
            port.ConnectionDirection = ConnectionDirection.Left;
            port.ID = "left";
            ports.Add(port);

            NodePortViewModel port2 = new NodePortViewModel()
            {
                NodeOffsetX = 1,
                NodeOffsetY = 0.5
            };

            port2.Constraints = PortConstraints.Connectable | PortConstraints.ConnectionDirection;
            port2.ConnectionDirection = ConnectionDirection.Right;
            port2.ID = "right";
            ports.Add(port2);

            //Node 제약
            //https://help.syncfusion.com/wpf/diagram/constraints#connector-constraints
            CustomNodeViewModel imgNode = new CustomNodeViewModel()
            {
                //sets the size
                UnitHeight = 100,
                UnitWidth = 100,
                //sets the position
                OffsetX = offset.X,
                OffsetY = offset.Y,
                //set image as content
                Content = App.Current.Resources[imagekey],
                ContentKey = imagekey,
                Ports = ports,
                Constraints = NodeConstraints.Selectable | NodeConstraints.Draggable | NodeConstraints.DragAnnotation,

            };

            (diagram1.Nodes as ObservableCollection<CustomNodeViewModel>).Add(imgNode);
            return imgNode;
        }

        public MainWindow()
        {
            InitializeComponent();

            (diagram1.Info as IGraphInfo).ItemAdded += MainWindow_ItemAdded;

            diagram1.Nodes = new ObservableCollection<CustomNodeViewModel>();
            diagram1.Connectors = new ConnectorCollection();


            var imgnode1 = AddTestImageNode(new Point(100, 100),"img");
            var imgnode2 = AddTestImageNode(new Point(200, 200),"img2");




            ConnectorViewModel connector = new ConnectorViewModel()
            {
                SourceNode = imgnode1,
                TargetNode = imgnode2,
                SourcePort = ((PortCollection)imgnode1.Ports)[0],
                TargetPort = ((PortCollection)imgnode2.Ports)[1]
            };
            //Adding connector into Collection
            (diagram1.Connectors as ConnectorCollection).Add(connector);
        }

        private void MainWindow_ItemAdded(object sender, ItemAddedEventArgs args)
        {
            if (args.Item is CustomNodeViewModel)
            {
                //Setting the content to the node using its custom property once it is added to the diagram.
                
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save XAML";
            dialog.Filter = "XAML File (*.xaml)|*.xaml";
            if (dialog.ShowDialog() == true)
            {
                using (Stream str = File.Open(dialog.FileName, FileMode.OpenOrCreate))
                {
                    diagram1.Save(str);
                }
            }

        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            //Load from saved XAML file
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                using (Stream myStream = dialog.OpenFile())
                {
                    diagram1.Load(myStream);
                }
            }

        }
    }

    /// <summary>
    /// Represents a class that have custom property to serialize the content of the node.
    /// </summary>
    public class CustomNodeViewModel : NodeViewModel
    {
        private string _mcontentkey;
        /// <summary>
        /// Gets or sets the key of the node content.
        /// </summary>
        [DataMember]
        public string ContentKey
        {
            get
            {
                return _mcontentkey;
            }
            set
            {
                if (_mcontentkey != value)
                {
                    _mcontentkey = value;
                    OnPropertyChanged("ContentKey");
                }
            }
        }

        protected override void OnPropertyChanged(string name)
        {
            if(name == "ContentKey")
            {
                Content = App.Current.Resources[ContentKey];
            }
            base.OnPropertyChanged(name);
        }
    }
}
