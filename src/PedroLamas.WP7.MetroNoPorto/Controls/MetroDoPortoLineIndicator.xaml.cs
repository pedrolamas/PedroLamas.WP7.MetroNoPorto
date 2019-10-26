using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PedroLamas.WP7.MetroNoPorto.Controls
{
    public partial class MetroDoPortoLineIndicator : UserControl
    {
        #region Properties

        public int LineIndex
        {
            get { return (int)GetValue(LineIndexProperty); }
            set { SetValue(LineIndexProperty, value); }
        }

        public static readonly DependencyProperty LineIndexProperty =
            DependencyProperty.Register("LineIndex", typeof(int), typeof(MetroDoPortoLineIndicator), new PropertyMetadata(0, LineIndexPropertyChanged));

        public static void LineIndexPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.Dispatcher.BeginInvoke(() =>
            {
                var rootElement = (FrameworkElement)VisualTreeHelper.GetChild(d, 0);

                var lineIdTextBlock = (TextBlock)rootElement.FindName("LineIdTextBlock");
                var backgroundEllipse = (Ellipse)rootElement.FindName("BackgroundEllipse");

                switch ((int)e.NewValue)
                {
                    case 1:
                        lineIdTextBlock.Text = "A";
                        backgroundEllipse.Fill = new SolidColorBrush(Color.FromArgb(255, 0, 153, 204));

                        break;

                    case 2:
                        lineIdTextBlock.Text = "B";
                        backgroundEllipse.Fill = new SolidColorBrush(Color.FromArgb(255, 220, 30, 0));

                        break;

                    case 3:
                        lineIdTextBlock.Text = "C";
                        backgroundEllipse.Fill = new SolidColorBrush(Color.FromArgb(255, 153, 204, 0));

                        break;

                    case 4:
                        lineIdTextBlock.Text = "D";
                        backgroundEllipse.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 190, 0));

                        break;

                    case 5:
                        lineIdTextBlock.Text = "E";
                        backgroundEllipse.Fill = new SolidColorBrush(Color.FromArgb(255, 100, 90, 149));

                        break;

                    case 6:
                        lineIdTextBlock.Text = "F";
                        backgroundEllipse.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 136, 0));

                        break;
                }
            });
        }

        #endregion

        public MetroDoPortoLineIndicator()
        {
            InitializeComponent();
        }
    }
}