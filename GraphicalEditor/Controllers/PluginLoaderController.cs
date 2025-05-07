using GraphicalEditor.Model.Services;
using GraphicalEditor.Model.Shapes;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GraphicalEditor.Controllers
{
    public class PluginLoaderController
    {
        private readonly PluginLoader _pluginLoader;
        private readonly ShapeFactory _shapeFactory;
        private readonly WrapPanel _toolbarPanel;

        public PluginLoaderController(ShapeFactory shapeFactory, WrapPanel toolbarPanel)
        {
            _pluginLoader = new PluginLoader();
            _shapeFactory = shapeFactory;
            _toolbarPanel = toolbarPanel;
        }

        public void LoadPlugin()
        {
            var dlg = new OpenFileDialog { Filter = "DLL files (*.dll)|*.dll" };
            if (dlg.ShowDialog() == true)
            {
                var plugins = _pluginLoader.LoadPlugins(dlg.FileName);
                foreach (var plugin in plugins)
                {
                    plugin.Register(_shapeFactory);
                    AddPluginButton(plugin);
                }
            }
        }

        private void AddPluginButton(IPlugin plugin)
        {
            var button = new Button
            {
                Width = 80,
                Height = 55,
                Content = new StackPanel
                {
                    Children =
                    {
                        new Image { Source = new BitmapImage(new Uri(plugin.IconPath)) },
                        new TextBlock { Text = plugin.DisplayName, HorizontalAlignment = HorizontalAlignment.Center }
                    }
                }
            };

            button.Click += (sender, e) =>
                ((MainWindow)Application.Current.MainWindow).drawCanvas.CurrentShapeType = plugin.ShapeType;

            _toolbarPanel.Children.Add(button);
        }
    }
}