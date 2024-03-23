using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HoneyBadger
{
    public class PolyCurveKinkBisector : GH_Component
    {

        public PolyCurveKinkBisector()
          : base(
                "PolyCurveKinkBisector", 
                "Nickname",
                "Description",
                "HoneyBadger", 
                "Planes"
                )
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGeometryParameter("polycurve", "Pc", "", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddCurveParameter("myCurve", "cv", "", GH_ParamAccess.list);
            pManager.AddPlaneParameter("bisectors", "pl", "", GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Plane> bisectors = new List<Plane>();
            List<Curve> curves = new List<Curve>();
            Curve crv = null;


            Curve[] exploded = new Curve[0];
            // bool mybool = new bool();

            DA.GetData("polycurve", ref crv);
            //PolyCurve pCrv = crv as PolyCurve;
            bool closed = new bool();
            closed = crv.IsClosed;


            //pCrv.RemoveNesting();
            //exploded = pCrv.Explode();
            exploded = crv.DuplicateSegments();


            for (int i = 0; i < exploded.Length; i++)
            {
                curves.Add(exploded[i]);
            }

            for (int i = 0; i < curves.Count; i++)
            {

                if (i > 0)
                {
                    Curve c1 = curves[i - 1];
                    Curve c2 = curves[i];
                    c1.Reverse();

                    Plane p1 = new Plane(c1.PointAtEnd, c1.TangentAtStart, c2.TangentAtStart);

                    Vector3d v1 = c1.TangentAtStart + c2.TangentAtStart;
                    v1.Unitize();
                    Plane p2 = new Plane(c1.PointAtStart, v1, p1.ZAxis);

                    bisectors.Add(p2);
                }
            }


            if (closed)
            {
                Curve c1 = curves[curves.Count - 1];
                Curve c2 = curves[0];
                c2.Reverse();
                Plane p1 = new Plane(c1.PointAtEnd, c1.TangentAtStart, c2.TangentAtStart);

                Vector3d v1 = c2.TangentAtStart + c1.TangentAtStart;
                v1.Unitize();
                v1.Rotate(Math.PI / 2, p1.ZAxis);
                Plane p2 = new Plane(c1.PointAtEnd, v1, p1.ZAxis);

                //bisectors.Add(p1);
                bisectors.Add(p2);
            }
            else
            {
                Plane p1 = new Plane();
                Plane p2 = new Plane();
                double param1 = new double();
                double param2 = new double();
                crv.ClosestPoint(crv.PointAtStart, out param1);
                crv.PerpendicularFrameAt(param1, out p1);
                crv.ClosestPoint(crv.PointAtEnd, out param2);
                crv.PerpendicularFrameAt(param2, out p2);
                bisectors.Insert(0, p1);
                bisectors.Add(p2);

            }

            DA.SetDataList("myCurve", curves);
            DA.SetDataList("bisectors", bisectors);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {

                return Properties.Resources.PolycurvePlane;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("0E9D6EC8-2B35-42BB-AAD1-69705EB67105"); }
        }
    }
}