using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HoneyBadger
{
    public class RayBouncer : GH_Component
    {

        public RayBouncer()
          : base("RayBouncer", "rb",
              "Description",
              "HoneyBadger",
              "Vectors")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("origin", "O", "", GH_ParamAccess.item);
            pManager.AddVectorParameter("direction", "D", "", GH_ParamAccess.item);
            pManager.AddGeometryParameter("BouncingGeometry", "G", "", GH_ParamAccess.list);
            pManager.AddIntegerParameter("maxBounces", "M", "", GH_ParamAccess.item, 5);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddCurveParameter("reflections", "r", "", GH_ParamAccess.item);
        }


        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // calculate the ray bounces off of all the geometry in the list

            // the ray is defined by the origin and direction

            //the geometry is defined by the list of geometry

            // the output is a curve that is the path of the ray
            Point3d myOrigin = new Point3d();
            Vector3d myDirection = new Vector3d();

            int maxBounces = new int();

            DA.GetData("origin", ref myOrigin);

            DA.GetData("direction", ref myDirection);
            DA.GetData("maxBounces", ref maxBounces);

            //construct ray from origin and direction
            Ray3d myRay = new Ray3d(myOrigin, myDirection);

            //get the geometry
            List<GeometryBase> myGeometry = new List<GeometryBase>();
            DA.GetDataList("BouncingGeometry", myGeometry);

            //bounce the ray off of the geometry and return a list of intersection points
            List<Point3d> intersectionPoints = new List<Point3d>();

            Point3d[] myintersections = Rhino.Geometry.Intersect.Intersection.RayShoot(myRay, myGeometry, maxBounces);
            myintersections.ToArray().Append(myOrigin);
            
            PolylineCurve myCurve = new PolylineCurve(myintersections);
            DA.SetData("reflections", myCurve);

         

            
        }
        protected override System.Drawing.Bitmap Icon { get { return null; } }

        public override Guid ComponentGuid { get { return new Guid("C849140F-DAA6-426A-9DA2-236CDC1B2962"); } }
    }
}