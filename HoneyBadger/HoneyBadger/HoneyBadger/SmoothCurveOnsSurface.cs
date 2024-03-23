using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HoneyBadger
{
    public class SurfaceSmooth : GH_Component
    {

        public SurfaceSmooth()
          : base(
                "SurfaceSmooth", 
                "Smooth",
                "Smooths out areas of higere curviture on a NURBS surface",
                "HoneyBadger", 
                "Surface"
                )
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddSurfaceParameter("surface", "srf", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("strength", "st", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("iterations", "it", "", GH_ParamAccess.item);
            pManager.AddBooleanParameter("fixed", "f", "", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddSurfaceParameter("smoothSurface", "s", "", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Surface s1 = null; DA.GetData("surface", ref s1); s1.ToNurbsSurface();


            double st = new double(); DA.GetData("strength", ref st);
            double it = new double(); DA.GetData("iterations", ref it);
            SmoothingCoordinateSystem pl = new SmoothingCoordinateSystem();
            Surface s2 = null;
            bool t = new bool(); DA.GetData("fixed", ref t);



            for (int i = 0; i < it; i++)
            {
                s2 = s1.Smooth(st, false, false, true, t, pl);
                s1 = s2;
            }
            DA.SetData("smoothSurface", s1);
        }
        //this is done

        protected override System.Drawing.Bitmap Icon
        {
            get
            {

                return null;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("CC749463-5EE5-4114-AB1F-2EA442CB5A59"); }
        }
    }
}