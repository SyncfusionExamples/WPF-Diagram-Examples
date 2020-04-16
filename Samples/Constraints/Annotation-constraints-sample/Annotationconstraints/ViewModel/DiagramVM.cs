using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Annotationconstraints.ViewModel
{
   public class DiagramVM:DiagramViewModel
    {
        #region Fields
                
        private bool _resize = false;
        private bool _edit = false;
        private bool _rotate = false;
        private bool _select = false;
        private bool _drag = false;

        #endregion

        AnnotationEditorViewModel anno;
        public DiagramVM()
        {
            Nodes = new NodeCollection();
            Connectors = new ConnectorCollection();

            anno = new AnnotationEditorViewModel()
            {
                Content = "Annoatation",
            };

            NodeViewModel node = new NodeViewModel()
            {
                OffsetX = 500,
                OffsetY = 250,
                UnitHeight = 100,
                UnitWidth = 100,               
                Annotations = new AnnotationCollection()
                {
                    anno,
                },                
            };            
            (Nodes as NodeCollection).Add(node);
        }

        #region Properties

        /// <summary>
        /// Gets or sets the value for Resize
        /// </summary>
        public bool Resize
        {
            get
            {
                return _resize;
            }
            set
            {
                if (value != _resize)
                {
                    _resize = value;
                    OnPropertyChanged("Resize");
                    OnResizeChanged(_resize);
                }
            }
        }

        /// <summary>
        /// Gets or sets the value for Rotate
        /// </summary>
        public bool Rotate
        {
            get
            {
                return _rotate;
            }
            set
            {
                if (value != _rotate)
                {
                    _rotate = value;
                    OnPropertyChanged("Rotate");
                    OnRotateChanged(_rotate);
                }
            }
        }

        /// <summary>
        /// Gets or sets the value for Edit
        /// </summary>
        public bool Edit
        {
            get
            {
                return _edit;
            }
            set
            {
                if (value != _edit)
                {
                    _edit = value;
                    OnPropertyChanged("Edit");
                    OnEditChanged(_edit);
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the value for Select
        /// </summary>
        public bool Select
        {
            get
            {
                return _select;
            }
            set
            {
                if (value != _select)
                {
                    _select = value;
                    OnPropertyChanged("Select");
                    OnSelectChanged(_select);
                }
            }
        }

        /// <summary>
        /// Gets or sets the value for Drag
        /// </summary>
        public bool Drag
        {
            get
            {
                return _drag;
            }
            set
            {
                if (value != _drag)
                {
                    _drag = value;
                    OnPropertyChanged("Drag");
                    OnDragChanged(_drag);
                }
            }
        }

        #endregion

        #region Helper Methods
     
        /// <summary>
        /// Method to change the value of Edit
        /// </summary>
        /// <param name="edit"></param>
        private void OnEditChanged(bool edit)
        {
            if (edit)
            {
                anno.Constraints = AnnotationConstraints.Editable;
            }
            else
            {
                anno.Constraints &=~ AnnotationConstraints.Editable;
            }
        }       

        /// <summary>
        /// Method to change the value of Resize
        /// </summary>
        /// <param name="resize"></param>
        private void OnResizeChanged(bool resize)
        {
            if (resize)
            {                
                anno.Constraints = AnnotationConstraints.Selectable|AnnotationConstraints.Resizable;
            }
            else
            {               
                anno.Constraints &=~ AnnotationConstraints.Resizable;
            }
        }

        /// <summary>
        /// Method to change the value of Select
        /// </summary>
        /// <param name="select"></param>
        private void OnSelectChanged(bool select)
        {
            if (select)
            {                
                anno.Constraints = AnnotationConstraints.Selectable;
            }
            else
            {
                anno.Constraints &= ~AnnotationConstraints.Selectable;
            }
        }

        /// <summary>
        /// Method to change the value of Rotate
        /// </summary>
        /// <param name="rotate"></param>
        private void OnRotateChanged(bool rotate)
        {
            if (rotate)
            {               
                anno.Constraints = AnnotationConstraints.Selectable|AnnotationConstraints.Rotatable;
            }
            else
            {
                anno.Constraints &= ~AnnotationConstraints.Rotatable;
            }
        }

        /// <summary>
        /// Method to change the value of Drag
        /// </summary>
        /// <param name="drag"></param>
        private void OnDragChanged(bool drag)
        {
            if (drag)
            {                
                anno.Constraints = AnnotationConstraints.Selectable | AnnotationConstraints.Draggable;
            }
            else
            {
                anno.Constraints &= ~AnnotationConstraints.Draggable;               
            }
        }

        #endregion
    }
}
