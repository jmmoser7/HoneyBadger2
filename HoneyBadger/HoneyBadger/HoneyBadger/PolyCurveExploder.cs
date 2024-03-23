using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HoneyBadger
{
    public class PolyCurveAngleFinder : GH_Component
    {

        public PolyCurveAngleFinder()
          : base(
                "PolyCurveAngleFinder", 
                "Nickname",
                "Explodes A Polycurve For You",
                "HoneyBadger", 
                "Curves"
                )
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("polycurve", "Pc", "", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("internalSupliment", "I", "", GH_ParamAccess.list);
            pManager.AddNumberParameter("externalSupliment", "E", "", GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Curve> curves = new List<Curve>();
            Curve[] cList = new Curve[0];
            Curve crv = null;
            List<double> angle = new List<double>();
            DA.GetData("polycurve", ref crv);
            double count = new double();
            List<Rhino.Display.Text3d> angles = new List<Rhino.Display.Text3d>();
            List<double> angles2 = new List<double>();


            cList = crv.DuplicateSegments();

            for (int i = 0; i < cList.Length; i++)
            {
                curves.Add(cList[i]);
            }
            for (int i = 0; i < cList.Length; i++)
            {
                if (i == 0)
                {
                    if (crv.IsClosed)
                    {

                        double num = new double();
                        Curve cv1 = null; cv1 = curves[curves.Count - 1]; cv1.Reverse();
                        Curve cv2 = null; cv2 = curves[0];

                        Vector3d v1 = new Vector3d(cv1.TangentAtStart); v1.Unitize();
                        Vector3d v2 = new Vector3d(cv2.TangentAtStart); v2.Unitize();

                        num = Vector3d.VectorAngle(v1, v2); angle.Add(num);

                    }
                    //you need an  expression for if the curve is open
                }
                if (i > 0)
                {
                    double num = new double();
                    Curve cv1 = null; cv1 = curves[i - 1]; cv1.Reverse();
                    Curve cv2 = null; cv2 = curves[i];

                    Vector3d v1 = new Vector3d(cv1.TangentAtStart); v1.Unitize();
                    Vector3d v2 = new Vector3d(cv2.TangentAtStart); v2.Unitize();

                    num = Vector3d.VectorAngle(v1, v2); angle.Add(num);
                }
            }
            for (int i = 0; i < angle.Count; i++)
            {
                double angle2 = new double();
                angle2 = angle[i];
                angle2 = Math.PI - angle2;
                angles2.Add(angle2);

            }


 
            DA.SetDataList("internalSupliment", angle);
            DA.SetDataList("externalSupliment", angles2);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {

                return Properties.Resources.AngleFinder;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("2504F5A7-0578-4BCD-912C-42548933A05E"); }
        }
    }
}