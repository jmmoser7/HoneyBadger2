using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;

namespace HoneyBadger
{
    public class GetY : GH_Component
    {

        public GetY()
          : base(
                "GetY",
                "Nickname",
                "Description",
                "HoneyBadger",
                "Planes"
                )
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("geo", "d", "", GH_ParamAccess.item);
            pManager[0].Optional = true;
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("out", "o", "", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            String planes = "Grasshopper.Kernel.Types.GH_Plane";
            String Points = "Grasshopper.Kernel.Types.GH_Point";
            String Vectors = "Grasshopper.Kernel.Types.GH_Vector";
            String Box = "Grasshopper.Kernel.Types.GH_Box";

            Object objIn = null;
            DA.GetData("geo", ref objIn);

            //DA.SetData("wtf", (planes == (objIn.GetType().ToString())));
            if (Params.Input[0].SourceCount == 0)
            {
                Vector3d vec = new Vector3d(0, 1, 0);
                DA.SetData("out", vec);
            }

            else if (Params.Input[0].Sources.Count != 0)
            {

                if (objIn == null)
                {
                    DA.SetData("out", null);
                }
                else if ((planes == (objIn.GetType().ToString())))
                {
                    Plane pl = new Plane();
                    Vector3d v = new Vector3d();
                    DA.GetData("geo", ref pl);
                    v = pl.YAxis; v.Unitize();

                    DA.SetData("out", v);
                }

                else if ((Points == (objIn.GetType().ToString())))
                {
                    Point3d pt = new Point3d();
                    Double d = new Double();
                    DA.GetData("geo", ref pt);

                    d = pt.Y;
                    DA.SetData("out", d);
                }

                else if ((Vectors == (objIn.GetType().ToString())))
                {
                    Vector3d vec = new Vector3d();
                    double d = new Double();
                    DA.GetData("geo", ref vec);
                    d = vec.Y;
                    DA.SetData("out", d);
                }


                else if (objIn is GH_Interval)
                {
                    Interval Domain = new Interval();
                    DA.GetData("geo", ref Domain);
                    double length = Domain.Length;
                    DA.SetData("out", length);
                }

                else if (objIn is GH_Box)
                {

                    Interval Domain = new Interval();
                    Box box = new Rhino.Geometry.Box();
                    DA.GetData("geo", ref box);
                    Domain = box.Y;
                    DA.SetData("out", Domain);
                }

                else if (objIn is IGH_GeometricGoo)
                {
                    IGH_GeometricGoo geo = objIn as IGH_GeometricGoo;
                    BoundingBox bb = geo.Boundingbox;

                    double a = bb.Min.Y;
                    double b = bb.Max.Y;
                    Interval Domain = new Interval(a, b);
                    DA.SetData("out", Domain);
                }
                else if (objIn is GH_Number)
                {
                    double d = new double();
                    DA.GetData("geo", ref d);
                    Vector3d v = new Vector3d(0, d, 0);
                    DA.SetData("out", v);
                }
                else if (objIn is GH_Integer)
                {
                    int d = new int();
                    DA.GetData("geo", ref d);
                    Vector3d v = new Vector3d(0, d, 0);
                    DA.SetData("out", v);
                }
                else if (objIn is GH_Boolean)
                { 
                bool d = new bool();
                DA.GetData("geo", ref d);
                if (d)
                {
                    Vector3d v = new Vector3d(0, 1, 0);
                    DA.SetData("out", v);
                }
                else
                {
                    Vector3d v = new Vector3d(0, 0, 0);
                    DA.SetData("out", v);
                }
                }
            else if (objIn is GH_Colour)
            {
                double d = new double();
                GH_Colour c = new GH_Colour();
                DA.GetData("geo", ref c);
                d = c.Value.G;
                DA.SetData("out", (d));
            }
        }
    
        

        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {

                return Properties.Resources.PlaneY;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("3D9C8716-7EB2-4488-B5AD-7AD55961435F"); }
        }
    }
}