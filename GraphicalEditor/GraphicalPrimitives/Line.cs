using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicalEditor.GraphicalPrimitives
{
    public class LineShape : ShapeBase
    {
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }

        public LineShape()
        {
            X1 = rnd.Next(0, CanvasWidth);
            Y1 = rnd.Next(0, CanvasHeight);
            X2 = rnd.Next(0, CanvasWidth);
            Y2 = rnd.Next(0, CanvasHeight);

            Stroke = new SolidColorBrush(Color.FromRgb(
                (byte)rnd.Next(256),
                (byte)rnd.Next(256),
                (byte)rnd.Next(256)));
            StrokeThickness = 2;
        }

        public override Shape CreateShape()
        {
            return new Line
            {
                X1 = X1,
                Y1 = Y1,
                X2 = X2,
                Y2 = Y2,
                Stroke = Stroke,
                StrokeThickness = StrokeThickness
            };
        }
    }
}
