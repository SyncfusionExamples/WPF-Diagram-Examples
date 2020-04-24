using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace PortCreation_Settool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class CustomDiagram:SfDiagram
    {
        public CustomDiagram()
        {
            (this.Info as IGraphInfo).ObjectDrawn += CustomDiagram_ObjectDrawn;
        }

        private void CustomDiagram_ObjectDrawn(object sender, ObjectDrawnEventArgs args)
        {
            //SourcePort should be set on Started state
            if (args.State == DragState.Started)
            {
                if (args.Item is IConnector)
                {
                    IConnector connector = args.Item as IConnector;
                    if (connector.SourceNode != null)
                    {
                        if ((connector.SourceNode as NodeViewModel).Ports == null)
                            //Initialize the Port collection
                            (connector.SourceNode as NodeViewModel).Ports = new ObservableCollection<IPort>();

                        //Set the TargetPort as NodePort to the Node
                        args.SourcePort = new NodePortViewModel();
                    }
                    if (connector.SourceConnector != null)
                    {
                        if ((connector.SourceConnector as ConnectorViewModel).Ports == null)
                            //Initialize the Port collection
                            (connector.SourceConnector as ConnectorViewModel).Ports = new ObservableCollection<IPort>();
                        //Set the TargetPort as ConnectorPort to the Connector
                        args.SourcePort = new ConnectorPortViewModel();
                    }
                }
            }

            //TargetPort should be set on Started state
            if (args.State == DragState.Completed)
            {
                if (args.Item is IConnector)
                {
                    IConnector connector = args.Item as IConnector;
                    if (connector.TargetNode != null)
                    {
                        if ((connector.TargetNode as NodeViewModel).Ports == null)
                            //Initialize the Port collection
                            (connector.TargetNode as NodeViewModel).Ports = new ObservableCollection<IPort>();
                        //Set the TargetPort as NodePort to the Node
                        args.TargetPort = new NodePortViewModel();
                    }
                    if (connector.TargetConnector != null)
                    {
                        if ((connector.TargetConnector as ConnectorViewModel).Ports == null)
                            //Initialize the Port collection
                            (connector.TargetConnector as ConnectorViewModel).Ports = new ObservableCollection<IPort>();
                        //Set the TargetPort as ConnectorPort to the Connector
                        args.TargetPort = new ConnectorPortViewModel();
                    }
                }
            }

        }

        //Override the SetTool method
        protected override void SetTool(SetToolArgs args)
        {
            if (args.Source is INode || args.Source is IConnector)
            {
                args.Action = ActiveTool.Draw;
            }
            else
            {
                base.SetTool(args);
            }
        }

    }
}
