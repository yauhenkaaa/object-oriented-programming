using GraphicalEditor.GraphicalPrimitives;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphicalEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClearCanvas() { 
            drawCanvas.Children.Clear();
        }

        private void AddShapeToCanvas(ShapeBase shape)
        {
            ClearCanvas();
            var element = shape.CreateShape();
            drawCanvas.Children.Add(element);
        }

        private void btnLine_Click(object sender, RoutedEventArgs e) => AddShapeToCanvas(new LineShape());


        private void btnRectangle_Click(object sender, RoutedEventArgs e) => AddShapeToCanvas(new RectangleShape());

        private void btnEllipse_Click(object sender, RoutedEventArgs e) => AddShapeToCanvas(new EllipseShape());

        private void btnPolyline_Click(object sender, RoutedEventArgs e) => AddShapeToCanvas(new PolylineShape());


        private void btnPolygon_Click(object sender, RoutedEventArgs e) => AddShapeToCanvas(new PolygonShape());

    }
}