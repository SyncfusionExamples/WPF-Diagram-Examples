using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SetCursorKBSample
{
    /// <summary>
    /// Create custom class of SfDiagram to override the cursor
    /// </summary>
    public class CustomClass : SfDiagram
    {
        //Override method to customize the default actions of diagram objects
        protected override void SetTool(SetToolArgs args)
        {
            if (args.Source is INode)
            {
                args.Action = ActiveTool.Pan;
            }
            else if (args.Source is IConnector)
            {
                args.Action = ActiveTool.Draw;
            }
            else if (args.Source is IPort)
            {
                args.Action = ActiveTool.Drag;
            }
            else
            {
                base.SetTool(args);
            }
        }
    }
}
