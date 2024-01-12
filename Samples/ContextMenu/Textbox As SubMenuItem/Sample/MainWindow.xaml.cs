using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace TextBoxSubMenuItem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // create a node
            Createnode();

            // create and add submenu items
            Addsubmenu();
        }

        /// <summary>
        /// Method to add text box as sub-menu item.
        /// </summary>
        private void Addsubmenu()
        {
            IGraphInfo diagramInfo = (Diagram.Info as IGraphInfo);

            //Initialize the new custom Menu item
            var TextBoxMenuItem = new DiagramMenuItem()
            {
                Content = "TextBox Menu Items",
            };

            //Initializing TextBoxes
            TextBox textBox1 = new TextBox() { Text = "1" };
            TextBox textBox2 = new TextBox() { Text = "2" };

            //Intialize the sub-menu items.
            var TextBoxSubMenuItem1 = new DiagramMenuItem()
            {
                //setting menu item content as text box
                Content = textBox1
            };

            var TextBoxSubMenuItem2 = new DiagramMenuItem()
            {
                Content = textBox2
            };

            //Adding textbox menu item into diagram default menus.
            Diagram.Menu.MenuItems.Add(TextBoxMenuItem);
            //Initialize the menu items collection to the Textbox menu item
            TextBoxMenuItem.Items = new ObservableCollection<DiagramMenuItem>();
            //Adding SubMenu items to the collection of TextBoxMenuItem.
            TextBoxMenuItem.Items.Add(TextBoxSubMenuItem1);
            TextBoxMenuItem.Items.Add(TextBoxSubMenuItem2);
        }


        private void Createnode()
        {
            //Initialize the node 
            NodeViewModel node = new NodeViewModel()
            {
                OffsetX = 200,
                OffsetY = 100,
                UnitWidth = 100,
                UnitHeight = 100,
                Annotations = new AnnotationCollection()
                {
                    new AnnotationEditorViewModel()
                    {
                        Content ="Node",
                    }
                 },
            };

            //Adding the node into nodes collection
            (Diagram.Nodes as NodeCollection).Add(node);
        }
    }
}
