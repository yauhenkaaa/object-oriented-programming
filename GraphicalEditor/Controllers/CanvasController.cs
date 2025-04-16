using System.Collections.Generic;
using GraphicalEditor.Model.Commands;
using GraphicalEditor.Model.Shapes;

namespace GraphicalEditor.Controller
{
    public class CanvasController
    {
        private readonly IList<ShapeBase> _shapesList;
        public CanvasController(IList<ShapeBase> shapesList)
        {
            _shapesList = shapesList;
        }
        public void AddShape(ShapeBase shape)
        {
            ICommand cmd = new AddShapeCommand(shape, _shapesList);
            cmd.Execute();
        }
    }
}
