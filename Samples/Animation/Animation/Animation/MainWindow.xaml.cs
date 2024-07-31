using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
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

namespace WpfApplication2
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
    public class Annotation:ObservableCollection<IAnnotation>
    {

    }
    public class Port:ObservableCollection<IPort>
    {

    }
    public class customdiagram : SfDiagram
    {
        protected override void SetTool(SetToolArgs args)
        {
            if (args.Source is INode || args.Source is IPort)
            {
                args.Action = ActiveTool.Draw;
            }
            else
            {
                base.SetTool(args);
            }
        }
    }
}
