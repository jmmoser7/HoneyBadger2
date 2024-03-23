using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HoneyBadger
{
    public class SequentialCurveFlipper : GH_Component
    {

        public SequentialCurveFlipper()
          : base(
                "SequentialCurveFlipper", 
                "Flippy",
                "a node that marches through a list and sequentially flips curves to point in the same direction as the one tha preceded them.",
                "HoneyBadger",  
                "Curves"
                )
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("curve", "c", "", GH_ParamAccess.list);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddCurveParameter("flipedCurve", "Fc", "", GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Curve> iCurves = new List<Curve>();
            List<Curve> flipedCrv = new List<Curve>();

            DA.GetDataList("curve", iCurves);

            for (int i = 0; i < iCurves.Count; i++)
            {
                if (i == 0)
                {
                    flipedCrv.Add(iCurves[i]);
                }
                else
                {
                    Point3d p1 = new Point3d(iCurves[i].PointAtStart);
                    Point3d p2 = new Point3d(iCurves[i].PointAtEnd);
                    Point3d p3 = new Point3d(iCurves[i - 1].PointAtStart);
                    Point3d p4 = new Point3d(iCurves[i - 1].PointAtEnd);

                    Vector3d v1 = new Vector3d(p2 - p1);
                    Vector3d v2 = new Vector3d(p4 - p3);

                    v1.Unitize();
                    v2.Unitize();

                    Vector3d v3 = new Vector3d(v1 + v2);
                    double length = v3.Length;

                    if (length < 1.414)
                    {
                        iCurves[i].Reverse();
                        flipedCrv.Add(iCurves[i]);
                    }
                    else
                    {
                        flipedCrv.Add(iCurves[i]);
                    }
                }
            }
            DA.SetDataList("flipedCurve", flipedCrv);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {

                return Properties.Resources.CurveFlipper;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("596E7D87-6354-407A-BAA6-B475F4DA3223"); }
        }
    }
}