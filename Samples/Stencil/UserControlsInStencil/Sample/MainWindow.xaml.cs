using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Stencil;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Xaml_Designer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            diagram.PageSettings.PageBorderBrush = new SolidColorBrush(Colors.Transparent);
            //Adding known types to the diagram
            diagram.KnownTypes = GetKnownTypes;
        }

        private bool SymbolFilter(SymbolFilterProvider sender, object symbol)
        {
            return true;
        }

        //Setting known class to the diagram to serialize
        private IEnumerable<Type> GetKnownTypes()
        {
            List<Type> known = new List<Type>()
            {
                typeof(NodeContent),
                 typeof(CustomComboBox),
                 typeof(Person),
            };
            foreach (var item in known)
            {
                yield return item;
            }
        }
    }

    /// <summary>
    /// Reprsents a class that stores text value to the node content.
    /// </summary>
    [DataContract]
    public class NodeContent
    {
        private bool _IsHitTestVisible = false;
        [DataMember]
        public bool IsHitTestVisible
        {
            get { return _IsHitTestVisible; }
            set
            {
                if (value != _IsHitTestVisible)
                {
                    _IsHitTestVisible = value;
                    OnPropertyChanged("IsHitTestVisible");
                }
            }
        }

        private string _text;

        /// <summary>
        /// Gets or sets the text values to the node's content.
        /// </summary>
        [DataMember]
        public string Text
        {
            get { return _text; }
            set
            {
                if (value != _text)
                {
                    _text = value;
                    OnPropertyChanged("Text");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    /// <summary>
    /// Represents a class that stores combobox values to the node's content property.
    /// </summary>
    [DataContract]
    public class CustomComboBox
    {
        private ObservableCollection<Person> _items;

        /// <summary>
        /// Gets or sets items that needs to be display in the combobox.
        /// </summary>
        [DataMember]
        public ObservableCollection<Person> Items
        {
            get { return _items; }
            set
            {
                if (value != _items)
                {
                    _items = value;
                    OnPropertyChanged("Items");
                }
            }
        }

        private bool _IsHitTestVisible = false;
        [DataMember]
        public bool IsHitTestVisible
        {
            get { return _IsHitTestVisible; }
            set
            {
                if (value != _IsHitTestVisible)
                {
                    _IsHitTestVisible = value;
                    OnPropertyChanged("IsHitTestVisible");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    /// <summary>
    /// Represents the class that stores name values to the combobox items
    /// </summary>
    [DataContract]
    public class Person
    {
        [DataMember]
        public string  Name { get; set; }
    }

    public class annaotioncollection : ObservableCollection<IAnnotation>
    {

    }

    public class SymbolCollection : ObservableCollection<object>
    {

    }

    public class ComboboxItems : ObservableCollection<Person>
    {

    }

    /// <summary>
    /// Represents a class that derived from NodeViewModel and it stores custom content template and custom content values and they used to serialize the node's original Content and ContentTemplate property values.
    /// </summary>
    public class CustomNode:NodeViewModel
    {
        private string _ContenttempName = string.Empty;
        /// <summary>
        /// Gets or sets the key value to access the data template of node from resource file.
        /// </summary>
        [DataMember]
        public string ContentTemplatKey
        {
            get { return _ContenttempName; }
            set
            {
                if (value != _ContenttempName)
                {
                    _ContenttempName = value;
                    this.ContentTemplate = App.Current.Resources[_ContenttempName] as DataTemplate;
                    OnPropertyChanged("ContentTemplatKey");
                }
            }
        }

        private NodeContent _mcustomcontent;
        /// <summary>
        /// Gets or sets the content of the node.
        /// </summary>
        [DataMember]
        public NodeContent CustomContent
        {
            get { return _mcustomcontent; }
            set
            {
                if (value != _mcustomcontent)
                {
                    _mcustomcontent = value;
                    this.Content = CustomContent;
                    OnPropertyChanged("CustomContent");
                }
            }
        }

        private CustomComboBox _mcustomComboBoxContent;
        /// <summary>
        /// Gets or sets the custom content to the combobox node.
        /// </summary>
        [DataMember]
        public CustomComboBox CustomComboBoxContent
        {
            get 
            { 
                return _mcustomComboBoxContent;
            }
            set
            {
                if (value != _mcustomComboBoxContent)
                {
                    _mcustomComboBoxContent = value;
                    this.Content = CustomComboBoxContent;
                    OnPropertyChanged("CustomComboBoxContent");
                }
            }
        }
    }
}
