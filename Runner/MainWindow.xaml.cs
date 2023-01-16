using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Diagnostics;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Runner
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //fields
        //colision
        Rect playerBoxColider;
        Rect groundBoxColider;
        Rect objectBoxColider;

        bool jumping = false;
        int force = 200;
        int speed = 50;
        int score = 0;

        Random random = new Random();

        bool gameOver = false;

        //graphics
        double numImage = 1;

        ImageBrush playerBrush = new ImageBrush();
        ImageBrush bgBrush = new ImageBrush();
        ImageBrush obstaclBrush = new ImageBrush();

        //rendering
        TimeSpan prevTime;
        Stopwatch stopwatch = Stopwatch.StartNew();
        public MainWindow()
        {
            InitializeComponent();
            Init();
            CompositionTarget.Rendering += CompositionTarget_Rendering;

            prevTime = stopwatch.Elapsed;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            TimeSpan currentTime = stopwatch.Elapsed;
            double delta = (currentTime - prevTime).TotalSeconds;
            prevTime = currentTime;

            Update(delta);
        }

        public void Init()
        {
            bgBrush.ImageSource = new BitmapImage(
                new Uri("pack://application:,,,/Images/background.gif"));

            bg1.Fill = bgBrush;
            bg2.Fill = bgBrush;

            RunAimator(1);
        }

        public void Update(double delta)
        {
            //gravity
            Canvas.SetTop(player, Canvas.GetTop(player) + force * delta);

            //movebg
            Canvas.SetLeft(bg1, Canvas.GetLeft(bg1) - speed * delta);
            Canvas.SetLeft(bg2, Canvas.GetLeft(bg2) - speed * delta);

            playerBoxColider = new Rect(Canvas.GetLeft(player),Canvas.GetTop(player),player.Width,player.Height);
            groundBoxColider = new Rect(Canvas.GetLeft(ground), Canvas.GetTop(ground), ground.Width, ground.Height);

            if (playerBoxColider.IntersectsWith(groundBoxColider))
            {
                force = 0;
                numImage += 0.2f;
                if (numImage > 8)
                {
                    numImage = 1;
                }                
                RunAimator(numImage);
            }

            
        }

        public void StartGame()
        {

        }

        public void RunAimator(double index)
        {
            playerBrush.ImageSource = new BitmapImage(
                new Uri($"pack://application:,,,/Images/run_0{(int)index}.gif"));

            player.Fill = playerBrush;
        }

        private void myCanvas_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void myCanvas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {

            }
        }
    }
}
