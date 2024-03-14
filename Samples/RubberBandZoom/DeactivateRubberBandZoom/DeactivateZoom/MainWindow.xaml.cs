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

namespace DeactivateZoom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            NodeViewModel Begin = new NodeViewModel()
            {
                ID = "Begin",
                UnitWidth = 120,
                UnitHeight = 40,
                OffsetX = 300,
                OffsetY = 60,
                Shape = this.Resources["Ellipse"],
              
            };

            (diagram.Nodes as NodeCollection).Add(Begin);
        }
    }

    //Custom Diagram class for diagram
    public class CustomDiagram : SfDiagram
    {
        //Override method for deactivate the zoom behaviour of the diagram
        protected override void SetTool(SetToolArgs args)
        {
            base.SetTool(args);

            if (args.Action == ActiveTool.RubberBandZoom)
            {
                args.Action = ActiveTool.None;

            }
        }

    }
}
