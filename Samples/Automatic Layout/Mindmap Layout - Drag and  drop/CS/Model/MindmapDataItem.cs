using Syncfusion.UI.Xaml.Diagram.Layout;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AutomaticLayout_MindmapLayout.Model
{
    public class MindmapDataItem
    {
        private RootChildDirection _direction = RootChildDirection.Left;
        private int _id = 0;
        private List<MindmapDataItem> _children;
        public MindmapDataItem()
        {

        }
        public MindmapDataItem Parent { get; set; }
        public string Id 
        {
            get
            {
                return Parent == null ? _id.ToString() : Parent.Id.ToString() + (Parent.Children.IndexOf(this) + 1).ToString();
            }
        }
        public string ParentId
        {
            get
            {
                return Parent != null ? Parent.Id : "";
            }
        }
        public string Label { get; set; }
        public string Color {
            get 
            {
                return GetColor();
            }
        }
        public string Shape { get; set; }
        public int Level 
        {
            get 
            {
                return Parent != null ? Parent.Level + 1 : 0;
            }
        }
        public RootChildDirection Direction 
        {
            get
            {
                return (Parent != null && Parent.Level != 0 )? Parent.Direction : _direction;
            }
            set
            {
                _direction = value;
            }
        }
        public List<MindmapDataItem> Children {
            get
            {
                if (_children == null)
                    _children = new List<MindmapDataItem>();
                return _children;
            }
        }
        private string GetColor()
        {
            switch (Level)
            {
                case 0:
                    return "#E74C3C";
                case 1:
                    return "#F39C12";
                case 2:
                    return "#8E44AD";
                case 3:
                    return "#1b80c6";
                case 4:
                    return "#3dbfc9";
            }
            return "#3dbfc9";
        }
    }
    public class MindmapDataItems : ObservableCollection<MindmapDataItem>
    {

    }
}
