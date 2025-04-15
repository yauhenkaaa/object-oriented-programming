using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicalEditor.GraphicalPrimitives
{
    public class EllipseShape : ShapeBase
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public EllipseShape()
        {
            Width = rnd.Next(10, 300);
            Height = rnd.Next(10, 300);
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
            return new Ellipse
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
