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
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace CustomizePrint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            diagram.HorizontalRuler = new Ruler();
            diagram.VerticalRuler = new Ruler()
            {
                Orientation = Orientation.Vertical,
            };
            diagram.Nodes = new NodeCollection();
            diagram.Connectors = new ConnectorCollection();

            NodeViewModel node1 = new NodeViewModel()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 200,
                OffsetY = 200,
                Shape = this.Resources["Rectangle"],
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content="node1"
                    }
                },

            };
            (diagram.Nodes as NodeCollection).Add(node1);

            NodeViewModel node2 = new NodeViewModel()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 400,
                OffsetY = 400,
                Shape = this.Resources["Rectangle"],
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content="node2"
                    }
                },

            };
            (diagram.Nodes as NodeCollection).Add(node2);
            NodeViewModel node3 = new NodeViewModel()
            {
                UnitHeight = 100,
                UnitWidth = 100,
                OffsetX = 1700,
                OffsetY = 400,
                Shape = this.Resources["Rectangle"],
                Annotations = new ObservableCollection<IAnnotation>()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content="node2"
                    }
                },

            };
            (diagram.Nodes as NodeCollection).Add(node3);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String Extension;
            var zoom = diagram.ScrollSettings.ScrollInfo.CurrentZoom;
            var offsetX = diagram.ScrollSettings.ScrollInfo.HorizontalOffset;
            var offsetY = diagram.ScrollSettings.ScrollInfo.VerticalOffset;
            var width = diagram.ScrollSettings.ScrollInfo.ViewportWidth;
            var height = diagram.ScrollSettings.ScrollInfo.ViewportHeight;

            offsetX = offsetX / zoom;
            offsetY = offsetY / zoom;
            width = width / zoom;
            height = height / zoom;
            
            ExportSettings settings = new ExportSettings()
            {
                Clip = new Rect(offsetX, offsetY, width, height),
            };
          
            diagram.ExportSettings = settings;
            settings.ExportType = Syncfusion.UI.Xaml.Diagram.Controls.ExportType.JPEG;
            
            Extension = ("JPEG File(*.jpeg)|*.jpeg");
            SaveFileDialog m_SaveFileDialog = new SaveFileDialog();
            m_SaveFileDialog.Filter = Extension;
            bool? istrue = m_SaveFileDialog.ShowDialog();
            if (istrue == true)
            {
                string fileName = m_SaveFileDialog.FileName;
               
                //Assigning exportstream from the saved file
                diagram.ExportSettings.FileName = fileName;
                (diagram.Info as IGraphInfo).Export();
            }
        }
    }
}
