using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicalEditor.Model.Shapes
{
    public class EllipseShape : ShapeBase
    {
        public Point Center { get; set; }
        public double RadiusX { get; set; }
        public double RadiusY { get; set; }
        public override void Draw(DrawingContext dc)
        {
            dc.DrawEllipse(new SolidColorBrush(FillColor), new Pen(new SolidColorBrush(StrokeColor), StrokeThickness), Center, RadiusX, RadiusY);
        }
        public override void Update(Point point)
        {
            RadiusX = Math.Abs(point.X - Center.X);
            RadiusY = Math.Abs(point.Y - Center.Y);
        }
        public override void Initialize(Point point)
        {
            Center = point;
            RadiusX = 0;
            RadiusY = 0;
        }
    }
}
