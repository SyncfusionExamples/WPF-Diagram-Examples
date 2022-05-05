using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Syncfusion.SfSkinManager;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Theming;
using Syncfusion.Windows.Tools.Controls;

namespace DiagrammingApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        private bool first = true;
        public MainWindow()
        {
            InitializeComponent();
            (diagramcontrol.Info as IGraphInfo).ViewPortChangedEvent += FlowDiagram_ViewPortChangedEvent;
            stencil.DiagramTheme = diagramcontrol.Theme;
            diagramcontrol.PropertyChanged += Diagramcontrol_PropertyChanged;
            SfSkinManager.SetTheme(this, new Syncfusion.SfSkinManager.Theme() { ThemeName = "Office2019Colorful" });
        }

        private DiagramTheme cacheTheme;

        private void Diagramcontrol_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Theme")
            {
                if (cacheTheme != null)
                {
                    cacheTheme.PropertyChanged -= DiagramTheme_PropertyChanged;
                    cacheTheme = null;
                }

                if (((SfDiagram)sender).Theme != null)
                {
                    cacheTheme = ((SfDiagram)sender).Theme;
                    stencil.DiagramTheme = Activator.CreateInstance(cacheTheme.GetType()) as DiagramTheme;
                    cacheTheme.PropertyChanged += DiagramTheme_PropertyChanged;
                }
            }
        }

        private void DiagramTheme_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "NodeStyles")
            {
                stencil.DiagramTheme.NodeStyles = (sender as DiagramTheme).NodeStyles;
            }
            else if (e.PropertyName == "ConnectorStyles")
            {
                stencil.DiagramTheme.ConnectorStyles = (sender as DiagramTheme).ConnectorStyles;
            }
        }

        private void FlowDiagram_ViewPortChangedEvent(object sender, ChangeEventArgs<object, ScrollChanged> args)
        {
            if (diagramcontrol.Info != null && (args.Item as SfDiagram).IsLoaded == true && first && args.NewValue.ContentBounds != args.OldValue.ContentBounds)
            {
                (diagramcontrol.Info as IGraphInfo).BringIntoCenter(args.NewValue.ContentBounds);
                first = false;
            }
        }
    }
}
