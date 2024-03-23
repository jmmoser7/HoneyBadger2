using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HoneyBadger
{
    public class FindCurveMaxima : GH_Component
    {

        public FindCurveMaxima()
          : base("FindCurveMaxima", 
                 "MaxHeadroom",
                 "Finds points of maximum curvature along a nurbs curve",
                 "HoneyBadger", 
                 "Curves")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("Curve", "cv", "", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("MaxPoints", "mp","",GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Curve myCurve = null;
            Point3d[] pts = new Point3d[0];
            
            DA.GetData("Curve", ref myCurve);
            pts = myCurve.MaxCurvaturePoints();
            DA.SetDataList("MaxPoints", pts);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {

                return Properties.Resources.MaxCurviturePoint;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("3057FF95-A7A6-498C-97D4-B7BD57152DDA"); }
        }
    }
}