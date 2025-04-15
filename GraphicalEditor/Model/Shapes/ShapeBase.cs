using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicalEditor.GraphicalPrimitives
{
    public abstract class ShapeBase
    {
        protected const int CanvasWidth = 700;
        protected const int CanvasHeight = 737;
        protected static Random rnd = new Random();
        public Brush Stroke { get; set; }
        public double StrokeThickness { get; set; }

        public abstract Shape CreateShape();
    }
}
