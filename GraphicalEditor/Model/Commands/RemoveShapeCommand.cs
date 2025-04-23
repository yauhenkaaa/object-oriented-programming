using System.Collections.Generic;
using GraphicalEditor.Model.Shapes;

namespace GraphicalEditor.Model.Commands
{
    public class RemoveShapeCommand : ICommand
    {
        private readonly ShapeBase _shape;
        private readonly List<ShapeBase> _shapes;

        public RemoveShapeCommand(ShapeBase shape, List<ShapeBase> shapes)
        {
            _shape = shape;
            _shapes = shapes;
        }

        public void Execute() => _shapes.Remove(_shape);
        public void Undo() => _shapes.Add(_shape);
    }
}
