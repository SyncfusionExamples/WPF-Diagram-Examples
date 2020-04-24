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
            diagram.KnownTypes = GetKnownTypes;

        }

        private bool SymbolFilter(SymbolFilterProvider sender, object symbol)
        {
            return true;
        }
        private IEnumerable<Type> GetKnownTypes()
        {
            List<Type> known = new List<Type>()
            {
                typeof(CustomButton),
                 typeof(CustomComboBox),
                 typeof(CustomDataGrid),
                 typeof(CustomTextBox),
                 typeof(CustomTextBlock),
                 typeof(CustomEllipse)

            };
            foreach (var item in known)
            {
                yield return item;
            }
        }
    }
    public class CustomButton:NodeViewModel
    {
        private bool _IsHitTestVisible=false;
        [DataMember]
        public bool IsHitTestVisible
        {
            get { return _IsHitTestVisible; }
            set
            {
                if(value!=_IsHitTestVisible)
                {
                    _IsHitTestVisible = value;
                   OnPropertyChanged("IsHitTestVisible");
                }
            }
        }
        private string _text;
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

    }
    public class CustomComboBox:NodeViewModel
    {
        private ObservableCollection<Person> _text;
        [DataMember]
        public ObservableCollection<Person> Items
        {
            get { return _text; }
            set
            {
                if (value != _text)
                {
                    _text = value;
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
    }
    public class Person
    {
        public string  Name { get; set; }
    }
    public class CustomDataGrid:NodeViewModel
    {
        private ObservableCollection<Employee> _text;
        [DataMember]
        public ObservableCollection<Employee> Employees
        {
            get { return _text; }
            set
            {
                if (value != _text)
                {
                    _text = value;
                    OnPropertyChanged("Employees");
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
    }
    public class Employee
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class EmployeeCollection : ObservableCollection<Employee>
    {

    }
    public class CustomEllipse:NodeViewModel
    {
        private string _text;
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

    }
    public class CustomTextBlock:NodeViewModel
    {
        private string _text;
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
    }
    public class CustomTextBox:NodeViewModel
    {
        private string _text;
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
    public class CustomNode:NodeViewModel
    {
        private string _ContenttempName = string.Empty;
        [DataMember]
        public string ContenTempName
        {
            get { return _ContenttempName; }
            set
            {
                if (value != _ContenttempName)
                {
                    _ContenttempName = value;
                    this.ContentTemplate = App.Current.Resources[_ContenttempName] as DataTemplate;
                    OnPropertyChanged("ContenTempName");
                }
            }
        }
        
        [DataMember]
        public object DummyContent
        {
            get { return Content; }
            set
            {
                if (value != Content)
                {
                    Content = value;
                    OnPropertyChanged("Text");
                }
            }
        }

    }
}
