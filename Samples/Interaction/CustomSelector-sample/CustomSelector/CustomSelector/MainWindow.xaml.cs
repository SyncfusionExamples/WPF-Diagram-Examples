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

namespace CustomSelector
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

    public class Diagram : SfDiagram
    {
        public Diagram()
        {
            this.SFSelector.Style = App.Current.MainWindow.Resources["CustomSelectorStyle"] as Style;
        }
        //Apply Selectors

        public Selector SFSelector = new Selector();
        protected override Selector GetSelectorForItemOverride(object item)
        {
            return SFSelector;
        }
    }
}
