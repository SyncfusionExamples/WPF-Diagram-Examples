using Syncfusion.UI.Xaml.Diagram.Layout;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace AutomaticLayout_MindmapLayout.Model
{
    public class MindmapDataItem : INotifyPropertyChanged
    {
        private RootChildDirection _direction = RootChildDirection.Left;
        private ObservableCollection<MindmapDataItem> _children;
        private MindmapDataItem _parent = null;
        private State isexpand;

        public MindmapDataItem()
        {

        }
        public MindmapDataItem Parent 
        { 
            get
            {
                return _parent;
            }
            set
            {
                if(_parent != null && _parent != value)
                {
                    MindmapDataItem oldParent = _parent;
                    int index = oldParent.Children.IndexOf(this);
                    oldParent.Children.Remove(this);

                    for (int i = index; i < _parent.Children.Count; i++)
                    {
                        oldParent.Children[i].UpdateIdAndParentID();
                    }
                    _parent = value;
                    if (_parent != null)
                    {
                        _parent.Children.Add(this);
                        this.UpdateIdAndParentID();
                    }
                }
                else
                {
                    _parent = value;
                    if (_parent != null)
                        _parent.Children.Add(this);
                }
            }
        }
        public string Id 
        {
            get
            {
                return Parent == null ? "0" : Parent.Id.ToString() + (Parent.Children.IndexOf(this) + 1).ToString();
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
                return IsRoot? RootChildDirection.Left & RootChildDirection.Right : _direction;
            }
            set
            {
                if (_direction != value)
                {
                    _direction = value;
                    OnPropertyChanged("Direction");
                }
            }
        }
        public ObservableCollection<MindmapDataItem> Children {
            get
            {
                if (_children == null)
                {
                    _children = new ObservableCollection<MindmapDataItem>();
                }
                return _children;
            }
        }


        public bool HasChild
        {
            get
            {
                return (_children != null && _children.Count > 0 && !IsRoot);
            }
        }

        public bool IsRoot
        {
            get
            {
                return _parent == null;
            }
        }

        public State IsExpand
        {
            get
            {
                return isexpand;
            }
            set
            {
                if (isexpand != value)
                {
                    isexpand = value;
                    OnPropertyChanged(("IsExpand"));
                }
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
        public void UpdateIdAndParentID()
        {
            OnPropertyChanged(("Id"));
            OnPropertyChanged(("ParentId"));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    public enum State
    {
        Expand,
        Collapse,
        None
    };

    public class MindmapDataItems : ObservableCollection<MindmapDataItem>
    {

    }
}
