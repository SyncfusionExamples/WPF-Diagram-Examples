using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace Node_and_Annotation_Highlight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Diagram.Nodes = new NodeCollection();

            // Created a simple node with annotation based on the provided image.
            NodeViewModel viewModel = new NodeViewModel();
            viewModel.OffsetX = 300;
            viewModel.OffsetY = 300;
            viewModel.UnitHeight = 100;
            viewModel.UnitWidth = 100;
            viewModel.Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) };
            viewModel.ShapeStyle = this.Resources["NodeDefaultStyle"] as Style;

            viewModel.Annotations = new AnnotationCollection();

            AnnotationEditorViewModel anno = new AnnotationEditorViewModel();
            anno.Content = "Node";
            anno.ViewTemplate = this.Resources["AnnotationDefaultViewTemplate"] as DataTemplate;
            anno.Constraints = anno.Constraints.Remove(AnnotationConstraints.InheritSelectable);
            anno.Constraints = anno.Constraints.Add(AnnotationConstraints.Selectable);
            (viewModel.Annotations as AnnotationCollection).Add(anno);
            (Diagram.Nodes as NodeCollection).Add(viewModel);


            // Events to achieve your requirement
            (Diagram.Info as IGraphInfo).ItemUnSelectedEvent += MainWindow_ItemUnSelectedEvent;
            (Diagram.Info as IGraphInfo).ItemSelectedEvent += MainWindow_ItemSelectedEvent;
            (Diagram.Info as IGraphInfo).ItemTappedEvent += MainWindow_ItemTappedEvent;
        }

        private object previousTapItem = null;
        private object tapItemParent = null;

        /// <summary>
        /// Event which is used to apply the default style to node and its annotation while unselecting a node.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MainWindow_ItemUnSelectedEvent(object sender, DiagramEventArgs args)
        {
            // Check condition for the unselected item is node.
            if (args.Item is NodeViewModel)
            {
                NodeViewModel node = args.Item as NodeViewModel;
                node.ShapeStyle = this.Resources["NodeDefaultStyle"] as Style;
                // Get all the annotations in the unselected node and change thier view template to default.
                foreach (AnnotationEditorViewModel anno in node.Annotations as AnnotationCollection)
                {
                    anno.ViewTemplate = this.Resources["AnnotationDefaultViewTemplate"] as DataTemplate;
                }
            }
        }


        /// <summary>
        /// Event which is used to apply the highlight style to node and its annotation while selecting a node.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MainWindow_ItemSelectedEvent(object sender, DiagramEventArgs args)
        {
            // Check condition for the selected item is node.
            if (args.Item is NodeViewModel)
            {
                NodeViewModel node = args.Item as NodeViewModel;
                node.ShapeStyle = this.Resources["NodeHighlightStyle"] as Style;

                // Get all the annotations in the selected node and change thier view template to highlight.
                foreach (AnnotationEditorViewModel anno in node.Annotations as AnnotationCollection)
                {
                    anno.ViewTemplate = this.Resources["AnnotationHighlightViewTemplate"] as DataTemplate;
                }
            }
        }

        /// <summary>
        /// Event which is used to apply default and highlight style while tap on the annotation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MainWindow_ItemTappedEvent(object sender, ItemTappedEventArgs args)
        {
            // Check the tapped item is annotation. Based on that we have updated the style of node and teamplate of the annotation.
            if (args.Item is AnnotationEditorViewModel && previousTapItem == null && tapItemParent == null)
            {
                AnnotationEditorViewModel anno = (AnnotationEditorViewModel)args.Item;
                anno.ViewTemplate = this.Resources["AnnotationHighlightViewTemplate"] as DataTemplate;

                if(args.OriginalSource is NodeViewModel)
                {
                    NodeViewModel node = (NodeViewModel)args.OriginalSource;
                    node.ShapeStyle = this.Resources["NodeHighlightStyle"] as Style;
                    tapItemParent = node;
                }

                previousTapItem = anno;
            }
            // This code will help us to change to default when we again tap on annotation or on any element. 
            else if (previousTapItem != null && tapItemParent != null)
            {
                (previousTapItem as AnnotationEditorViewModel).ViewTemplate = this.Resources["AnnotationDefaultViewTemplate"] as DataTemplate;
                (tapItemParent as NodeViewModel).ShapeStyle = this.Resources["NodeDefaultStyle"] as Style;
                previousTapItem = null;
                tapItemParent = null;

            }
        }
    }
}
