using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HoneyBadger
{
    public class CurveSmooth : GH_Component
    {

        public CurveSmooth()
          : base(
                "CurveSmooth",
                "cs",
                "smoothe a curve",
                "HoneyBadger",
                "Curves"
             )
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("curve", "c", "", GH_ParamAccess.item);
            pManager.AddNumberParameter("factor", "f", "", GH_ParamAccess.item, 0.5);
            pManager.AddIntegerParameter("iterations", "i", "", GH_ParamAccess.item , 1);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddCurveParameter("curve", "c", "", GH_ParamAccess.item);
            pManager.AddTextParameter ("type", "t", "", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            SmoothingCoordinateSystem scs = new SmoothingCoordinateSystem();

            Curve crv = null;
            Curve crv2 = null;
            double factor = new double();   
            int iterations = new int();
            

            DA.GetData("curve", ref crv);
            DA.GetData("factor", ref factor);   
            DA.GetData("iterations", ref iterations);
            string type = crv.GetType().ToString();

            for (int i = 0; i < iterations; i++)
            {
                crv = crv.Smooth(factor, true, true, true, true, scs);
            }
            
                      
            
            DA.SetData("curve", crv);
            DA.SetData("type", type);   
            
        }

        protected override System.Drawing.Bitmap Icon{get{return Properties.Resources.curveSmoothing;}
        }

        public override Guid ComponentGuid{ get { return new Guid("372F76DD-640B-46F3-8600-2E42EC7A0D22"); }}
    }
}