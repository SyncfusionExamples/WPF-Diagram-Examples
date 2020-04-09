using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardShortcut.ViewModel
{
    public class CustomNode : NodeViewModel
    {
        private string _Id;

        private string _ParentId;

        [DataMember]
        public string Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        [DataMember]
        public string ParentId
        {
            get
            {
                return _ParentId;
            }
            set
            {
                if (_ParentId != value)
                {
                    _ParentId = value;
                    OnPropertyChanged("ParentId");
                }
            }
        }

        protected override void OnPropertyChanged(string name)
        {
            base.OnPropertyChanged(name);
        }
    }
}


