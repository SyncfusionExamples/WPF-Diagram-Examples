using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortToolOverrideSample
{
    public class CustomDiagram : SfDiagram
    {
        // Method to override basic functions in SfDiagram
        protected override void SetTool(SetToolArgs args)
        {
            // Verify the source is Port
            if (args.Source is IPort)
            {
                // Set the action as Drag
                args.Action = ActiveTool.Drag;
            }
            else
            {
                // Call base method
                base.SetTool(args);
            }
        }
    }
}
