using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HoneyBadger
{
    public class MakeArclikecs : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MakeArclikecs class.
        /// </summary>
        public MakeArclikecs()
          : base("MakeArclikecs", "Nickname",
              "Description",
              "HoneyBadger", "Curves")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("curves", "c", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("Tolerance", "t", "", GH_ParamAccess.item);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddCurveParameter("Arcs", "a", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Curve inCurve = null;
            Curve outCurve = null;  
            double inNum = 0.1;   

            DA.GetData("curves", ref inCurve);
            DA.GetData("Tolerance", ref inNum);
            outCurve = inCurve.ToArcsAndLines(inNum, 0.1, 0.5, 10);
            DA.SetData("Arcs", outCurve);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("88E1FA62-AB8C-4E81-B58B-132C28FF132D"); }
        }
    }
}