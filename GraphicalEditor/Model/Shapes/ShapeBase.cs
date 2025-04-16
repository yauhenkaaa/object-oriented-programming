using System.Windows;
using System.Windows.Media;

namespace GraphicalEditor.Model.Shapes
{
    public abstract class ShapeBase
    {
        public Color StrokeColor { get; set; }
        public Color FillColor { get; set; }
        public double StrokeThickness { get; set; }
        public abstract void Draw(DrawingContext dc);
        public abstract void Update(Point point);
        public virtual void FinalizeDrawing(Point point) => Update(point);
        public virtual void Initialize(Point point) { }
    }
}
