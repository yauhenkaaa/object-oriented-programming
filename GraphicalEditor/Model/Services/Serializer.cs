using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using GraphicalEditor.Model.Shapes;

namespace GraphicalEditor.Model.Services
{
    public class Serializer
    {
        private readonly JsonSerializerOptions _opts = new()
        {
            WriteIndented = true,
            Converters = { new ShapeJsonConverter() }
        };

        public void Save(string path, IEnumerable<ShapeBase> shapes)
        {
            var json = JsonSerializer.Serialize(shapes, _opts);
            File.WriteAllText(path, json);
        }

        public List<ShapeBase> Load(string path)
        {
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<ShapeBase>>(json, _opts)
                   ?? new List<ShapeBase>();
        }
    }
}
