using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicalEditor.Model.Shapes
{
    public class PolylineShape : ShapeBase
    {
        public override bool IsMultiClick => true;
        public List<Point> Points { get; set; } = new List<Point>();
        public override void Draw(DrawingContext dc)
        {
            if (Points.Count > 1)
            {
                for (int i = 0; i < Points.Count - 1; i++)
                    dc.DrawLine(new Pen(new SolidColorBrush(StrokeColor), StrokeThickness), Points[i], Points[i + 1]);
            }
        }
        public override void Update(Point point)
        {
            if (Points.Count > 0)
                Points[Points.Count - 1] = point;
        }
        public override void FinalizeDrawing(Point point)
        {
            Points.Add(point);
        }
        public override void Initialize(Point point)
        {
            Points.Clear();
            Points.Add(point);
        }
    }
}