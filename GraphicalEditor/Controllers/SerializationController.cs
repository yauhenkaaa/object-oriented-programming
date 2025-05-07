using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using GraphicalEditor.Model.Services;
using GraphicalEditor.Model.Shapes;

namespace GraphicalEditor.Controllers
{
    public class SerializationController
    {
        private readonly Serializer _serializer = new();
        private string _currentPath;

        public bool HasCurrentFile => !string.IsNullOrEmpty(_currentPath);
        public string CurrentFileName
            => HasCurrentFile ? Path.GetFileName(_currentPath) : null;

        public void New(List<ShapeBase> shapes)
        {
            shapes.Clear();
            _currentPath = null;
        }

        public List<ShapeBase> Open(string path)
        {
            _currentPath = path;
            try
            {
                return _serializer.Load(path);
            }
            catch (JsonException ex)
            {
                MessageBox.Show(
                    $"Не удалось десериализовать файл:\n{ex.Message}\n\n" +
                    "Скорее всего, в рисунке есть фигурa из плагина, который не загружен.\n" +
                    "Пожалуйста, сначала добавьте соответствующий плагин.",
                    "Ошибка загрузки рисунка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return new List<ShapeBase>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка при загрузке файла:\n{ex.Message}",
                    "Ошибка загрузки",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return new List<ShapeBase>();
            }
        }

        public void SaveAs(string path, List<ShapeBase> shapes)
        {
            _serializer.Save(path, shapes);
            _currentPath = path;
        }

        public void Save(List<ShapeBase> shapes)
        {
            if (!HasCurrentFile)
                throw new InvalidOperationException("Нет текущего файла для сохранения");
            _serializer.Save(_currentPath, shapes);
        }
    }
}
