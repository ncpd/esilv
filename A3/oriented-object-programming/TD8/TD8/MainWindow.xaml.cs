using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TD8
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Canvas.SetLeft(yellow_canvas, 0.0);
            Canvas.SetTop(yellow_canvas, 0.0);
            Canvas.SetRight(yellow_canvas, 100.0);
            Canvas.SetBottom(yellow_canvas, 0.0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("T'es naze");
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private Point GetRandomPoint()
        {
            Random m_Random = new Random();
            int x;
            int y;
            x = m_Random.Next((int)red_canvas.Width, (int)(yellow_canvas.Width - red_canvas.Width));
            y = m_Random.Next((int)red_canvas.Height, (int)(yellow_canvas.Height - red_canvas.Height));
            return new Point(x, y);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Point point = GetRandomPoint();

            //AnimateY(Canvas.GetLeft(red_canvas), point.X);
            //AnimateX(Canvas.GetTop(red_canvas), point.Y);
            Canvas.SetTop(red_canvas, point.Y);
            Canvas.SetLeft(red_canvas, point.X);
        }

        private void AnimateY(double x, double new_x)
        {
            Storyboard sb = new Storyboard();
            DoubleAnimation da = new DoubleAnimation(x, new_x, new Duration(new TimeSpan(0, 0, 1)));
            //DoubleAnimation da2 = new DoubleAnimation(x, y, new Duration(new TimeSpan(0, 0, 1)));
            Storyboard.SetTargetProperty(da, new PropertyPath("(Canvas.Top)")); //Do not miss the '(' and ')'
            sb.Children.Add(da);

            

            red_canvas.BeginStoryboard(sb);
            Canvas.SetTop(red_canvas, new_x);
        }

        private void AnimateX(double y, double new_y)
        {
            Storyboard sb = new Storyboard();
            DoubleAnimation da = new DoubleAnimation(y, new_y, new Duration(new TimeSpan(0, 0, 1)));
            //DoubleAnimation da2 = new DoubleAnimation(x, y, new Duration(new TimeSpan(0, 0, 1)));
            Storyboard.SetTargetProperty(da, new PropertyPath("(Canvas.Left)")); //Do not miss the '(' and ')'
            sb.Children.Add(da);

            red_canvas.BeginStoryboard(sb);
            Canvas.SetLeft(red_canvas, new_y);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            red_canvas.HorizontalAlignment = HorizontalAlignment.Left;
            red_canvas.VerticalAlignment = VerticalAlignment.Top;
            //Canvas.SetTop(red_canvas, 0.0);
            //Canvas.SetLeft(red_canvas, 0.0);
        }
        /*
private void Button_Click_1(object sender, RoutedEventArgs e)
{
if(left_box.Text != "")
{
right_box.Text = left_box.Text;
left_box.Text = "";
} else
{
ShowMessage("T'as rien entré !");
}
}

private void Button_Click_2(object sender, RoutedEventArgs e)
{
if (right_box.Text != "")
{
left_box.Text = right_box.Text;
right_box.Text = "";
}
else
{
ShowMessage("T'as rien entré !");
}
}

private void centered_box_TextChanged(object sender, TextChangedEventArgs e)
{
label.Content = "Bonjour " + centered_box.Text;
}*/
    }
}
