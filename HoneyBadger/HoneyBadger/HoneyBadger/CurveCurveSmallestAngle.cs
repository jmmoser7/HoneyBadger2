using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace HoneyBadger
{
    public class CurveCurveSmallestAingle : GH_Component
    {

        public CurveCurveSmallestAingle()
          : base(
                "CurveSmallestAngle",
                 "Angle",
                 "A component that finds the smallest angle between two curves",
                 "HoneyBadger",
                 "Curves"
                )
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("line1", "L1", "", GH_ParamAccess.item);
            pManager.AddCurveParameter("line2", "L2", "", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("angle1", "a", "", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Curve L1 = null;
            Curve L2 = null;
            DA.GetData("line1", ref L1);
            DA.GetData("line2", ref L2);

            const double intersection_tolerance = 0.001;
            const double overlap_tolerance = 0.0;
            List<Point3d> pts = new List<Point3d>();
            Point3d A = new Point3d(L1.PointAtStart);
            Point3d B = new Point3d(L1.PointAtEnd);
            Point3d C = new Point3d(L2.PointAtStart);
            Point3d D = new Point3d(L2.PointAtEnd);



            var intersection = Rhino.Geometry.Intersect.Intersection.CurveCurve(L1, L2, intersection_tolerance, overlap_tolerance);
            if (intersection != null)
            {
                {
                    ///List < Point3d > pts = new List<Point3d>();
                    /// Point3d[] pointss = new Point3d[0];
                    Point3d pt = new Point3d();
                    /// double param = new double();
                    double firstAngle = new double();
                    double secondAngle = new double();
                    double thirdAngle = new double();
                    double fourthAngle = new double();
                    List<double> biggySmalls = new List<double>();
                    for (int i = 0; i < intersection.Count; i++)
                    {
                        var firstIntersection = intersection[i];
                        pt = (firstIntersection.PointA);

                        if (pt == A || pt == B || pt == C || pt == D)
                        {
                            Vector3d v1 = new Vector3d();
                            Vector3d v2 = new Vector3d();
                            ///Vector3d v3 = new Vector3d();

                            double p1 = new double();
                            double p2 = new double();

                            L1.ClosestPoint(pt, out p1);
                            L2.ClosestPoint(pt, out p2);

                            v1 = L1.TangentAt(p1);
                            v2 = L2.TangentAt(p2);

                            Point3d S1 = L1.PointAtStart;
                            Point3d S2 = L1.PointAtEnd;
                            Point3d S3 = L2.PointAtStart;
                            Point3d S4 = L2.PointAtEnd;


                            if (S1 == S3)
                            {
                                firstAngle = Vector3d.VectorAngle(v1, v2);
                            }
                            else
                            {
                                if (S1 == S4)
                                {
                                    v2.Reverse();
                                    secondAngle = Vector3d.VectorAngle(v1, v2);
                                }
                                else
                                {
                                    if (S2 == S3)
                                    {
                                        v1.Reverse();
                                        thirdAngle = Vector3d.VectorAngle(v1, v2);
                                    }
                                    else
                                    {
                                        if (S2 == S4)
                                        {
                                            v1.Reverse();
                                            v2.Reverse();
                                            fourthAngle = Vector3d.VectorAngle(v1, v2);
                                        }
                                    }
                                }
                            }


                            biggySmalls.Add(firstAngle);
                            biggySmalls.Add(secondAngle);
                            biggySmalls.Add(thirdAngle);
                            biggySmalls.Add(fourthAngle);
                            biggySmalls.Sort();

                            DA.SetData("angle1", biggySmalls[3]);

                            ///DA.SetData("angle1", biggySmalls[0]);
                            /// DA.SetData("angle2", biggySmalls[1]);
                            /// evaluate the tangency of L1 at this point
                            /// evaluate the tangency of L2 at this point
                            /// evaluate the angle between the tangencie
                            /// reverse one vector and evaluate the new aingle between the tangencies
                            /// figure out how to output a tree
                           // pts.Add(pt);
                        }

                        else
                        {
                            DA.SetData("angle1", 0.0);
                        }

                    }
                    //DA.SetData("angle1", biggySmalls[3]);
                    ///DA.SetData("angle2", biggySmalls[1]);

                }
            }
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return Properties.Resources.TwoCurveAngleFinder;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("5A35DD76-BB25-4A02-9042-EB48F4938CF0"); }
        }
    }
}