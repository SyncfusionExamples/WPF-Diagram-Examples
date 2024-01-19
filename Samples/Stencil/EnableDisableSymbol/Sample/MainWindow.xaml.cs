using Syncfusion.UI.Xaml.Diagram.Stencil;
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

namespace EnableDisableStencilSymbol
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

    /// <summary>
    /// Represents a custom class to enable or disable the symbol 
    /// </summary>
    public class CustomSymbolVM: SymbolViewModel
    {
        private bool isSymbolEnabled = false;
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
    }

}
