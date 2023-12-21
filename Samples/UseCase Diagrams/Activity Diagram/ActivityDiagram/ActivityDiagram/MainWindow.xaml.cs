using Syncfusion.UI.Xaml.Diagram.Theming;
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
using ActivityDiagram.ViewModel;
using Syncfusion.SfSkinManager;

namespace ActivityDiagram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string themename;

        public MainWindow()
        {
            InitializeComponent();
            SfSkinManager.SetTheme(this, new Syncfusion.SfSkinManager.Theme() { ThemeName = "Office2019Colorful" });
            Diagram.Theme = new OfficeTheme();
            stencil.DiagramTheme = Diagram.Theme;
           
        }
    }
}
