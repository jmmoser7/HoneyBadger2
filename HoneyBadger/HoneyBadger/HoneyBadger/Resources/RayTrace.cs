using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Rhino.Geometry.Intersect;

namespace HoneyBadger
{
    public class RayTrace : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the RayTrace class.
        /// </summary>
        public RayTrace()
  : base("RayTracePath", "Path", "Description", "HoneyBadger", "Vectors")
        {
        }
  
        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("RayBasePoint", "bp", "", GH_ParamAccess.item, new Point3d(0, 0, 0));
            pManager.AddVectorParameter("RayDirection", "d", "", GH_ParamAccess.item, new Vector3d(1, 0, 0));
            pManager.AddGeometryParameter("ContextGeometry", "g", "", GH_ParamAccess.list);
            pManager.AddIntegerParameter("ReflectionCount", "r", "", GH_ParamAccess.item, 4);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("paths", "p", "", GH_ParamAccess.list);
            pManager.AddGenericParameter("out","o","",GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Point3d basePoint = new Point3d();
            Vector3d direction = new Vector3d();
            List<GeometryBase> list = new List<GeometryBase>();
            int BouncyBouncyBouncyBouncyFunFunFunFunFun = new int();



            DA.GetData("RayBasePoint", ref basePoint);
            DA.GetData("RayDirection", ref direction);
            DA.GetDataList("ContextGeometry", list);
            DA.GetData("ReflectionCount", ref BouncyBouncyBouncyBouncyFunFunFunFunFun);
            Ray3d ray = new Ray3d(basePoint, direction);
            DA.SetData("out", ray.Direction);
            //Point3d[] paths = RayShoot(ray, list, BouncyBouncyBouncyBouncyFunFunFunFunFun);


        }


        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("F9AA1D1A-6A08-4A4D-8440-7A8D9FA232C7"); }
        }
    }
}