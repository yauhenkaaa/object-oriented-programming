using System.Collections.Generic;
using GraphicalEditor.Model.Commands;
using GraphicalEditor.Model.Shapes;

namespace GraphicalEditor.Controllers
{
    public class CanvasController
    {
        private List<ShapeBase> _shapesList;
        public CanvasController(List<ShapeBase> shapesList)
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
