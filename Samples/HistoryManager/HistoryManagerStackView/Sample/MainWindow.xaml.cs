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

namespace HistoryManagerDemoSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Diagram.CommandManager.Commands.RemoveAt(2);
            Diagram.CommandManager.Commands.RemoveAt(1);
            Diagram.CommandManager.Commands.RemoveAt(0);

            ((Diagram.SelectedItems as SelectorViewModel).Commands as QuickCommandCollection).RemoveAt(2);
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            (sender as ToggleButton).Content = "End Group Action";
            System.Windows.Input.Keyboard.Focus(this.Diagram);
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            (sender as ToggleButton).Content = "Start Group Action";
            System.Windows.Input.Keyboard.Focus(this.Diagram);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (Diagram.Info as IGraphInfo).ClearHistory();
            UndoText.Text = "";
            RedoText.Text = "";
        }
    }
}
