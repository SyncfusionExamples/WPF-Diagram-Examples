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
                    if (value == null)
                    {
                        _parent.Children.Remove(this);
                        _parent = value;
                    }
                    else
                    {
                        MindmapDataItem oldParent = _parent;
                        int index = oldParent.Children.IndexOf(this);
                        oldParent.Children.Remove(this);
                        _parent = value;
                        _parent.Children.Add(this);
                        this.UpdateIdAndParentID();
                        if (index < oldParent.Children.Count)
                        {
                            oldParent.Children[index].UpdateIdAndParentID();
                        }
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
        public string Shape
        {
            get
            {
                return GetShape();
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
        private string GetShape()
        {
            switch (Level)
            {
                case 0:
                    //clound
                    return "M14.921997,0 C16.337997,1.5650994E-07 17.527,0.82099953 17.873001,1.9339985 18.344002,1.0140004 19.419998,0.37099861 20.673996,0.37099838 22.209,0.37099861 23.476997,1.3349995 23.694,2.5890017 24.308998,2.1370016 25.144997,1.8559996 26.068001,1.8559994 27.955002,1.8559996 29.484001,3.0210007 29.484001,4.457002 29.484001,5.0169996 29.248001,5.5350011 28.851997,5.9600008 30.625999,6.0850008 32,6.9429987 32,7.984999 32,9.1130055 30.390999,10.027999 28.404999,10.027999 27.289001,10.027999 26.290001,9.7390049 25.630997,9.2850026 L25.154999,9.2850026 C25.160995,9.346999 25.168999,9.4080037 25.168999,9.4709996 25.168999,11.009002 23.237999,12.257003 20.853996,12.257003 19.693001,12.257003 18.640999,11.958999 17.864998,11.477004 17.152,12.380004 15.700996,13 14.023003,13 11.918999,13 10.167999,12.026 9.7879982,10.740004 9.3050003,11.634002 8.2449989,12.257003 7.0119972,12.257003 5.3239975,12.257003 3.9559975,11.093001 3.9559975,9.6570042 3.9559975,9.6480015 3.9570007,9.6400059 3.9570007,9.6310032 3.7799988,9.6470021 3.6009979,9.6570042 3.4160004,9.6570042 1.5299988,9.6570042 0,8.8250035 0,7.799002 0,6.774 1.5299988,5.9420031 3.4160004,5.9420031 3.4769974,5.9420031 3.5359993,5.9460009 3.5960007,5.9470003 L3.5960007,5.8690012 C2.1459999,5.6320012 1.0789986,4.8410009 1.0789986,3.8989993 1.0789986,2.7720002 2.6079979,1.8559996 4.4949989,1.8559994 5.0909996,1.8559996 5.6520004,1.949002 6.1399994,2.1090017 6.6860008,1.2949986 7.757,0.7420009 8.9889984,0.74200108 10.272999,0.7420009 11.379997,1.3450017 11.902,2.2160006 12.121002,0.96199837 13.387001,1.5650994E-07 14.921997,0 z";
                case 1:
                    //Starburst
                    return "M13.246002,0 L16.058998,3.4279938 21.449997,0.45700073 20.747002,4.5709991 28.249001,2.0569916 26.139999,5.9420013 32,5.7139893 26.843002,8.2279968 32,10.513992 24.029999,10.742996 27.780998,14.399994 19.574997,11.884995 19.574997,15.770996 16.058998,13.028 11.136002,16 11.136002,11.884995 2.9300003,14.399994 6.7989998,10.856995 0.23500061,9.4850006 4.6889992,7.8849945 0,4.4570007 7.0330009,5.3710022 6.0950012,1.2569885 11.136002,4.5709991 z"; ;
                case 2:
                    //Rectangle
                    return "M0.5,0.5L25.5,0.5L25.5,25.557L0.5,25.557z";
            }
            //Oval
            return "M15.999998,0 C24.837005,-1.2914262E-07 31.999999,2.9099997 31.999999,6.4999999 31.999999,10.09 24.837005,13 15.999998,13 7.1639984,13 0,10.09 0,6.4999999 0,2.9099997 7.1639984,-1.2914262E-07 15.999998,0 z";
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
    public class MindmapDataItems : ObservableCollection<MindmapDataItem>
    {

    }
}
