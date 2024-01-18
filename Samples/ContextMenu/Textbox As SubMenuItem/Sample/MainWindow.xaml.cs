using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            //Initialize the new custom Menu item
            var TextBoxMenuItem = new DiagramMenuItem()
            {
                Content = "TextBox Menu Items",
            };

            //Initializing TextBoxe
            TextBox textBox = new TextBox() { Text = "One" };
            //Adding TextChanged event
            textBox.TextChanged += textBox_TextChanged;

            //Initialize the sub-menu items.
            var TextBoxSubMenuItem = new DiagramMenuItem()
            {
                //setting menu item content as text box
                Content = textBox
            };
            
            //Adding textbox menu item into diagram default menus.
            Diagram.Menu.MenuItems.Add(TextBoxMenuItem);
            //Initialize the menu items collection to the Textbox menu item
            TextBoxMenuItem.Items = new ObservableCollection<DiagramMenuItem>();
            //Adding SubMenu items to the collection of TextBoxMenuItem.
            TextBoxMenuItem.Items.Add(TextBoxSubMenuItem);
        }

        //Text box text changed event.
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (NodeViewModel node in (Diagram.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>)
            {
                foreach (IAnnotation annotation in node.Annotations as ObservableCollection<IAnnotation>)
                {
                    //Setting text box text value to the selected node annotation's content.
                    annotation.Content = (e.Source as TextBox).Text;
                }
            }
        }

        //Method to create node
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
                        Content ="One",
                        FontWeight = FontWeights.Bold,
                        Foreground = new SolidColorBrush(Colors.White),
                    }
                 },
            };

            //Adding the node into nodes collection
            (Diagram.Nodes as NodeCollection).Add(node);
        }
    }
}
