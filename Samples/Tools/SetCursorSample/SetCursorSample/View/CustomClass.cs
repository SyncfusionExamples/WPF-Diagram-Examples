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
        //Override method to customize the default cursors of diagram objects
        protected override void SetCursor(SetCursorArgs args)
        {
            if (args.Source is INode)
            {
                args.Cursor = Cursors.No;
            }
            else if (args.Source is IConnector)
            {
                args.Cursor = Cursors.Hand;
            }
            else if (args.Source is IPort)
            {
                args.Cursor = Cursors.SizeAll;
            }
            else
            {
                base.SetCursor(args);
            }
        }

    }
}
