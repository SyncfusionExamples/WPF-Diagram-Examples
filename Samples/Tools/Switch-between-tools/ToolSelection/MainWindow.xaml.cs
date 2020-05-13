using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Panels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
using ToolSelection.ViewModel;

namespace ToolSelection
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

    public class CustomDiagram : SfDiagram
    {
        public CustomDiagram()
        {

        }
        protected override void SetTool(SetToolArgs args)
        {
            if (args.Source is INode || args.Source is IConnector || args.Source is DiagramPage)
            {
                if ((DataContext as DiagramVM)._singleselect)
                {
                    args.Action = ActiveTool.Drag;
                }
                else if ((DataContext as DiagramVM)._multipleselect)
                {
                    args.Action = ActiveTool.RubberBandSelection;
                }
                else if ((DataContext as DiagramVM)._none)
                {
                    args.Action = ActiveTool.None;
                }
                else if ((DataContext as DiagramVM)._zoompan)
                {
                    args.Action = ActiveTool.Pan;
                }
                else if ((DataContext as DiagramVM)._draw)
                {
                    args.Action = ActiveTool.Draw;
                }
            }
            else
            {
                base.SetTool(args);
            }

        }
    }
}
