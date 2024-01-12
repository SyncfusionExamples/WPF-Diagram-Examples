using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ColorPaletteInContextMenuItem
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

        private void Addsubmenu()
        {
            IGraphInfo diagramInfo = (Diagram.Info as IGraphInfo);

            //Initialize the new custom Menu for colorpallate grid
            DiagramMenuItem ChangeColor = new DiagramMenuItem()
            {
                Content = "Change Color",
            };

            //Initialize grid for colorpallate container
            Grid colorgrid = new Grid();
            //Rows definition for the grid
            RowDefinition firstrowdef = new RowDefinition() { Height = new GridLength(25) };
            RowDefinition secondrowdef = new RowDefinition() { Height = new GridLength(25) };
            colorgrid.RowDefinitions.Add(firstrowdef);
            colorgrid.RowDefinitions.Add(secondrowdef);

            //Creating stack panels to the grid
            StackPanel firstrow = new StackPanel() { Orientation = Orientation.Horizontal };
            StackPanel secondrow = new StackPanel() { Orientation = Orientation.Horizontal };

            Grid.SetRow(firstrow, 0);
            Grid.SetRow(secondrow, 1);

            //Buttons for the grid control 
            Button redcolor = new Button() { Width = 20, Height = 20, Margin = new Thickness(20, 0, 0, 0) };
            redcolor.Background = new SolidColorBrush(Colors.Red);
            redcolor.Click += Redcolor_Click;

            Button greencolor = new Button() { Width = 20, Height = 20, Margin = new Thickness(20, 0, 0, 0) };
            greencolor.Background = new SolidColorBrush(Colors.Green);
            greencolor.Click += Greencolor_Click;

            Button yellowcolor = new Button() { Width = 20, Height = 20, Margin = new Thickness(20, 0, 0, 0) };
            yellowcolor.Background = new SolidColorBrush(Colors.Yellow);
            yellowcolor.Click += Yellowcolor_Click;

            //adding buttons to the stack panel
            firstrow.Children.Add(redcolor);
            firstrow.Children.Add(greencolor);
            firstrow.Children.Add(yellowcolor);

            Button bluecolor = new Button() { Width = 20, Height = 20, Margin = new Thickness(20, 0, 0, 0) };
            bluecolor.Background = new SolidColorBrush(Colors.Blue);
            bluecolor.Click += Bluecolor_Click;

            Button blackcolor = new Button() { Width = 20, Height = 20, Margin = new Thickness(20, 0, 0, 0) };
            blackcolor.Background = new SolidColorBrush(Colors.Black);
            blackcolor.Click += Blackcolor_Click;

            Button browncolor = new Button() { Width = 20, Height = 20, Margin = new Thickness(20, 0, 0, 0) };
            browncolor.Background = new SolidColorBrush (Colors.Brown);
            browncolor.Click += Browncolor_Click;

            secondrow.Children.Add(bluecolor);
            secondrow.Children.Add(blackcolor);
            secondrow.Children.Add(browncolor);

            //Adding stack panel to the grid
            colorgrid.Children.Add(firstrow);
            colorgrid.Children.Add(secondrow);

            //Intialize the sub-menu item for color pallate
            DiagramMenuItem ColorsItem = new DiagramMenuItem()
            {
                // Defines the content of menu item as grid
                Content = colorgrid,
            };            

            ChangeColor.Items = new ObservableCollection<DiagramMenuItem>();
            //Adding color palatte menu item into diagram menu as to add sub-menu items.
            ChangeColor.Items.Add(ColorsItem);

            //Adding color palatte menu item into diagram default menu.
            Diagram.Menu.MenuItems.Add(ChangeColor);
        }

        private void Browncolor_Click(object sender, RoutedEventArgs e)
        {
            NodeViewModel node = ((Diagram.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>).First() as NodeViewModel;
            Style style = new Style(typeof(Path));
            style.Setters.Add(new Setter(Path.FillProperty, Brushes.Brown));
            style.Setters.Add(new Setter(Path.StretchProperty, Stretch.Fill));
            node.ShapeStyle = style;
        }

        private void Blackcolor_Click(object sender, RoutedEventArgs e)
        {
            NodeViewModel node = ((Diagram.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>).First() as NodeViewModel;
            Style style = new Style(typeof(Path));
            style.Setters.Add(new Setter(Path.FillProperty, Brushes.Black));
            style.Setters.Add(new Setter(Path.StretchProperty, Stretch.Fill));
            node.ShapeStyle = style;
        }

        private void Bluecolor_Click(object sender, RoutedEventArgs e)
        {
            NodeViewModel node = ((Diagram.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>).First() as NodeViewModel;
            Style style = new Style(typeof(Path));
            style.Setters.Add(new Setter(Path.FillProperty, Brushes.Blue));
            style.Setters.Add(new Setter(Path.StretchProperty, Stretch.Fill));
            node.ShapeStyle = style;
        }

        private void Yellowcolor_Click(object sender, RoutedEventArgs e)
        {
            NodeViewModel node = ((Diagram.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>).First() as NodeViewModel;
            Style style = new Style(typeof(Path));
            style.Setters.Add(new Setter(Path.FillProperty, Brushes.Yellow));
            style.Setters.Add(new Setter(Path.StretchProperty, Stretch.Fill));
            node.ShapeStyle = style;
        }

        private void Greencolor_Click(object sender, RoutedEventArgs e)
        {
            NodeViewModel node = ((Diagram.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>).First() as NodeViewModel;
            Style style = new Style(typeof(Path));
            style.Setters.Add(new Setter(Path.FillProperty, Brushes.Green));
            style.Setters.Add(new Setter(Path.StretchProperty, Stretch.Fill));
            node.ShapeStyle = style;
        }

        private void Redcolor_Click(object sender, RoutedEventArgs e)
        {
            NodeViewModel node = ((Diagram.SelectedItems as SelectorViewModel).Nodes as IEnumerable<object>).First() as NodeViewModel;
            Style style = new Style(typeof(Path));
            style.Setters.Add(new Setter(Path.FillProperty, Brushes.Red));
            style.Setters.Add(new Setter(Path.StretchProperty, Stretch.Fill));
            node.ShapeStyle = style;
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
                        Content = "Node",
                    }
                },
                Shape = new RectangleGeometry() { Rect = new Rect(10, 10, 10, 10) },
                ShapeStyle = Application.Current.MainWindow.Resources["NodeStyle"] as Style,
            };

            //Adding the node into nodes collection
            (Diagram.Nodes as NodeCollection).Add(node);
        }
    }
}
