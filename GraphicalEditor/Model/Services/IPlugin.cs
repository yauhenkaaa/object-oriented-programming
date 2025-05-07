using GraphicalEditor.Model.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Model.Services
{
    public interface IPlugin
    {
        string ShapeType { get; }
        string DisplayName { get; }
        string IconPath { get; }
        void Register(ShapeFactory factory);
    }
}