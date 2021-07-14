using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
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

namespace ConnectionValidation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var selector = diagram.SelectedItems as ISelector;
            selector.SelectorConstraints &= ~(SelectorConstraints.QuickCommands | SelectorConstraints.Tooltip | SelectorConstraints.Rotator);
        }

        private void ShowPort_ChangedEvent(object sender, SelectionChangedEventArgs e)
        {
            if (diagram != null)
            {
                switch (showPort.SelectedIndex)
                {
                    case 0:
                        diagram.PortVisibility = PortVisibility.Visible;
                        break;
                    case 1:
                        diagram.PortVisibility = PortVisibility.MouseOver;
                        break;
                    case 2:
                        diagram.PortVisibility = PortVisibility.ValidConnection;
                        break;
                }
            }
        }
    }

    public class NodeVM : NodeViewModel
    {

    }

    public class PortVM : NodePortViewModel
    {
        public PortVM()
        {
            UnitWidth = 10;
            UnitHeight = 10;
            MaxConnection = 1;
        }
        public int MaxConnection { get; set; }
        public bool CanCreateConnection(IConnector ignore)
        {
            var info = this.Info as INodePortInfo;
            if (info.Connectors != null)
            {
                var count = info.Connectors.Where(c => c != ignore).Count();

                // Validate number of connections
                if (MaxConnection >= 0 && count >= MaxConnection)
                {
                    return false;
                }
            }
            else if (MaxConnection == 0)
            {
                return false;
            }
            return true;
        }


    }

    public class CustomDiagram : SfDiagram
    {
        // Validate and choose required tool.
        protected override void SetTool(SetToolArgs args)
        {
            base.SetTool(args);
            if (args.Source is IPort)
            {
                args.Action = ActiveTool.Draw;
            }

            if (args.Source is PortVM && args.Action == ActiveTool.Draw)
            {
                var port = args.Source as PortVM;
                if (!port.CanCreateConnection(null))
                {
                    args.Action = ActiveTool.None;
                }
            }
        }

        // Validate the connection when connector endpoints are dragged
        protected override void ValidateConnection(ConnectionParameter args)
        {
            if (args.TargetPort is PortVM)
            {
                var port = args.TargetPort as PortVM;
                if (!port.CanCreateConnection(args.Connector as IConnector))
                {
                    args.TargetPort = null;
                }
            }
            base.ValidateConnection(args);
        }
    }

    public class DiagramVM : DiagramViewModel
    {
        private bool _animtation = true;
        public bool AnimationEnabled { get { return _animtation; } set { _animtation = value; OnPropertyChanged("AnimationEnabled"); } }
    }
}
