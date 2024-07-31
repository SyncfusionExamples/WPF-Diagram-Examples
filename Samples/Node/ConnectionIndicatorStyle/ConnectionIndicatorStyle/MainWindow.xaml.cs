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
using Syncfusion.UI.Xaml.Diagram;
using System.Diagnostics;

namespace CreateDiagram
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

    public class Diagram : SfDiagram
    {
        protected override void SetTool(SetToolArgs args)
        {
            Console.WriteLine(args.Source);
            if (args.Source is INodePort)
            {
                //To change the connection indicator style of the port connection.
                this.ConnectionIndicatorStyle = this.Resources["PortConnectorIndicatorstyle"] as Style;
                base.SetTool(args);
            }
            else if (args.Source is NodeViewModel)
            {
                this.ConnectionIndicatorStyle = this.Resources["NodeConnectorIndicatorstyle"] as Style;
                base.SetTool(args);
            }
            else
            {
                base.SetTool(args);
            }
        }

        protected override void ValidateConnection(ConnectionParameter args)
        {
            var isSourceAction = args.ConnectorEnd == ConnectorEnd.Source;
            if ((isSourceAction && args.SourcePort != null) || (!isSourceAction && args.TargetPort != null))
            {
                //To change the connection indicator style of the port connection.
                this.ConnectionIndicatorStyle = Application.Current.Resources["PortConnectorIndicatorstyle"] as Style;
            }
            else if ((isSourceAction && args.SourceNode != null) || (!isSourceAction && args.TargetNode != null))
            {
                this.ConnectionIndicatorStyle = Application.Current.Resources["NodeConnectorIndicatorstyle"] as Style;
            }

            base.ValidateConnection(args);
        }
    }
}
