using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZindexUpdate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int OldNodeZindex;
        public MainWindow()
        {
            InitializeComponent();

            //Event will get hooked whenever interact with the node.
            (diagram.Info as IGraphInfo).NodeChangedEvent += MainWindow_NodeChangedEvent;
        }

        private void MainWindow_NodeChangedEvent(object sender, ChangeEventArgs<object, NodeChangedEventArgs> args)
        {
            var draggednode = args.Item as NodeViewModel;

            if (args.NewValue.InteractionState == NodeChangedInteractionState.Dragging)
            {
                if (draggednode.ZIndex != int.MaxValue)
                {
                    OldNodeZindex = draggednode.ZIndex;
                }
                //Updating the maximum ZIndex to the dragging element.
                draggednode.ZIndex = int.MaxValue;                
            }
            else if (args.NewValue.InteractionState == NodeChangedInteractionState.Dragged)
            {
                //Resetting the ZIndex to the actual value.
                draggednode.ZIndex = OldNodeZindex;                
            }
        }
    }
}
