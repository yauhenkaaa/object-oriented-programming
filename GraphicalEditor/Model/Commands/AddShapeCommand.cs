using System.Collections.Generic;
using GraphicalEditor.Model.Shapes;

namespace GraphicalEditor.Model.Commands
{
    public class AddShapeCommand : ICommand
    {
        private readonly ShapeBase _shape;
        private readonly List<ShapeBase> _shapes;
        public AddShapeCommand(ShapeBase shape, List<ShapeBase> shapes)
        {
            _shape = shape;
            _shapes = shapes;
        }
        public void Execute() => _shapes.Add(_shape);
        public void Undo() => _shapes.Remove(_shape);
    }
}