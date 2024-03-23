using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HoneyBadger
{
    public class FindCurveInflection : GH_Component
    {

        public FindCurveInflection()
          : base(
                "FindCurveInflection", 
                "if",
                "Finds the locations of zero curvature along a nurbs curve",
                "HoneyBadger", 
                "Curves"
                )
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("Curve", "crv", "", GH_ParamAccess.item);    
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Points", "pts", "", GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Curve crv = null;
            Point3d[] pts = new Point3d[0];

            DA.GetData("Curve", ref crv);
            pts = crv.InflectionPoints();
            DA.SetDataList("Points", pts);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {

                return Properties.Resources.MinCurviturePoint;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("D1C79EE4-BD24-4D63-A6F2-1582F7134A4B"); }
        }
    }
}