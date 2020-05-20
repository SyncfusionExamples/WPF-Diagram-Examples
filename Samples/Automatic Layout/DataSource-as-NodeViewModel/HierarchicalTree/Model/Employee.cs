using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HierarchicalTree.Model
{
    public class Employee : NodeViewModel,INotifyPropertyChanged
    {
        public string EmpId { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }
        public string _Color { get; set; }
    }


    public class Employees : ObservableCollection<Employee>
    {

    }
}
