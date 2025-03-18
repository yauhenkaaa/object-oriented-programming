using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicalEditor.GraphicalPrimitives
{
    public class RectangleShape : ShapeBase
    {
        public double X { get; }
        public double Y { get; }
        public double Width { get; }
        public double Height { get; }

        public RectangleShape()
        {
            Width = rnd.Next(20, 300);
            Height = rnd.Next(20, 300);
            X = rnd.Next(0, CanvasWidth - (int)Width);
            Y = rnd.Next(0, CanvasHeight - (int)Height);

            Stroke = new SolidColorBrush(Color.FromRgb(
                (byte)rnd.Next(256),
                (byte)rnd.Next(256),
                (byte)rnd.Next(256)));
            StrokeThickness = 2;
        }

        public override Shape CreateShape()
        {
            return new Rectangle
            {
                Width = Width,
                Height = Height,
                Stroke = Stroke,
                StrokeThickness = StrokeThickness,
                RenderTransform = new TranslateTransform(X, Y)
            };
        }
    }
}