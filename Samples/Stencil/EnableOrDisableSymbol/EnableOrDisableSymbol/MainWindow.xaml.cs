using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
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

namespace StencilSymbolViewModel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
            (diagram.Info as IGraphInfo).ItemAdded += MainWindow_ItemAdded;
        }

        private void MainWindow_ItemAdded(object sender, ItemAddedEventArgs args)
        {
            if(args.Item is NodeViewModel)
            {
                (args.Item as NodeViewModel).UnitHeight = 80;
                (args.Item as NodeViewModel).UnitWidth = 80;
                if ((args.Item as CustomNodeVM).ContentTemplateKey != "")
                {
                    (args.Item as NodeViewModel).ContentTemplate = this.Resources[(args.Item as CustomNodeVM).ContentTemplateKey] as DataTemplate;
                }
            }
        }
    }

    /// <summary>
    /// Represents a custom class to enable or disable the symbol 
    /// </summary>
    public class CustomNodeVM: NodeViewModel
    {
        private bool isSymbolEnabled = false;

        private string contenttemplatekey = string.Empty;
        /// <summary>
        /// Gets or sets value that indicating whether the symbol can be enabled or disbaled.
        /// </summary>
        public bool IsSymbolEnabled
        {
            get
            {
                return isSymbolEnabled;
            }

            set
            {
                if (isSymbolEnabled != value)
                {
                    isSymbolEnabled = value;
                    OnPropertyChanged("IsSymbolEnabled");
                }
            }
        }

        /// <summary>
        /// Gets or Sets the Key value for the ContentTemplate
        /// </summary>
        [DataMember]
        public string ContentTemplateKey
        {
            get 
            { 
                return contenttemplatekey; 
            }
            set
            {
                if (contenttemplatekey != value)
                {
                    contenttemplatekey = value;
                    OnPropertyChanged("ContentTemplateKey");
                }

            }
        }
    }

}
