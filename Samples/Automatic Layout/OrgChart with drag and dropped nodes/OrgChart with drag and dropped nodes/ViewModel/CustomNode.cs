using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgChart_with_drag_and_dropped_nodes.ViewModel
{
    public class CustomNode : NodeViewModel
    {
        private string _Id;
        private string _ParentId;

        public string Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (value != _Id)
                {
                    _Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public string ParentId
        {
            get
            {
                return _ParentId;
            }
            set
            {
                if (value != _ParentId)
                {
                    _ParentId = value;
                    OnPropertyChanged("ParentId");
                }
            }
        }
    }
}
