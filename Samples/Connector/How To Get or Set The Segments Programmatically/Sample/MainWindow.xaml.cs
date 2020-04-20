using Microsoft.Win32;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Incident_sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IEnumerable<Point> segmentpoints;
        ConnectorViewModel connector;
        public MainWindow()
        {
            InitializeComponent();
            diagram.Connectors = new ConnectorCollection();
            connector = new ConnectorViewModel()
            {
                SourcePoint = new Point(400,400),
                TargetPoint = new Point(800,200),
            };
            (diagram.Connectors as ConnectorCollection).Add(connector);
            (diagram.Info as IGraphInfo).ConnectorEditing += MainWindow_ConnectorEditing;
        }

        private void MainWindow_ConnectorEditing(object sender, ConnectorEditingEventArgs args)
        {
            if (args.Item is ConnectorViewModel)
            {
                //To get segment points of the Orthogonal segment
                segmentpoints = ((args.Item as ConnectorViewModel).Info as IConnectorInfo).ToPoints();
            }
        }

        private void LoadSegment_Click(object sender, RoutedEventArgs e)
        {
            // To Load segment points of the connector
            segmentpoints = new List<Point>() { new Point(20, 20), new Point(30, 30) };
            (connector.Info as IConnectorInfo).LoadSegments(segmentpoints);
        }
    }
}
