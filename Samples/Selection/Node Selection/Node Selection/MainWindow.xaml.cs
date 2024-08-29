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

namespace Test
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
        protected override Selector GetSelectorForItemOverride(object item)
        {
            //Assigning custom selector to the diagram
            CustomSelector selector = new CustomSelector();
            return selector;
        }
    }
    public class CustomSelector : Selector
    {
        //Method for get the Out Neighbors 
        private void GetOutNeighbors(INode firstSelect, INode lastSelect, List<INode> neighbors)
        {
            if (firstSelect != null && lastSelect != null && firstSelect != lastSelect)
            {
                var outNeighbors = (firstSelect.Info as INodeInfo).OutNeighbors as IEnumerable<object>;
                foreach (INode neighbor in outNeighbors)
                {
                    neighbors.Add(neighbor);
                    GetOutNeighbors(neighbor, lastSelect, neighbors);
                }
            }
        }

        //Method for get the In Neighbors
        private void GetInNeighbors(INode firstSelect, INode lastSelect, List<INode> neighbors)
        {
            if (firstSelect != null && lastSelect != null && firstSelect != lastSelect)
            {
                var inNeighbors = (firstSelect.Info as INodeInfo).InNeighbors as IEnumerable<object>;
                foreach (INode neighbor in inNeighbors)
                {
                    neighbors.Add(neighbor);
                    GetInNeighbors(neighbor, lastSelect, neighbors);
                }
            }
        }

        protected override void PointerSelection(PointerSelectionArgs args)
        {
            //Ensured the shift key press
            bool isShiftPressed = Keyboard.IsKeyDown(System.Windows.Input.Key.LeftShift) || Keyboard.IsKeyDown(System.Windows.Input.Key.RightShift);
            if (isShiftPressed)
            {
                var vm = this.DataContext as SelectorViewModel;
                if ((vm != null && vm.SelectedItem == null) || !(args.Source is INode && !(args.Source is IGroup)))
                {
                    base.PointerSelection(args);
                }
                else
                {
                    //Get the first selected item in the diagram
                    var firstSelect = vm.SelectedItem as INode;

                    //Get the last selected item 
                    var lastSelect = args.Source as INode;
                    var neighbors = new List<INode>() { firstSelect };
                    GetOutNeighbors(firstSelect, lastSelect, neighbors);
                    if (neighbors.Count > 1 && neighbors.Last() == lastSelect)
                    {
                        foreach(var neighbor in neighbors)
                        {
                            neighbor.IsSelected = true;
                        }
                    }
                    else
                    {
                        neighbors = new List<INode>() { firstSelect };
                        GetInNeighbors(firstSelect, lastSelect, neighbors);
                        if (neighbors.Count > 1 && neighbors.Last() == lastSelect)
                        {
                            foreach (var neighbor in neighbors)
                            {
                                neighbor.IsSelected = true;
                            }
                        }
                    }
                }
            }
            else
            {
                base.PointerSelection(args);
            }
        }

    }
}
