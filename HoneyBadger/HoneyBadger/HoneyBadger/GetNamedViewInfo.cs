using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;

namespace HoneyBadger

#region Methods of GH_Component interface
{
    public class VariableInputTest : GH_Component, IGH_VariableParameterComponent
    {

        public VariableInputTest()
          : base
            (
                "VariableInputTest",
                "Nickname",
                "Description",
                "HoneyBadger",
                "Views"
            )
        {
        }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
        }


        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            
        }


        protected override void SolveInstance(IGH_DataAccess DA)
        {
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
            get { return new Guid("a96dfc6b-da40-4ad5-a5fe-a3e7a7e70235"); }
        }
        #endregion

        #region Methods of IGH_VariableParameterComponent Interface
        // i think ths creats a boolian that talks to the kirnal and tels it weather it is alloud to add inputs
        bool IGH_VariableParameterComponent.CanInsertParameter(GH_ParameterSide side, int index)
        {
            if (side == GH_ParameterSide.Input)
            {
                return true;
            }
            else
            {
                return false;
            }
        }       
        //creats another boolian that keeps you from removing non existnt inputs
        bool IGH_VariableParameterComponent.CanRemoveParameter(GH_ParameterSide side, int index)
        {
            if (side == GH_ParameterSide.Input && Params.Input.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //does the heavy lifting of creating a new paramiter.
        IGH_Param IGH_VariableParameterComponent.CreateParameter(GH_ParameterSide side, int index)
        {
            Param_Number param = new Param_Number();

            param.Name = GH_ComponentParamServer.InventUniqueNickname("ABCDEFGHIJKLMNOPQRSTUVWXYZ", Params.Input);
            param.NickName = param.Name;
            param.Description = "Param" + (Params.Input.Count + 1);
            param.SetPersistentData(0.0);

            return param;

        }
        bool IGH_VariableParameterComponent.DestroyParameter(GH_ParameterSide side, int index)
        {
            return true;
        }
        void IGH_VariableParameterComponent.VariableParameterMaintenance()
        {
        }

        


        #endregion
    }
}