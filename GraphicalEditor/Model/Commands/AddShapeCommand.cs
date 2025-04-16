using System.Collections.Generic;
using GraphicalEditor.Model.Shapes;

namespace GraphicalEditor.Model.Commands
{
    public interface ICommand
    {
        void Execute();
        void Unexecute();
    }

    public class AddShapeCommand : ICommand
    {
        private readonly ShapeBase _shape;
        private readonly IList<ShapeBase> _shapesList;
        public AddShapeCommand(ShapeBase shape, IList<ShapeBase> shapesList)
        {
            _shape = shape;
            _shapesList = shapesList;
        }
        public void Execute() => _shapesList.Add(_shape);
        public void Unexecute() => _shapesList.Remove(_shape);
    }
}
