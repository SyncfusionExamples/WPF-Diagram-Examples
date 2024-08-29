using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CustomDataSource.Model
{
    public class ItemInfo
    {
        private string _shape; 
        private double _width = 250;
        private double _height = 60;
        public ItemInfo()
        {

        }

        public string Id { get; set; }
        public string Name { get; set; }

        public string Label { get; set; }
        public List<string> ParentId { get; set; }
        public string _Shape {
            get
            {
                return _shape;
            }
            set
            {
                _shape = value;

            }
        }
       public double Height
        {
            get
            {
                return _height;
            }
            set { _height = value; }
        }
        public double Width
        {
            get
            {
                return _width;
            }
            set { _width = value; }
        }
       
        public string level { get; set; }
       
        public string BranchDirection { get;  set; }
        public object Text { get; set; }
    }
    public class DataItems : ObservableCollection<ItemInfo>
    {

    }
}
