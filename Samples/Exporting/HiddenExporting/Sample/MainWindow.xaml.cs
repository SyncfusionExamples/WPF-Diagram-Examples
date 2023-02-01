using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Layout;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace HiddenExport
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            diagram.Loaded += Diagram_Loaded;
        }

        private void Diagram_Loaded(object sender, RoutedEventArgs e)
        {
            diagram.Visibility= Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            outputAddress.Text = string.Empty;
            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = myDocumentsPath + "\\" + (cmbDataSource.SelectedItem is ComboBoxItem items ? items.Content.ToString() : string.Empty);
            diagram.ExportSettings.FileName = fileName;
            diagram.ExportSettings.ExportType = ExportType.PNG;
            diagram.Export();

            outputAddress.Text = fileName;
        }

        private void cmbDataSource_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is ComboBoxItem selectedItem)
            {
                switch (selectedItem.Content.ToString())
                {
                    case "DataSource 1":
                        this.UpdateDataSource1();
                        break;

                    case "DataSource 2":
                        this.UpdateDataSource2();
                        break;
                }

                diagram.LayoutManager.Layout.UpdateLayout();
            }
        }


        private void UpdateDataSource1()
        {
            Employees employees = new Employees();

            employees.Add(new Employee()
            {
                Name = "Steve",
                EmpId = "1",
                ParentId = "",
                Designation = "CEO"
            });
            employees.Add(new Employee()
            {
                Name = "Kevin",
                EmpId = "2",
                ParentId = "1",
                Designation = "Manager"
            });
            employees.Add(new Employee()
            {
                Name = "John",
                EmpId = "3",
                ParentId = "1",
                Designation = "Manager"
            });
            employees.Add(new Employee()
            {
                Name = "Raj",
                EmpId = "4",
                ParentId = "2",
                Designation = "Team Lead"
            });
            employees.Add(new Employee()
            {
                Name = "Will",
                EmpId = "5",
                ParentId = "2",
                Designation = "S/w Developer"
            });
            employees.Add(new Employee()
            {
                Name = "Sarah",
                EmpId = "6",
                ParentId = "3",
                Designation = "TeamLead"
            });
            employees.Add(new Employee()
            {
                Name = "Mike",
                EmpId = "7",
                ParentId = "3",
                Designation = "Testing Engineer"
            });

            diagram.DataSourceSettings = new DataSourceSettings()
            {
                Id = "EmpId",
                ParentId = "ParentId",
                DataSource = employees,
            };

            diagram.LayoutManager = new LayoutManager()
            {
                Layout = new DirectedTreeLayout()
                {
                    Type = LayoutType.Hierarchical,
                    Orientation = TreeOrientation.TopToBottom,
                    HorizontalSpacing = 80,
                    VerticalSpacing = 80,
                },
            };
        }

        private void UpdateDataSource2()
        {
            DataItems data = new DataItems();

            data.Add(new ItemInfo("n11", "#ff6329"));

            data.Add(new ItemInfo("n12", "#ff6329"));

            data.Add(new ItemInfo("n13", "#ff6329"));

            data.Add(new ItemInfo("n21", "#941100") { ReportingPerson = new List<string> { "n11", "n12", "n13" } });

            data.Add(new ItemInfo("n31", "#669be5") { ReportingPerson = new List<string> { "n21" } });

            data.Add(new ItemInfo("n32", "#669be5") { ReportingPerson = new List<string> { "n21" } });

            data.Add(new ItemInfo("n41", "#30ab5c") { ReportingPerson = new List<string> { "n31" } });

            data.Add(new ItemInfo("n42", "#30ab5c") { ReportingPerson = new List<string> { "n31" } });

            data.Add(new ItemInfo("n43", "#30ab5c") { ReportingPerson = new List<string> { "n31" } });

            data.Add(new ItemInfo("n44", "#30ab5c") { ReportingPerson = new List<string> { "n31", "n32" } });

            data.Add(new ItemInfo("n45", "#30ab5c") { ReportingPerson = new List<string> { "n32" } });

            data.Add(new ItemInfo("n46", "#30ab5c") { ReportingPerson = new List<string> { "n32" } });

            data.Add(new ItemInfo("n47", "#30ab5c") { ReportingPerson = new List<string> { "n32" } });

            data.Add(new ItemInfo("n51", "#ff9400") { ReportingPerson = new List<string> { "n41", "n42", "n43" } });

            data.Add(new ItemInfo("n52", "#ff9400") { ReportingPerson = new List<string> { "n45", "n46", "n47" } });

            data.Add(new ItemInfo("n61", "#99bb55") { ReportingPerson = new List<string> { "n51" } });

            data.Add(new ItemInfo("n62", "#99bb55") { ReportingPerson = new List<string> { "n51" } });

            data.Add(new ItemInfo("n63", "#99bb55") { ReportingPerson = new List<string> { "n51", "n44" } });

            data.Add(new ItemInfo("n64", "#99bb55") { ReportingPerson = new List<string> { "n44", "n52" } });

            data.Add(new ItemInfo("n65", "#99bb55") { ReportingPerson = new List<string> { "n52" } });

            data.Add(new ItemInfo("n66", "#99bb55") { ReportingPerson = new List<string> { "n52" } });

            diagram.DataSourceSettings = new DataSourceSettings()
            {
                ParentId = "ReportingPerson",
                Id = "Name",
                DataSource = data,
            };

            // Initialize LayoutSettings for SfDiagram
            diagram.LayoutManager = new LayoutManager()
            {
                Layout = new DirectedTreeLayout()
                {
                    Type = LayoutType.Hierarchical,
                    Orientation = TreeOrientation.TopToBottom,
                    AvoidSegmentOverlapping = true,
                    HorizontalSpacing = 80,
                    VerticalSpacing = 80,
                },
            };
        }
    }

    public class Employee
    {
        public string Name { get; set; }
        public string EmpId { get; set; }
        public string ParentId { get; set; }
        public string Designation { get; set; }
    }

    public class Employees : ObservableCollection<Employee>
    {

    }

    public class ItemInfo
    {
        public ItemInfo(string name, string color)
        {
            this.Name = name;
            this.RatingColor = color;
        }

        public string RatingColor { get; set; }
        public string Name { get; set; }
        public List<string> ReportingPerson { get; set; }
    }

    public class DataItems : ObservableCollection<ItemInfo>
    {

    }
}
