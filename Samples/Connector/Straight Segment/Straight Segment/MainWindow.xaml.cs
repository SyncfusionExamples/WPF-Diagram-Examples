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

namespace StraightSegmentSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Register the item tapped event.
            (Diagram.Info as IGraphInfo).ItemTappedEvent += MainWindow_ItemTappedEvent;
        }

        /// <summary>
        /// Method to raise the item tapped event for splitting the segments.
        /// </summary>
        /// <param name="sender">diagram</param>
        /// <param name="args">Event args</param>
        private void MainWindow_ItemTappedEvent(object sender, DiagramEventArgs args)
        {
            if (args.Item is IConnector)
            {
                //Getting the mouse point while tap on the connector
                Point point = ((args as ItemTappedEventArgs).MouseEventArgs as MouseEventArgs).GetPosition(Diagram.Page);
                //Specify the editing point, editing type and hit padding value
                AddRemoveStraightSegmentArgs addremoveargs = new AddRemoveStraightSegmentArgs()
                {
                    Point = point,
                    SegmentEditing = SegmentEditing.Add,
                    HitPadding = 5
                };

                //Method to add or remove the segment
                ((args.Item as IConnector).Info as IConnectorInfo).EditSegment(addremoveargs);
            }
        }
    }
}
