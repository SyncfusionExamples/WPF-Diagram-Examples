using HierarchicalTree.Model;
using HierarchicalTree.Utility;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Layout;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace HierarchicalTree.ViewModel
{
    public class DiagramVM : DiagramViewModel
    {        
        public DiagramVM()
        {
            PageSettings = new PageSettings()
            {
                PageBorderBrush = new SolidColorBrush(Colors.Transparent),
                PageBackground = new SolidColorBrush(Colors.White)
            };
            ScrollSettings = new ScrollSettings()
            {
                ScrollLimit = ScrollLimit.Diagram
            };
            Menu = null;
            DataSourceSettings = new DataSourceSettings()
            {
                ParentId = "ParentId",
                Id = "EmpId",
                DataSource = GetData()
            };
            LayoutManager = new Syncfusion.UI.Xaml.Diagram.Layout.LayoutManager()
            {
                Layout = new DirectedTreeLayout()
                {
                    Type = LayoutType.Hierarchical,
                    Orientation = TreeOrientation.LeftToRight,
                }
            };
            
            ItemDeletingCommand = new Command(OnItemDeleting);
           
        }

        private void OnItemDeleting(object args)
        {
            (args as ItemDeletingEventArgs).DeleteSuccessors = true;
        }

        /// <summary>
        /// Method to Get Data for DataSource
        /// </summary>
        /// <param name="data"></param>
        private Employees GetData()
        {
            Employees ItemsInfo = new Employees();
            #region flow
            ItemsInfo.Add(new Employee()
            {
                EmpId = "1",
                ParentId = "",
                Name = "Plant Manager",
                _Color = "#034d6d"
            });

            ItemsInfo.Add(new Employee()
            {
                EmpId = "2",
                ParentId = "1",
                Name = "Production Manager",
                _Color = "#1b80c6"
            });

            ItemsInfo.Add(new Employee()
            {
                EmpId = "3",
                ParentId = "1",
                Name = "Administrative Officer",
                _Color = "#1b80c6"
            });

            ItemsInfo.Add(new Employee()
            {
                EmpId = "4",
                ParentId = "1",
                Name = "Maintenance Manager",
                _Color = "#1b80c6"
            });

            ItemsInfo.Add(new Employee()
            {
                EmpId = "5",
                ParentId = "2",
                Name = "Control Room",
                _Color = "#3dbfc9"
            });

            ItemsInfo.Add(new Employee()
            {
                EmpId = "6",
                ParentId = "2",
                Name = "Plant Operator",
                _Color = "#3dbfc9"
            });

            ItemsInfo.Add(new Employee()
            {
                EmpId = "7",
                ParentId = "4",
                Name = "Electrical Supervisor",
                _Color = "#3dbfc9"
            });

            ItemsInfo.Add(new Employee()
            {
                EmpId = "8",
                ParentId = "4",
                Name = "Mechanical Supervisor",
                _Color = "#3dbfc9"
            });

            ItemsInfo.Add(new Employee()
            {
                EmpId = "9",
                ParentId = "5",
                Name = "Foreman",
                _Color = "#2bb28e"
            });

            ItemsInfo.Add(new Employee()
            {
                EmpId = "10",
                ParentId = "6",
                Name = "Foreman",
                _Color = "#2bb28e"

            });

            ItemsInfo.Add(new Employee()
            {
                EmpId = "11",
                ParentId = "7",
                Name = "Craft Personnel",
                _Color = "#2bb28e"

            });

            ItemsInfo.Add(new Employee()
            {
                EmpId = "12",
                ParentId = "7",
                Name = "Craft Personnel",
                _Color = "#2bb28e"
            });

            ItemsInfo.Add(new Employee()
            {
                EmpId = "13",
                ParentId = "8",
                Name = "Craft Personnel",
                _Color = "#2bb28e"
            });

            ItemsInfo.Add(new Employee()
            {
                EmpId = "14",
                ParentId = "8",
                Name = "Craft Personnel",
                _Color = "#2bb28e"
            });

            ItemsInfo.Add(new Employee()
            {
                EmpId = "15",
                ParentId = "9",
                Name = "Craft Personnel",
                _Color = "#76d13b"
            });
            ItemsInfo.Add(new Employee()
            {
                EmpId = "16",
                ParentId = "9",
                Name = "Craft Personnel",
                _Color = "#76d13b"
            });
            ItemsInfo.Add(new Employee()
            {
                EmpId = "17",
                ParentId = "10",
                Name = "Craft Personnel",
                _Color = "#76d13b"
            });
            #endregion

            return ItemsInfo;
        }

    }
}
