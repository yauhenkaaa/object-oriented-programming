using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GraphicalEditor.Model.Services
{
    public class DrawingSettings
    {
        public Color StrokeColor { get; set; } = Colors.Black;
        public Color FillColor { get; set; } = Colors.Transparent;
        public double StrokeThickness { get; set; } = 1.0;
 
    }
}