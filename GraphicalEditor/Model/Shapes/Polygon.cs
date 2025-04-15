using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicalEditor.GraphicalPrimitives
{
    public class PolygonShape : ShapeBase
    {
        public PointCollection Points { get; }
        public int Sides { get; }
        public double Radius { get; }
        public double CenterX { get; }
        public double CenterY { get; }

        public PolygonShape()
        {
            Sides = rnd.Next(3, 8);
            Radius = rnd.Next(20, 200);
            CenterX = rnd.Next((int)Radius, CanvasWidth - (int)Radius);
            CenterY = rnd.Next((int)Radius, CanvasHeight - (int)Radius);

            Points = GenerateRegularPolygonPoints();

            Stroke = new SolidColorBrush(Color.FromRgb(
                (byte)rnd.Next(256),
                (byte)rnd.Next(256),
                (byte)rnd.Next(256)));
            StrokeThickness = 2;
        }

        private PointCollection GenerateRegularPolygonPoints()
        {
            var points = new PointCollection();
            double angleStep = 360.0 / Sides;
            double currentAngle = 0;

            for (int i = 0; i < Sides; i++)
            {
                double radians = currentAngle * (Math.PI / 180);
                double x = CenterX + Radius * Math.Cos(radians);
                double y = CenterY + Radius * Math.Sin(radians);

                points.Add(new Point(x, y));
                currentAngle += angleStep;
            }

            return points;
        }

        public override Shape CreateShape()
        {
            return new Polygon
            {
                Points = Points,
                Stroke = Stroke,
                StrokeThickness = StrokeThickness,
                RenderTransform = new TranslateTransform(0, 0)
            };
        }
    }
}