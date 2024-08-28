using Syncfusion.UI.Xaml.Diagram.Stencil;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Xml.Linq;

namespace StencilSample
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

    public class CustomStencil: Stencil
    {
        public CustomStencil() : base()
        {

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var searchTextBox = this.GetTemplateChild("Part_SearchTextBox") as TextBox;
            if (searchTextBox != null)
            {
                searchTextBox.SetValue(AutomationProperties.AutomationIdProperty, "StencilSearchBox");
            }
        }
    }
}
