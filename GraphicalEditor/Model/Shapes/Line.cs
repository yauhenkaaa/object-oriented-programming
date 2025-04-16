using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

namespace GraphicalEditor.Model.Shapes
{
    public class LineShape : ShapeBase
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public override void Draw(DrawingContext dc)
        {
            dc.DrawLine(new Pen(new SolidColorBrush(StrokeColor), StrokeThickness), Start, End);
        }
        public override void Update(Point point)
        {
            End = point;
        }
        public override void Initialize(Point point)
        {
            Start = point;
            End = point;
        }
    }
}
