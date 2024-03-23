using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using System.Windows.Forms;
using Grasshopper.GUI;


namespace HoneyBadger
{
    public class IsArcLike : GH_Component, IGH_VariableParameterComponent
    {

        public IsArcLike()
          : base("IsArcWithTolerence", "Arc?", "Description", "HoneyBadger", "Curves")
        {
        }
        private int inputIndex = 0;
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("crv", "C", "", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBooleanParameter("isArc", "A", "", GH_ParamAccess.item);            
        }
       
        
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double myNum = new Double();

            if (Params.Input.Count == 2) { DA.GetData("Tolerence", ref myNum); }
            else { myNum = 0.001; }

            Curve myCurve = null;
            DA.GetData("crv", ref myCurve);
            if (myCurve != null)
            {
                if (myCurve.GetType().BaseType == typeof(Curve))
                {
                    bool myBool = myCurve.IsArc(Params.Input.Count == 1 ? 0.001 : myNum);
                    DA.SetData("isArc", myBool);
                }
                else { DA.SetData("isArk", null); }
            }
            else { DA.SetData("isArk", null); }
     
        }
        protected override System.Drawing.Bitmap Icon{get{ return Properties.Resources.IsCurveArkLike;}}
        public override Guid ComponentGuid{ get { return new Guid("EBFF7AEC-98F5-4BEF-BEAA-0AFECEA18D32"); }}

        bool IGH_VariableParameterComponent.CanInsertParameter(GH_ParameterSide side, int index)
        {

            if (side == GH_ParameterSide.Input && index == 1 && Params.Input.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool IGH_VariableParameterComponent.CanRemoveParameter(GH_ParameterSide side, int index)
        {
            if (side == GH_ParameterSide.Input && Params.Input.Count > 1 && index == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public IGH_Param CreateParameter(GH_ParameterSide side, int index)
        {
            inputIndex = index;
            //dropdownMenue();
            LoadOptionOne();
            return null;

        }
        public bool DestroyParameter(GH_ParameterSide side, int index) { return true; }
        public void VariableParameterMaintenance()
        {
        }
        protected override void AppendAdditionalComponentMenuItems(ToolStripDropDown menu)
        {

        }
        private void LoadOptionOne()
        {
            if (this.Params.Input.Count != 1)
            {
                Params.UnregisterInputParameter(Params.Input[1]);
            }

            Param_Number pn2 = new Param_Number
            {
                Name = "Tolerence",
                NickName = "t",
                Description = "Tolernece",
                Access = GH_ParamAccess.item,
                Optional = true
            };
            Params.RegisterInputParam(pn2, 1);
            Params.OnParametersChanged();
        }
    }
}