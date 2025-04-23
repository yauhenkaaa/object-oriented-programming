using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using GraphicalEditor.Model.Shapes;
using GraphicalEditor.Controllers;
using GraphicalEditor.Model.Commands;
using System.Windows.Threading;

namespace GraphicalEditor
{
    public partial class MyCanvas : UserControl
    {
        private ShapeBase _currentShape;
        private bool _isDrawing;
        private UndoRedoController _undoRedoController;

        public List<ShapeBase> ShapesList { get; } = new List<ShapeBase>();
        public string CurrentShapeType { get; set; }
        public DrawingSettingsController DrawingSettingsController { get; set; }

        public void SetUndoRedoController(UndoRedoController controller)
        {
            _undoRedoController = controller;
        }

        public MyCanvas()
        {
            InitializeComponent();
            MouseDown += cnvDrawingArea_MouseDown;
            MouseMove += cnvDrawingArea_MouseMove;
            MouseUp += cnvDrawingArea_MouseUp;
        }

        private bool IsMultiClickShape => CurrentShapeType == "Polyline" || CurrentShapeType == "Polygon";

        private void cnvDrawingArea_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DrawingSettingsController == null) return;
            var pos = e.GetPosition(cnvDrawingArea);

            if (e.ChangedButton == MouseButton.Right && IsMultiClickShape && _isDrawing)
            {
                EndMultiClickShape();
                return;
            }

            if (e.ChangedButton != MouseButton.Left) return;

            if (IsMultiClickShape)
            {
                if (!_isDrawing)
                {
                    StartNewShape(pos);
                }
                else
                {
                    _currentShape.FinalizeDrawing(pos);
                }
                InvalidateVisual();
            }
            else
            {
                StartNewShape(pos);
            }
        }

        private void StartNewShape(Point pos)
        {
            _currentShape = ShapeFactory.Instance.CreateShape(CurrentShapeType);
            ApplyDrawingSettings(_currentShape);
            _currentShape.Initialize(pos);
            _isDrawing = true;
            CaptureMouse();
        }

        private void cnvDrawingArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDrawing || _currentShape == null) return;
            _currentShape.Update(e.GetPosition(cnvDrawingArea));
            Dispatcher.Invoke(InvalidateVisual, DispatcherPriority.Render);
        }

        private void cnvDrawingArea_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_isDrawing || _currentShape == null || IsMultiClickShape) return;

            if (e.ChangedButton == MouseButton.Left)
            {
                var pos = e.GetPosition(cnvDrawingArea);
                _currentShape.FinalizeDrawing(pos);
                CompleteDrawing();
            }
        }

        private void CompleteDrawing()
        {
            _isDrawing = false;
            ReleaseMouseCapture();
            _undoRedoController.RegisterCommand(new AddShapeCommand(_currentShape, ShapesList));
            _currentShape = null;
            InvalidateVisual();
        }

        private void EndMultiClickShape()
        {
            _isDrawing = false;
            ReleaseMouseCapture();
            _undoRedoController.RegisterCommand(new AddShapeCommand(_currentShape, ShapesList));
            _currentShape = null;
            InvalidateVisual();
        }

        private void ApplyDrawingSettings(ShapeBase shape)
        {
            shape.StrokeColor = DrawingSettingsController.Settings.StrokeColor;
            shape.FillColor = DrawingSettingsController.Settings.FillColor;
            shape.StrokeThickness = DrawingSettingsController.Settings.StrokeThickness;
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            foreach (var shape in ShapesList) shape.Draw(dc);
            if (_isDrawing && _currentShape != null) _currentShape.Draw(dc);
        }
    }
}