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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace PokemonGo
{
    /// <summary>
    /// Interaction logic for Navigation.xaml
    /// </summary>
    public partial class Navigation : Window
    {
        private Player p1;
        private Random rand;
        private Dictionary<location, Image> PokeballLoc;

        System.Windows.Threading.DispatcherTimer balltimer = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer regulartimer = new System.Windows.Threading.DispatcherTimer();
        public Navigation(string name)
        {
            InitializeComponent();
            p1 = new Player(name);
            rand = new Random();
            PokeballLoc = new Dictionary<location, Image>();
            SpawnPokeball();
            //SpawnPokeball
            RegularTimer();
        }
        private void RegularTimer()
        {
            regulartimer.Tick += regulartimer_Tick;
            regulartimer.Interval = TimeSpan.FromSeconds(0.1);
            regulartimer.Start();
        }
        private void regulartimer_Tick(object sender, EventArgs e)
        {
            foreach (var ballLoc in PokeballLoc)
            {
                if (Math.Abs(Canvas.GetLeft(player1) - ballLoc.Key.left) < 30 && Math.Abs(Canvas.GetTop(player1) - ballLoc.Key.top) < 30)
                {
                    ballLoc.Value.Visibility = Visibility.Collapsed;
                    PokeballLoc.Remove(ballLoc.Key);
                    p1.AddPokeball();
                    break;
                }
            }
            debug1.Text = Canvas.GetLeft(player1).ToString() + "," + Canvas.GetTop(player1).ToString();
            debug2.Text = p1.Pokeball_count.ToString();
        }
        private void SpawnPokeball()
        {
            balltimer.Tick += balltimer_Tick;
            balltimer.Interval = TimeSpan.FromSeconds(5);
            balltimer.Start();
        }
        private void balltimer_Tick(object sender, EventArgs e)
        {
            if(PokeballLoc.Count <= 5)
            {
                Image ball1 = new Image();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri("pokeball.png", UriKind.Relative);
                bitmap.EndInit();
                ball1.Source = bitmap;
                MyCanvas.Children.Add(ball1);
                ball1.Width = 28;
                int top = rand.Next(0, 360);
                int left = rand.Next(0, 740);
                Canvas.SetTop(ball1, top);
                Canvas.SetLeft(ball1, left);
                PokeballLoc.Add(new location(left, top), ball1);
            }
            
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                if (Canvas.GetTop(player1) >= 350)
                    return;
                Canvas.SetTop(player1, Canvas.GetTop(player1) + 10);
            }
            else if (e.Key == Key.Up)
            {
                if (Canvas.GetTop(player1) <= -5)
                    return;
                Canvas.SetTop(player1, Canvas.GetTop(player1) - 10);
            }
            else if (e.Key == Key.Left)
            {
                if (Canvas.GetLeft(player1) <= -5)
                    return;
                Canvas.SetLeft(player1, Canvas.GetLeft(player1) - 10);
            }
            else if (e.Key == Key.Right)
            {
                if (Canvas.GetLeft(player1) >= 730)
                    return;
                Canvas.SetLeft(player1, Canvas.GetLeft(player1) + 10);
            }

        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        private class location 
        {
            public double left;
            public double top;
            public location(double x, double y)
            {
                left = x;
                top = y;
            }
        
        }

    }
}
