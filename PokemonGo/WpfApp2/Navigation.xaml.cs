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
        private int pokeballCount;
        private Random rand;
        System.Windows.Threading.DispatcherTimer balltimer = new System.Windows.Threading.DispatcherTimer();
        public Navigation(string name)
        {
            InitializeComponent();
            p1 = new Player(name);
            pokeballCount = 0;
            rand = new Random();
            SpawnPokeball();
            //SpawnPokeball
        }
        private void SpawnPokeball()
        {
            balltimer.Tick += balltimer_Tick;
            balltimer.Interval = TimeSpan.FromSeconds(20);
            balltimer.Start();
        }
        private void balltimer_Tick(object sender, EventArgs e)
        {
            Image ball1 = new Image();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("pokeball.png", UriKind.Relative);
            bitmap.EndInit();
            ball1.Source = bitmap;
            MyCanvas.Children.Add(ball1);
            ball1.Width = 28;
            Canvas.SetTop(ball1, rand.Next(0, 360));
            Canvas.SetLeft(ball1, rand.Next(0, 740));
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                if (Canvas.GetTop(player1) >= 20)
                    return;
                Canvas.SetTop(player1, Canvas.GetTop(player1) + 10);
            }
            else if (e.Key == Key.Up)
            {
                if (Canvas.GetTop(player1) <= -340)
                    return;
                Canvas.SetTop(player1, Canvas.GetTop(player1) - 10);
            }
            else if (e.Key == Key.Left)
            {
                if (Canvas.GetLeft(player1) <= -240)
                    return;
                Canvas.SetLeft(player1, Canvas.GetLeft(player1) - 10);
            }
            else if (e.Key == Key.Right)
            {
                if (Canvas.GetLeft(player1) >= 500)
                    return;
                Canvas.SetLeft(player1, Canvas.GetLeft(player1) + 10);
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
