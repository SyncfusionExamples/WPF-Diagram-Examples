using AutomaticLayout_MindmapLayout.ViewModel;
using Syncfusion.UI.Xaml.Diagram;
using System.Windows;

namespace AutomaticLayout_MindmapLayout
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Diagram.Loaded += Diagram_Loaded;
        }

        private void Diagram_Loaded(object sender, RoutedEventArgs e)
        {
            var layout = this.Diagram.LayoutManager.Layout as SfMindMapTreeLayout;

            (this.Diagram.Info as IGraphInfo).Commands.FitToPage.Execute(null);

            foreach (NodeViewModel node in this.Diagram.Nodes as NodeCollection)
            {
                node.Annotations = null;
            }

            ((layout.LayoutRoot as INode).Info as INodeInfo).BringIntoCenter();
        }

        private void TextBox_PreviewMouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
