using GraphicalEditor.Model.Shapes;
using GraphicalEditor.Model.Services;
using GraphicalEditor.Controllers;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace GraphicalEditor
{
    public partial class MyCanvas : UserControl
    {
        private ShapeBase _currentShape;
        private bool _isDrawing;

        public List<ShapeBase> ShapesList { get; } = new List<ShapeBase>();
        public string CurrentShapeType { get; set; }
        public DrawingSettingsController DrawingSettingsController { get; set; }

        public MyCanvas()
        {
            InitializeComponent();
            Background = Brushes.White;
            MouseDown += cnvDrawingArea_MouseDown;
            MouseMove += cnvDrawingArea_MouseMove;
            MouseUp += cnvDrawingArea_MouseUp;
        }

        private bool IsMultiClickShape =>
            CurrentShapeType == "Polyline" || CurrentShapeType == "Polygon";

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
                    _currentShape = ShapeFactory.Instance.CreateShape(CurrentShapeType);
                    ApplyDrawingSettings(_currentShape);
                    _currentShape.Initialize(pos);
                    _currentShape.FinalizeDrawing(pos);
                    _isDrawing = true;
                    CaptureMouse();
                }
                else
                {
                    _currentShape.FinalizeDrawing(pos);
                }
                InvalidateVisual();
            }
            else
            {
                _currentShape = ShapeFactory.Instance.CreateShape(CurrentShapeType);
                ApplyDrawingSettings(_currentShape);
                _currentShape.Initialize(pos);
                _isDrawing = true;
                CaptureMouse();
            }
        }

        private void cnvDrawingArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDrawing || _currentShape == null) return;
            var pos = e.GetPosition(cnvDrawingArea);
            _currentShape.Update(pos);
            Dispatcher.Invoke(InvalidateVisual, DispatcherPriority.Render);
        }

        private void cnvDrawingArea_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_isDrawing || _currentShape == null) return;
            if (IsMultiClickShape) return;

            if (e.ChangedButton == MouseButton.Left)
            {
                var pos = e.GetPosition(cnvDrawingArea);
                _currentShape.FinalizeDrawing(pos);
                _isDrawing = false;
                ReleaseMouseCapture();
                ShapesList.Add(_currentShape);
                _currentShape = null;
                InvalidateVisual();
            }
        }

        private void EndMultiClickShape()
        {
            _isDrawing = false;
            ReleaseMouseCapture();
            ShapesList.Add(_currentShape);
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
            foreach (var shape in ShapesList)
                shape.Draw(dc);
            if (_isDrawing && _currentShape != null)
                _currentShape.Draw(dc);
        }
    }
}
