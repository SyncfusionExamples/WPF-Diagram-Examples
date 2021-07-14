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
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Layout;
using Syncfusion.UI.Xaml.Diagram.Panels;
using System.Collections.ObjectModel;
using Syncfusion.UI.Xaml.Diagram;
using System.Diagnostics;

namespace WpfApplication13
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            node1.Constraints &= ~NodeConstraints.Connectable;
            node2.Constraints &= ~NodeConstraints.Connectable;
            port1.Constraints &= ~PortConstraints.InheritPortVisibility;
            port2.Constraints &= ~PortConstraints.InheritPortVisibility;
            port3.Constraints &= ~PortConstraints.InheritPortVisibility;
            port4.Constraints &= ~PortConstraints.InheritPortVisibility;
            port5.Constraints &= ~PortConstraints.InheritPortVisibility;
            port6.Constraints &= ~PortConstraints.InheritPortVisibility;
            port1.PortVisibility = PortVisibility.Visible;
            port2.PortVisibility = PortVisibility.Visible;
            port3.PortVisibility = PortVisibility.Visible;
            port4.PortVisibility = PortVisibility.Visible;
            port5.PortVisibility = PortVisibility.Visible;
            port6.PortVisibility = PortVisibility.Visible;
            port1.Constraints = PortConstraints.Connectable & ~PortConstraints.InheritConnectable;
            port2.Constraints = PortConstraints.Connectable & ~PortConstraints.InheritConnectable;
            port3.Constraints = PortConstraints.Connectable & ~PortConstraints.InheritConnectable;
            port4.Constraints = PortConstraints.Connectable & ~PortConstraints.InheritConnectable;
            port5.Constraints = PortConstraints.Connectable & ~PortConstraints.InheritConnectable;
            port6.Constraints = PortConstraints.Connectable & ~PortConstraints.InheritConnectable;
        }
    }
    public class AnnotationCollection : ObservableCollection<IAnnotation>
    {

    }
    public class portcollection : ObservableCollection<IPort>
    {

    }
    public class customdiagram:SfDiagram
    {
        protected override void SetTool(SetToolArgs args)
        {
            if (args.Source is IPort)
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

