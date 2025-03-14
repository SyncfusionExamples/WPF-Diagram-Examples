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

namespace SequenceDiagram
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

        private void LoadDiagramFromMermaid_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(MermaidTextBox.Text))
            {
                Diagram.LoadDiagramFromMermaid(MermaidTextBox.Text.ToString());
            }
        }

        private void SaveDiagramFromMermaid_Click(object sender, RoutedEventArgs e)
        {
            if (Diagram.Model != null)
                MermaidTextBox.Text = Diagram.SaveDiagramAsMermaid();
        }
    }
}
