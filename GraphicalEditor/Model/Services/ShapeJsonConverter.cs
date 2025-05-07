using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Media;
using GraphicalEditor.Model.Shapes;

namespace GraphicalEditor.Model.Services
{
    public class ShapeJsonConverter : JsonConverter<ShapeBase>
    {
        public override ShapeBase Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;

            if (!root.TryGetProperty("Type", out var typeProp))
                throw new JsonException("Missing Type discriminator");

            var key = typeProp.GetString()!;
            var registry = ShapeFactory.Instance.RegisteredTypes();
            if (!registry.TryGetValue(key, out var clrType))
                throw new JsonException($"Unknown shape type '{key}'");
            var shape = (ShapeBase)Activator.CreateInstance(clrType)!;
            foreach (var propJson in root.EnumerateObject())
            {
                var name = propJson.Name;
                if (name == "Type") continue;

                var pi = clrType.GetProperty(name,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (pi == null || !pi.CanWrite) continue;

                object? val;
                if (pi.PropertyType == typeof(Color))
                {
                    var s = propJson.Value.GetString()!;
                    val = (Color)ColorConverter.ConvertFromString(s);
                }
                else if (pi.PropertyType == typeof(double))
                {
                    val = propJson.Value.GetDouble();
                }
                else if (pi.PropertyType == typeof(bool))
                {
                    val = propJson.Value.GetBoolean();
                }
                else if (pi.PropertyType == typeof(Point))
                {
                    var x = propJson.Value.GetProperty("X").GetDouble();
                    var y = propJson.Value.GetProperty("Y").GetDouble();
                    val = new Point(x, y);
                }
                else
                {
                    var raw = propJson.Value.GetRawText();
                    val = JsonSerializer.Deserialize(raw, pi.PropertyType, options);
                }

                pi.SetValue(shape, val);
            }

            return shape;
        }

        public override void Write(Utf8JsonWriter writer, ShapeBase value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            var tn = value.GetType().Name;
            if (tn.EndsWith("Shape")) tn = tn[..^"Shape".Length];
            writer.WriteString("Type", tn);

            foreach (var pi in value.GetType()
                                    .GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!pi.CanRead) continue;
                var name = pi.Name;
                var val = pi.GetValue(value);
                if (val == null) continue;

                writer.WritePropertyName(name);

                if (val is Color c)
                {
                    writer.WriteStringValue(c.ToString());
                }
                else if (val is Point p)
                {
                    writer.WriteStartObject();
                    writer.WriteNumber("X", p.X);
                    writer.WriteNumber("Y", p.Y);
                    writer.WriteEndObject();
                }
                else
                {
                    JsonSerializer.Serialize(writer, val, pi.PropertyType, options);
                }
            }

            writer.WriteEndObject();
        }
    }
}
