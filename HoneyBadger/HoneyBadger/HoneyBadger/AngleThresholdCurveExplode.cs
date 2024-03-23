using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace HoneyBadger
{
    public class AngleThresholdCurveExplode : GH_Component
    {

        public AngleThresholdCurveExplode()
          : base(
                "ThresholdCurveExplode",
                "BoomBoom",
                "Splits a curve at all locations where kink exceeds a given angle.",
                "HoneyBadger",
                "Curves"
                )
        {
        }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("polyCurve", "Pc", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("angle", "a", "", GH_ParamAccess.item, 0.1);
        }


        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddCurveParameter("polyCurves", "Pc", "", GH_ParamAccess.list);
        }
       

        protected override void SolveInstance(IGH_DataAccess DA)
        {

            Curve crv = null;
            List<double> oRads = new List<double>();
            double iRad = new double();
            Curve[] sCurves = new Curve[0];
            List<Curve> cvs = new List<Curve>();
            double oRad = new double();
            List<Point3d> pts = new List<Point3d>();
            List<double> t = new List<double>();
            Curve[] exploded = new Curve[0];

            DA.GetData("polyCurve", ref crv);
            DA.GetData("angle", ref oRad);

            sCurves = crv.DuplicateSegments();



            for (int i = 0; i < sCurves.Length; i++)
            {
                if (i == 0)
                {
                    if (crv.IsClosed)
                    {
                        Curve c1 = null; c1 = sCurves[sCurves.Length - 1]; c1.Reverse();
                        Curve c2 = null; c2 = sCurves[i];
                        Vector3d v1 = new Vector3d(); v1 = c1.TangentAtStart;
                        Vector3d v2 = new Vector3d(); v2 = c2.TangentAtStart;
                        iRad = Vector3d.VectorAngle(v1, v2); iRad = Math.PI - iRad;
                        if (iRad > oRad)
                        {
                            Point3d p1 = new Point3d();
                            p1 = c1.PointAtStart;
                            pts.Add(p1);
                        }

                    }

                }
                if (i > 0)
                {
                    Curve c1 = null; c1 = sCurves[i - 1]; c1.Reverse();
                    Curve c2 = null; c2 = sCurves[i];
                    Vector3d v1 = new Vector3d(); v1 = c1.TangentAtStart;
                    Vector3d v2 = new Vector3d(); v2 = c2.TangentAtStart;
                    iRad = Vector3d.VectorAngle(v1, v2); iRad = Math.PI - iRad;
                    if (iRad > oRad)
                    {
                        Point3d p1 = new Point3d();
                        p1 = c1.PointAtStart;
                        pts.Add(p1);
                    }

                }
            }
            for (int i = 0; i < pts.Count; i++)
            {
                double st = new double();
                crv.ClosestPoint(pts[i], out st);


                t.Add(st);

            }
            exploded = crv.Split(t);
            for (int i = 0; i < exploded.Length; i++)
            {
                cvs.Add(exploded[i]);
            }
            // for(int i = 0; i < pts.Count; i++)
            if (cvs.Count == 0)
            {
                cvs.Add(crv);
            }
            DA.SetDataList("polyCurves", cvs);
        }


        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.CurveShater;
            }
        }


        public override Guid ComponentGuid => new Guid("E68B5FB7-0434-4E8A-A9A2-4F9F0691AC32");


    }
}