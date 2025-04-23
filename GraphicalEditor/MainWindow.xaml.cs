using GraphicalEditor.Controllers;
using GraphicalEditor.Model.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GraphicalEditor
{
    public partial class MainWindow : Window
    {
        private readonly UndoRedoController _undoRedoController;
        private readonly DrawingSettingsController _drawingSettingsController = new DrawingSettingsController();

        public MainWindow()
        {
            InitializeComponent();

            var undoRedoService = new UndoRedoService();
            _undoRedoController = new UndoRedoController(undoRedoService, drawCanvas);

            drawCanvas.SetUndoRedoController(_undoRedoController);
            drawCanvas.DrawingSettingsController = _drawingSettingsController;
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
    }
}