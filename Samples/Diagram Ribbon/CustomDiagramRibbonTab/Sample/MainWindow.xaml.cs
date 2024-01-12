using Syncfusion.SfSkinManager;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Theming;
using Syncfusion.UI.Xaml.DiagramRibbon;
using Syncfusion.Windows.Tools.Controls;
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

namespace CustomDiagramRibbonTab
{
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DiagramRibbon.Loaded += CustomDiagramRibbonTab_Loaded;
            SfSkinManager.SetTheme(this, new Syncfusion.SfSkinManager.Theme() { ThemeName = "Office2019Colorful" });            
        }

        private void CustomDiagramRibbonTab_Loaded(object sender, RoutedEventArgs e)
        {
            // To get the home ribbon tab. Here 0 is the index value of home tab.
            RibbonTab HomeTab = (sender as SfDiagramRibbon).Tabs.ElementAt(0);
            //To insert the new Ribbon bar at the necessary index.
            HomeTab.Items.Insert(0, new RibbonBar() { Header = "Interaction", Name = "Interaction", Width = 150 });

            //Check box for resizing the nodes in aspect ratio.
            CheckBox aspectRatio = new CheckBox() { Content = "Aspect Ratio" };
            aspectRatio.Checked += AspectRatio_Checked;
            aspectRatio.Unchecked += AspectRatio_Unchecked;

            //Check box for drag the connectors.
            CheckBox dragConnector = new CheckBox() { Content = "Drag Connector", Margin = new Thickness(0, 5, 0, 0) };
            dragConnector.Checked += DragConnector_Checked;
            dragConnector.Unchecked += DragConnector_Unchecked;

            // To get the interaction ribbon bor item from Home tab.
            RibbonBar HomeTabBar = HomeTab.Items[0] as RibbonBar;
            //Adding checkBoxes to the interaction ribbon bar items.
            HomeTabBar.Items.Add(aspectRatio);
            HomeTabBar.Items.Add(dragConnector);
        }

        private void DragConnector_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (IConnector connector in Diagram.Connectors as ConnectorCollection)
            {
                //To remove draggable constraint for connectors
                connector.Constraints &= ~ConnectorConstraints.Draggable;
            }
        }

        private void DragConnector_Checked(object sender, RoutedEventArgs e)
        {
            foreach (IConnector connector in Diagram.Connectors as ConnectorCollection)
            {
                //To add draggable constraint for connectors
                connector.Constraints |= ConnectorConstraints.Draggable;
            }
        }

        private void AspectRatio_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (INode node in Diagram.Nodes as NodeCollection)
            {
                //To remove AspectRatio constraint for nodes
                node.Constraints &= ~NodeConstraints.AspectRatio;
            }
        }

        private void AspectRatio_Checked(object sender, RoutedEventArgs e)
        {
            foreach (INode node in Diagram.Nodes as NodeCollection)
            {
                //To add AspectRatio constraint for nodes
                node.Constraints |= NodeConstraints.AspectRatio;
            }
        }
    }
}
