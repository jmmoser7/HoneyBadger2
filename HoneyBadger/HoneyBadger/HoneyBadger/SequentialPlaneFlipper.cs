using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HoneyBadger
{
    public class SequentialPlaneFlipper : GH_Component
    {

        public SequentialPlaneFlipper()
          : base
            (
                "SequentialPlaneFlipper", 
                "Nickname",
                "Automatickly aligns a list of plains by first rotating them to match the orientation of their neighbor and than aligning their x axies",
                "HoneyBadger", 
                "Planes"
            )
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPlaneParameter("planes", "Pi", "", GH_ParamAccess.list);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPlaneParameter("alignedPlanes", "Ap", "", GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Plane> inPlanes = new List<Plane>();
            DA.GetDataList("planes", inPlanes);

            for (int i = 0; i < inPlanes.Count; i++)
            {
                Plane myPlane1 = new Plane();
                Plane myPlane2 = new Plane();
                Plane myPlane3 = new Plane();

                Vector3d vector1 = new Vector3d();
                Vector3d vector2 = new Vector3d();

                if (i == 0)
                {
                    myPlane3 = inPlanes[i];
                }
                else
                {

                    myPlane1 = inPlanes[i - 1];
                    myPlane2 = inPlanes[i];

                    Transform projection = new Transform(Transform.PlanarProjection(myPlane2));

                    vector1 = myPlane1.XAxis;
                    vector1.Transform(projection);
                    vector1.Unitize();
                    //vector1.Reverse();

                    vector2 = vector1;
                    vector2.Rotate(Math.PI / 2, myPlane2.ZAxis);
                    //vector2.Reverse();

                    Plane cPlane = new Plane(myPlane2.Origin, vector1, vector2);
                    myPlane3 = cPlane;
                }

                inPlanes.Insert(i, myPlane3);
                inPlanes.RemoveAt(i + 1);
            }

            for (int i = 0; i < inPlanes.Count; i++)
            {
                Plane mp2 = new Plane();
                Plane mp3 = new Plane();


                if (i == 0)
                {
                    mp3 = inPlanes[i];
                }
                else
                {

                    mp2 = inPlanes[i - 1];
                    mp3 = inPlanes[i];

                    double addition = (mp2.ZAxis + mp3.ZAxis).Length;
                    Boolean dispatch1 = new Boolean();
                    dispatch1 = addition < Math.Sqrt(2);

                    if (dispatch1)
                    {
                        mp3.Rotate(Math.PI, mp3.XAxis);

                    }
                    else
                    {

                    }
                }
                inPlanes.Insert(i, mp3);
                inPlanes.RemoveAt(i + 1);
            }

            DA.SetDataList("alignedPlanes", inPlanes);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {

                return Properties.Resources.PlaneRotation;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("DBC1D81B-1AF5-42AA-ACB6-4FB8B72088C0"); }
        }
    }
}