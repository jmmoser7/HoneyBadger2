using System;
using System.Drawing;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;

public class SpecialIntegerObject : GH_Param<GH_Integer>
{
    public SpecialIntegerObject() :
      base(new GH_InstanceDescription("Toggle", "Toggle", "True/False", "Params", "Input"))
    { }

    public override void CreateAttributes()
    {
        m_attributes = new SpecialIntegerAttributes(this);
    }


    public override GH_Exposure Exposure
    {
        get
        {
            return GH_Exposure.primary | GH_Exposure.obscure;
        }
    }
    public override System.Guid ComponentGuid
    {
        get { return new Guid("{4A617FB8-E57A-42fd-8C17-3FFCE014D56F}"); }
    }

    private int m_value = 0;
    public int Value
    {
        get { return m_value; }
        set { m_value = value; }
    }
    protected override void CollectVolatileData_Custom()
    {
        VolatileData.Clear();
        AddVolatileData(new Grasshopper.Kernel.Data.GH_Path(0), 0, new GH_Integer(Value));
    }

    public override bool Write(GH_IO.Serialization.GH_IWriter writer)
    {
        writer.SetInt32("SpecialInteger", m_value);
        return base.Write(writer);
    }
    public override bool Read(GH_IO.Serialization.GH_IReader reader)
    {
        m_value = 0;
        reader.TryGetInt32("SpecialInteger", ref m_value);
        return base.Read(reader);
    }
}

public class SpecialIntegerAttributes : GH_Attributes<SpecialIntegerObject>
{
    private int[][] m_square;
    public SpecialIntegerAttributes(SpecialIntegerObject owner)
      : base(owner)
    {
        m_square = new int[3][];
        m_square[0] = new int[3] { ' ', 0, 1 };

    }

    public override bool HasInputGrip { get { return false; } }
    public override bool HasOutputGrip { get { return true; } }

    private const int ButtonSize = 35;

    //Our object is always the same size, but it needs to be anchored to the pivot.
    protected override void Layout()
    {
        //Lock this object to the pixel grid. 
        //I.e., do not allow it to be position in between pixels.
        Pivot = GH_Convert.ToPoint(Pivot);
        Bounds = new RectangleF(Pivot, new SizeF(3 * ButtonSize, ButtonSize));
    }
    /// <summary>
    /// This method returns the button at the given column and row offsets.
    /// </summary>
    private Rectangle Button(int column, int row)
    {
        int x = Convert.ToInt32(Pivot.X);
        int y = Convert.ToInt32(Pivot.Y);
        return new Rectangle(x + column * ButtonSize, y + row * ButtonSize, ButtonSize, ButtonSize);
    }
    /// <summary>
    /// Gets the value for the given button.
    /// </summary>
    private int Value(int column, int row)
    {
        return m_square[row][column];
    }

    public override GH_ObjectResponse RespondToMouseDown(GH_Canvas sender, GH_CanvasMouseEvent e)
    {
        //On a double click we'll set the owner value.
        if (e.Button == System.Windows.Forms.MouseButtons.Left)
        {
            for (int col = 1; col < 3; col++)
            {
                for (int row = 0; row < 1; row++)
                {
                    RectangleF button = Button(col, row);
                    if (button.Contains(e.CanvasLocation))
                    {
                        int value = Value(col, row);
                        Owner.RecordUndoEvent("Square Change");
                        Owner.Value = value;
                        Owner.ExpireSolution(true);
                        return GH_ObjectResponse.Handled;
                    }
                }
            }
        }
        return base.RespondToMouseDown(sender, e);
        //return base.RespondToMouseDoubleClick(sender, e);
    }
    public override void SetupTooltip(PointF point, GH_TooltipDisplayEventArgs e)
    {
        base.SetupTooltip(point, e);
        e.Description = "Toggle";
    }

    /// <summary>
    /// This object is rendered as a 4x4 grid of capsules.
    /// </summary>
    protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
    {
        if (channel == GH_CanvasChannel.Objects)
        {
            //Render output grip.
            GH_CapsuleRenderEngine.RenderOutputGrip(graphics, canvas.Viewport.Zoom, OutputGrip, true);

            //Render capsules.
            for (int col = 1; col < 3; col++)
            {
                for (int row = 0; row < 1; row++)
                {
                    int value = Value(col, row);
                    Rectangle button = Button(col, row);

                    GH_Palette palette = GH_Palette.White;
                    if (value == Owner.Value)
                        palette = GH_Palette.Black;

                    GH_Capsule capsule = GH_Capsule.CreateTextCapsule(button, button, palette, value.ToString(), 0, 0);
                    capsule.Render(graphics, Selected, Owner.Locked, false);
                    capsule.Dispose();
                }
            }
        }
    }
}