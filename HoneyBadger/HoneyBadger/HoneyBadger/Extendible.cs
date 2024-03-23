using System;
using System.Windows.Forms;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;

namespace HoneyBadger
{
    public class MyComponent : GH_Component
    {
        private bool _showMenuOption = false;

        public MyComponent() : base
            (
                "VariableInputTestai",
                "Nickname",
                "Description",
                "HoneyBadger",
                "Views"
            ) { }

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("Show Menu", "Show", "Toggle the visibility of the custom menu option", GH_ParamAccess.item, false);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Output", "Out", "The output of the custom menu option", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            bool showMenu = false;
            if (!DA.GetData(0, ref showMenu)) return;

            _showMenuOption = showMenu;

            if (showMenu)
            {
                DA.SetData(0, "Custom menu option is visible");
            }
            else
            {
                DA.SetData(0, "Custom menu option is hidden");
            }
        }
        public override bool AppendMenuItems(ToolStripDropDown menu)
        {
            if (_showMenuOption)
            {
                Menu_AppendItem(menu, "My Custom Menu Option", MyCustomMenuOption, true, true);
                return true;
            }
            return false;
        }
        /*public override bool AppendMenuItems(ToolStripDropDown menu)
        {
            if (_showMenuOption)
            {
                Menu_AppendItem(menu, "My Custom Menu Option", MyCustomMenuOption, true, true);
                return true;
            }
            return false;
        }*/

        private void MyCustomMenuOption(object sender, EventArgs e)
        {
            // Place your custom menu option functionality here
            Rhino.RhinoApp.WriteLine("Custom menu option selected");
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return null;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("2B83B92D-AFB5-418F-983D-E114E6852202"); }
        }
    }
}
