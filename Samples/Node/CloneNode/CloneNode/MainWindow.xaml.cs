using Syncfusion.UI.Xaml.Diagram;
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

namespace NodeCreation
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

        private void clone_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ((diagram.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>).FirstOrDefault();
            if (selectedItem is CustomNodeViewModel)
            {
                //Node that need to be clone
                CustomNodeViewModel origianlNode = selectedItem as CustomNodeViewModel;
                //cloned node
                object clonedNode = origianlNode.Clone();
                //Adding the cloned into diagram node's collection
                (diagram.Nodes as NodeCollection).Add(clonedNode as CustomNodeViewModel);
            }
        }
    }

    /// <summary>
    /// Represents a class that clones the original instance.
    /// </summary>
    public class CustomNodeViewModel : NodeViewModel, ICloneable
    {
        public object Clone()
        {
            var newobject = new CustomNodeViewModel()
            {
                //Adding basic node details from original node clone node
                OffsetX = this.OffsetX,
                OffsetY = this.OffsetY,
                Constraints = this.Constraints,
                Shape = this.Shape,
                ShapeStyle = this.ShapeStyle,
                UnitHeight = this.UnitHeight,
                UnitWidth = this.UnitWidth,

                //You can add other custom propeties of the node that you want to add to clone node.
            
            };

            //cloning ports from original node to cloned node
            newobject.Ports = new PortCollection();
            foreach (NodePortViewModel originalPort in this.Ports as IEnumerable<object>)
            {
                NodePortViewModel clonedPort = Activator.CreateInstance(typeof(NodePortViewModel)) as NodePortViewModel;
                clonedPort.NodeOffsetX = originalPort.NodeOffsetX;
                clonedPort.NodeOffsetY = originalPort.NodeOffsetY;
                (newobject.Ports as PortCollection).Add(clonedPort);
            }

            //cloning annotation from original node to cloned node
            newobject.Annotations = new AnnotationCollection();
            foreach (AnnotationEditorViewModel originalAnnotation in this.Annotations as IEnumerable<object>)
            {
                AnnotationEditorViewModel clonedAnnotation = Activator.CreateInstance(typeof(AnnotationEditorViewModel)) as AnnotationEditorViewModel;
                clonedAnnotation.Content = originalAnnotation.Content;
                (newobject.Annotations as AnnotationCollection).Add(clonedAnnotation);
            }

            return newobject;
        }
    }
}


