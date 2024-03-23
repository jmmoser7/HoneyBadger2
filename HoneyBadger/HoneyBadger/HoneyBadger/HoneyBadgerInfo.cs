using System;
using System.Drawing;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;

namespace HoneyBadger
{
    public class HoneyBadgerInfo : GH_AssemblyInfo
    {
        public override string Name => "HoneyBadger";



        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon       
        {

            get { return Properties.Resources.AngleFinder; }    
        }
        public override Bitmap AssemblyIcon => Properties.Resources.PolycurvePlane;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "";

        public override Guid Id => new Guid("9F7F0104-50FB-4D53-92E5-10EB7FAAE3A2");

        //Return a string identifying you or your company.
        public override string AuthorName => "";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "";
    }
}