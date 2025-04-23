using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicalEditor.Model.Shapes
{
    public class PolygonShape : ShapeBase
    {
        public override bool IsMultiClick => true;
        public List<Point> Points { get; set; } = new List<Point>();
        public override void Draw(DrawingContext dc)
        {
            if (Points.Count >= 2)
            {
                var geometry = new StreamGeometry();
                using (var ctx = geometry.Open())
                {
                    ctx.BeginFigure(Points[0], true, true); 
                    ctx.PolyLineTo(Points.GetRange(1, Points.Count - 1), true, false);
                }
                geometry.Freeze();
                dc.DrawGeometry(
                    new SolidColorBrush(FillColor),
                    new Pen(new SolidColorBrush(StrokeColor), StrokeThickness),
                    geometry
                );
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