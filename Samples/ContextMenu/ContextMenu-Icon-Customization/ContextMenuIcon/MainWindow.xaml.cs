using Syncfusion.UI.Xaml.Diagram.Controls;
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

namespace ContextMenuIcon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Directly set a image 
            DiagramMenuItem rotatemenu = new DiagramMenuItem()
            {
                Content = "Rotate",
                Command = (SampleDiagram.Info as IGraphInfo).Commands.Rotate,
                Icon = new Image()
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/Asset/Rotate.png"))
                },
            };
            SampleDiagram.Menu.MenuItems.Add(rotatemenu);

            //Set a image using string format
            DiagramMenuItem duplicatemenu = new DiagramMenuItem()
            {
                Content = "Duplicate",
                Command = (SampleDiagram.Info as IGraphInfo).Commands.Duplicate,
                Icon = @"pack://application:,,,/Asset/Duplicate.png"
            };
            SampleDiagram.Menu.MenuItems.Add(duplicatemenu);


            //Set a image using uri format
            DiagramMenuItem editmenu = new DiagramMenuItem()
            {
                Content = "Edit Annotation",
                Command = (SampleDiagram.Info as IGraphInfo).Commands.EditAnnotation,
                Icon = new Uri("pack://application:,,,/Asset/Edit Annotation.png", UriKind.RelativeOrAbsolute),
            };
            SampleDiagram.Menu.MenuItems.Add(editmenu);

            //Set a image using Path Data 
            DiagramMenuItem boldmenu = new DiagramMenuItem()
            {
                Content = "Delete",
                Command = (SampleDiagram.Info as IGraphInfo).Commands.Delete,
                Icon =new Path()
                { 
                    Data = Geometry.Parse("M15.000001,8.0000272L17,8.0000272 17,27.000028 15.000001,27.000028z M11.000001,8.0000272L13.000001,8.0000272 13.000001,27.000028 11.000001,27.000028z M7.0000005,8.0000272L9.000001,8.0000272 9.000001,27.000028 7.0000005,27.000028z M3.0790101,5.0000274L4.9959994,29.922999 19.000006,30.000026 20.918949,5.0000274z M13.771989,1.9959999L10.198989,2.0000001C10.158989,2.025,9.9989892,2.241,9.9989892,2.6000001L9.9989892,3.0000258 13.998989,3.0000258 13.998989,2.6000001C13.998989,2.241,13.838988,2.025,13.771989,1.9959999z M10.198989,0L13.798988,0C15.031989,0,15.998989,1.142,15.998989,2.6000001L15.998989,3.0000258 23.07898,3.0000258 24,3.0000272 24,5.0000274 22.925121,5.0000274 20.995976,30.076991C20.999027,31.102992,20.100956,32.000026,18.999029,32.000026L4.9990512,32.000026C3.8960255,32.000026,2.9990543,31.102992,2.999054,30.000026L1.073059,5.0000274 0,5.0000274 0,3.0000272 0.91897895,3.0000272 7.9989887,3.0000258 7.9989887,2.6000001C7.9989887,1.142,8.9659892,0,10.198989,0z"),
                    Fill=new SolidColorBrush(Colors.White),
                    Stretch= Stretch.Fill,
                }
            };
            SampleDiagram.Menu.MenuItems.Add(boldmenu);

        }
    }
}
