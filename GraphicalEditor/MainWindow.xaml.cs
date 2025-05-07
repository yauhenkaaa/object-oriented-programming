using GraphicalEditor.Controllers;
using GraphicalEditor.Model.Services;
using GraphicalEditor.Model.Shapes;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicalEditor
{
    public partial class MainWindow : Window
    {
        private readonly UndoRedoController _undoRedoController;
        private readonly DrawingSettingsController _drawingSettingsController = new DrawingSettingsController();
        private readonly PluginLoaderController _pluginLoaderController;

        private readonly SerializationController _serializationController;
        public MainWindow()
        {
            InitializeComponent();
            _serializationController = new SerializationController();
            var undoRedoService = new UndoRedoService();
            _undoRedoController = new UndoRedoController(undoRedoService, drawCanvas);

            drawCanvas.SetUndoRedoController(_undoRedoController);
            drawCanvas.DrawingSettingsController = _drawingSettingsController;

            _pluginLoaderController = new PluginLoaderController(
                ShapeFactory.Instance,
                (WrapPanel)FindName("toolbarPanel")
            );
        }

        private void btnLine_Click(object sender, RoutedEventArgs e) => drawCanvas.CurrentShapeType = "Line";
        private void btnRectangle_Click(object sender, RoutedEventArgs e) => drawCanvas.CurrentShapeType = "Rectangle";
        private void btnEllipse_Click(object sender, RoutedEventArgs e) => drawCanvas.CurrentShapeType = "Ellipse";
        private void btnPolyline_Click(object sender, RoutedEventArgs e) => drawCanvas.CurrentShapeType = "Polyline";
        private void btnPolygon_Click(object sender, RoutedEventArgs e) => drawCanvas.CurrentShapeType = "Polygon";
        private void sldThickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
            => _drawingSettingsController.UpdateStrokeThickness(e.NewValue);
        private void LineColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (e.NewValue.HasValue)
                _drawingSettingsController.UpdateStrokeColor(e.NewValue.Value);
        }
        private void FillColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (e.NewValue.HasValue)
                _drawingSettingsController.UpdateFillColor(e.NewValue.Value);
        }
        private void btnUndo_Click(object sender, RoutedEventArgs e) => _undoRedoController.Undo();
        private void btnRedo_Click(object sender, RoutedEventArgs e) => _undoRedoController.Redo();

        private void UpdateTitle()
        {
            Title = _serializationController.HasCurrentFile
                ? $"Graphical Editor - {_serializationController.CurrentFileName}"
                : "Graphical Editor";
        }

        private void File_New_Click(object sender, RoutedEventArgs e)
        {
            _serializationController.New(drawCanvas.ShapesList);
            drawCanvas.InvalidateVisual();
            UpdateTitle();
        }

        private void File_Open_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog { Filter = "JSON (*.json)|*.json" };
            if (dlg.ShowDialog() == true)
            {
                var list = _serializationController.Open(dlg.FileName);
                drawCanvas.ShapesList.Clear();
                drawCanvas.ShapesList.AddRange(list);
                drawCanvas.InvalidateVisual();
                UpdateTitle();
            }
        }

        private void File_Save_Click(object sender, RoutedEventArgs e)
        {
            if (_serializationController.HasCurrentFile)
                _serializationController.Save(drawCanvas.ShapesList);
            else
                File_SaveAs_Click(sender, e);
            UpdateTitle();
        }

        private void File_SaveAs_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog { Filter = "JSON (*.json)|*.json" };
            if (dlg.ShowDialog() == true)
            {
                _serializationController.SaveAs(dlg.FileName, drawCanvas.ShapesList);
                UpdateTitle();
            }
        }
        private void btnAddPlugin_Click(object sender, RoutedEventArgs e)
        {
            _pluginLoaderController.LoadPlugin();
        }
    }
}