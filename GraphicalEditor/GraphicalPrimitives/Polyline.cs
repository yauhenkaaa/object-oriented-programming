using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicalEditor.GraphicalPrimitives
{
    public class PolylineShape : ShapeBase
    {
        public PointCollection Points { get; }

        public PolylineShape()
        {
            Points = new PointCollection();
            int numPoints = rnd.Next(3, 7);

            for (int i = 0; i < numPoints; i++)
            {
                Points.Add(new Point(
                    rnd.Next(0, CanvasWidth),
                    rnd.Next(0, CanvasHeight)));
            }

            Stroke = new SolidColorBrush(Color.FromRgb(
                (byte)rnd.Next(256),
                (byte)rnd.Next(256),
                (byte)rnd.Next(256)));
            StrokeThickness = 2;
        }

        public override Shape CreateShape()
        {
            return new Polyline
            {
                Points = Points,
                Stroke = Stroke,
                StrokeThickness = StrokeThickness,
                RenderTransform = new TranslateTransform(0, 0)
            };
        }
    }
}