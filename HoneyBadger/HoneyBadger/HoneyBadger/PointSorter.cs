using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HoneyBadger
{
    public class PointSorter : GH_Component
    {

        public PointSorter()
          : base(
                "PointSorter", 
                "Nickname",
                "Description",
                "HoneyBadger", 
                "Points"
                )
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("points", "P", "", GH_ParamAccess.list);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("sortedPoints", "Sp", "", GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Point3d> iPoints = new List<Point3d>();
            ///List<Point3d> sPoints = new List<Point3d>();
            int n = iPoints.Count;
            Point3d[] points = new Point3d[0];

            DA.GetDataList("points", iPoints);
            points = Point3d.SortAndCullPointList(iPoints, 0.01);
            DA.SetDataList("sortedPoints", points);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {

                return Properties.Resources.PointSorter;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("42EE86A8-7B03-48F6-AD29-123D1B240C62"); }
        }
    }
}