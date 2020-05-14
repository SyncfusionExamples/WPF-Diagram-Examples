#region Copyright Syncfusion Inc. 2001-2020.
// Copyright Syncfusion Inc. 2001-2020. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using FlowchartCreator.Utility;
using Microsoft.Win32;
//using Syncfusion.Pdf;
using Syncfusion.UI.Xaml.Diagram;
//using Syncfusion.XPS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace FlowchartCreator.ViewModel
{
    /// <summary>
    /// Custom diagram view model class
    /// </summary>
    public class DiagramVM : DiagramViewModel
    {
        private string _exporttype;
        private List<string> _mexporttype = new List<string> { "PNG", "JPG", "TIF", "GIF", "BMP", "WDP", "XPS", "PDF" };

        public List<string> ExportTypes
        {
            get
            {
                return _mexporttype;
            }
        }

        public string ExportType
        {
            get
            {
                return _exporttype;
            }
            set
            {
                if(_exporttype != value)
                {
                    _exporttype = value;
                    OnPropertyChanged("ExportType");
                }
            }
        }

        public DiagramVM()
        {
            PrintingService = new PrintingService();
            //Custom command to execute export action
            LoadCommand = new Command(OnLoadCommand);
            SaveCommand = new Command(OnSaveCommand);
            ExportCommand = new Command(OnExported);
            PrintClickCommand = new Command(OnPrintCommand);
            DropCommand = new Command(OnItemDroped);
            ItemAddedCommand = new Command(OnItemAdded);

            
        }
        public ICommand LoadCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ExportCommand { get; set; }
        public ICommand PrintClickCommand { get; set; }


        private void OnLoadCommand(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "XAML File (*.xaml)|*.xaml";
            if (dialog.ShowDialog() == true)
            {
                using (Stream myStream = dialog.OpenFile())
                {
                    (Info as IGraphInfo).Load(myStream);
                }
            }
        }
        private void OnSaveCommand(object obj)
        {
            //To Represent SaveFile Dialog Box
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save XAML";
            dialog.Filter = "XAML File (*.xaml)|*.xaml";
            if (dialog.ShowDialog() == true)
            {
                using (Stream str = File.Open(dialog.FileName, FileMode.OpenOrCreate))
                {
                    (Info as IGraphInfo).Save(str);
                }
            }
        }
        private void OnExported(object ob)
        {
            String obj = this.ExportType;
            String extension = ("PNG File(*.png)|*.png");
            //Assigning Extension based on the chosen exporttype.
            switch (obj)
            {
                case "BMP":
                    extension = ("BMP File(*.bmp)|*.bmp");
                    break;
                case "WDP":
                    extension = ("WDP File(*.wdp)|*.wdp");
                    break;
                case "JPG":
                    extension = ("JPG File(*.jpg)|*.jpg");
                    break;
                case "PNG":
                    extension = ("PNG File(*.png)|*.png");
                    break;
                case "TIF":
                    extension = ("TIF File(*.tif)|*.tif");
                    break;
                case "GIF":
                    extension = ("GIF File(*.gif)|*.gif");
                    break;
                case "XPS":
                    extension = ("XPS File(*.xps)|*.xps");
                    break;
                case "PDF":
                    extension = ("PDF File(*.pdf)|*.pdf");
                    break;
            }
                
            SaveFile(extension);
        }


        private void SaveFile(string filter)
        {
            //To Represent SaveFile Dialog Box
            SaveFileDialog m_SaveFileDialog = new SaveFileDialog();                     

            //Assign the selected extension to the SavefileDialog filter
            m_SaveFileDialog.Filter = filter;

            //To display savefiledialog       
            bool? istrue = m_SaveFileDialog.ShowDialog();

            string filenamechanged;

            if (istrue == true)
            {
                //assign the filename to a local variable
                string extension = System.IO.Path.GetExtension(m_SaveFileDialog.FileName).TrimStart('.');
                string fileName = m_SaveFileDialog.FileName;
                if (extension != "")
                {
                    if (extension.ToLower() != ExportType.ToLower())
                    {
                        fileName = fileName + "." + ExportType.ToLower();
                    }

                    if (ExportType.ToLower() == "pdf")
                    {
                        filenamechanged = fileName + ".xps";

                        ExportSettings.IsSaveToXps = true;

                        //Assigning exportstream from the saved file
                        this.ExportSettings.FileName = filenamechanged;
                        // Method to Export the SfDiagram
                        (this.Info as IGraphInfo).Export();

                        //var converter = new XPSToPdfConverter { };
                        
                        //var document = new PdfDocument();

                        //document = converter.Convert(filenamechanged);
                        //document.Save(fileName);
                        //document.Close(true);

                    }

                    else
                    {
                        if (ExportType.ToLower() == "xps")
                        {
                            ExportSettings.IsSaveToXps = true;
                        }
                        //Assigning exportstream from the saved file
                        this.ExportSettings.FileName = fileName;
                        // Method to Export the SfDiagram
                        (this.Info as IGraphInfo).Export();
                    }
                }
            }

        }

        //Method to execute Print action
        private void OnPrintCommand(object obj)
        {
            PrintingService.ShowDialog = true;
            PrintingService.Print();
        }

        /// <summary>
        /// The method will be invoked when any item is added in diagram
        /// </summary>
        /// <param name="obj"></param>
        private void OnItemAdded(object obj)
        {
            ItemAddedEventArgs args = obj as ItemAddedEventArgs;
            if (args != null && args.Item is NodeViewModel)
            {
                //Enble the droping of one node to another node
                (args.Item as NodeViewModel).Constraints = (args.Item as NodeViewModel).Constraints.Add(NodeConstraints.AllowDrop);
            }
        }

        /// <summary>
        /// The method will be invoked when node is dropped on to another node.
        /// </summary>
        /// <param name="obj">Item dropped event argument</param>
        private void OnItemDroped(object obj)
        {
            ItemDropEventArgs args = obj as ItemDropEventArgs;
            if (args != null)
            {
                if (!(args.Target is SfDiagram))
                {
                    foreach (object targetElement in args.Target as IEnumerable<object>)
                    {
                        if (targetElement is NodeViewModel)
                        {
                            //Newl dropped node
                            NodeViewModel sourcenode = args.Source as NodeViewModel;
                            //Existing node
                            NodeViewModel targetnode = targetElement as NodeViewModel;
                            //Update the new node's offset x and y values
                            sourcenode.OffsetX = targetnode.OffsetX;
                            sourcenode.OffsetY = targetnode.OffsetY + 130;
                            //Create connection from exiting node to new node
                            ConnectorViewModel connectorVM = new ConnectorViewModel()
                            {
                                SourceNode = targetnode,
                                TargetNode = sourcenode
                            };
                            (Connectors as ObservableCollection<ConnectorViewModel>).Add(connectorVM);
                        }
                    }
                }
            }
        }
    }
}
