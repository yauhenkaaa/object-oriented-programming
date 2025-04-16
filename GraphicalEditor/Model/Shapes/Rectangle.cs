using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicalEditor.Model.Shapes
{
    public class RectangleShape : ShapeBase
    {
        public Point TopLeft { get; set; }
        public Point BottomRight { get; set; }
        public override void Draw(DrawingContext dc)
        {
            Rect rect = new Rect(TopLeft, BottomRight);
            dc.DrawRectangle(new SolidColorBrush(FillColor), new Pen(new SolidColorBrush(StrokeColor), StrokeThickness), rect);
        }
        public override void Update(Point point)
        {
            BottomRight = point;
        }
        public override void Initialize(Point point)
        {
            TopLeft = point;
            BottomRight = point;
        }
    
    }
}