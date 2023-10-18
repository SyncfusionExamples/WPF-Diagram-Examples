using Microsoft.Win32;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Packaging;
using System.Linq;
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

namespace SerializeMultipleDiagramPages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SelectedDiagram _seletedDiagram;

        /// <summary>
        /// Gets or sets the current diagram page in the multiple diagrams
        /// </summary>
        public SelectedDiagram SelectedDiagram
        {
            get { return _seletedDiagram; }
            set
            {
                _seletedDiagram = value;
            }
        }

        /// <summary>
        /// Gets or sets the diagram collection.
        /// </summary>
        public ObservableCollection<SelectedDiagram> Diagrams { get; set; }

        //To save xml file names as strings while save the diagram pages
        List<string> xamlFiles = new List<string>();

        public MainWindow()
        {
            InitializeComponent();

            //Initialize the mutiple diagram collections
            Diagrams = new ObservableCollection<SelectedDiagram>();
            //Initialize the current selected diagram page
            SelectedDiagram = new SelectedDiagram() { Name = "DiagramPage" + (Diagrams.Count + 1) };
            //Assign diagram collections as item source of the tab control
            tab.ItemsSource = Diagrams;
           //adding current diagram item into diagram collection
            Diagrams.Add(SelectedDiagram);
            //Selected item changed event to the tab item
            tab.SelectedItemChangedEvent += Tab_SelectedItemChangedEvent;
        }

        
        private void Tab_SelectedItemChangedEvent(object sender, SelectedItemChangedEventArgs e)
        {
            //setting currently selected diagram from tab control
            SelectedDiagram = (e.NewSelectedItem as TabItemExt).Content as SelectedDiagram;
            (e.NewSelectedItem as TabItemExt).Header = SelectedDiagram.Name;
        }

        private void newPage_Click(object sender, RoutedEventArgs e)
        {
            //creating new diagram page
            Diagrams.Add(new SelectedDiagram() { Name = "DiagramPage" + (Diagrams.Count + 1) });
        }

        private void removePage_Click(object sender, RoutedEventArgs e)
        {
            //To remove current diagram item from diagram collection
            Diagrams.Remove(SelectedDiagram);
        }

        //This method to save single diagram page in desired location by using save dialog popup.
        private void save_Click(object sender, RoutedEventArgs e)
        {
            //To Save as stream in file
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save XAML";
            dialog.Filter = "XAML File (*.xaml)|*.xaml";
            if (dialog.ShowDialog() == true)
            {
                using (Stream str = File.Open(dialog.FileName, FileMode.CreateNew))
                {
                    //saving diagram as stream file.
                    SelectedDiagram.Save(str);
                }
            }
        }

        //Method to load single diagram page from saved location using save promote dialog.
        private void load_Click(object sender, RoutedEventArgs e)
        {
            //Load from saved XAML file
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                using (Stream myStream = dialog.OpenFile())
                {
                    //loading strem file in currnelt selected diagram page
                    SelectedDiagram.Load(myStream);
                }
            }
        }

        private void savePackage_Click(object sender, RoutedEventArgs e)
        {
            CreateSFDPackage("ConsolidatedDiagram.sfd", xamlFiles);
            MessageBox.Show("Pages are saved as single package!", "Configuration", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //To save multiple diagram pages as single package using stream file.
        public void CreateSFDPackage(string packageFilePath, List<string> xamlFiles)
        {
            //Saving all diagram pages as individual file using stream.
            int i = 1;
            foreach(SelectedDiagram diagram in Diagrams)
            {
                using (Stream str = File.Open("Diagram" + i + ".xaml", FileMode.CreateNew))
                {
                    xamlFiles.Add("Diagram" + i + ".xaml");
                    diagram.Save(str);
                }

                i++;
            }

            //To store all saved diagram files into single package
            using (Package package = Package.Open(packageFilePath, FileMode.Create))
            {
                foreach (var xamlFile in xamlFiles)
                {
                    Uri partUri = PackUriHelper.CreatePartUri(new Uri(System.IO.Path.GetFileName(xamlFile), UriKind.Relative));
                    PackagePart packagePart = package.CreatePart(partUri, System.Net.Mime.MediaTypeNames.Text.Xml);

                    using (FileStream fileStream = new FileStream(xamlFile, FileMode.Open, FileAccess.Read))
                    {
                        using (Stream partStream = packagePart.GetStream())
                        {
                            fileStream.CopyTo(partStream);
                        }
                    }
                }
            }
        }

        private void loadPackage_Click(object sender, RoutedEventArgs e)
        {
            OpenSFDPackage("ConsolidatedDiagram.sfd");
        }

        //To load the saved diagram package into multiple diagram pages
        public void OpenSFDPackage(string packageFilePath)
        {
            //To clear the diagram items.
            Diagrams.Clear();
            using (Package package = Package.Open(packageFilePath, FileMode.Open, FileAccess.Read))
            {
                foreach (PackagePart part in package.GetParts())
                {
                    if (part.ContentType == System.Net.Mime.MediaTypeNames.Text.Xml)
                    {
                        using (Stream partStream = part.GetStream())
                        {
                            Diagrams.Add(new SelectedDiagram() { Name = "DiagramPage" + (Diagrams.Count + 1) });
                            SelectedDiagram.Load(partStream);
                        }
                    }
                }
            }
        }
    }

    public class SelectedDiagram : SfDiagram
    {
        public SelectedDiagram()
        {
            Nodes = new ObservableCollection<NodeViewModel>();
            Connectors = new ObservableCollection<ConnectorViewModel>();
            Groups = new ObservableCollection<GroupViewModel>();
        }
    }
}
