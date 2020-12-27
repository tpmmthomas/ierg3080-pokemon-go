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
    /// Interaction logic for Capture.xaml
    /// </summary>
    public partial class Capture : Window
    {
        private Player p1;
        private PokemonType pkm;
        private Random rand;
        private double acceleration;
        private int orgwidth;
        private int countelapsed;
        System.Windows.Threading.DispatcherTimer starttimer = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer gametimer = new System.Windows.Threading.DispatcherTimer();
        private int timercounter;
        public Capture(Player p, PokemonType x)
        {
            InitializeComponent();
            EllipseX.SetCenter(objective, 384,224);
            EllipseX.SetCenter(moving, 384,224);
            p1 = p;
            pkm = x;
            rand = new Random();
            switch (pkm.Name)
            {
                case "Bulbasaur":
                    Bulbasaur.Visibility = Visibility.Visible;
                    break;
                case "Squirtle":
                    Squirtle.Visibility = Visibility.Visible;
                    break;
                case "Charmander":
                    Charmander.Visibility = Visibility.Visible;
                    break;
                case "Pikachu":
                    Pikachu.Visibility = Visibility.Visible;
                    break;
                case "Snorlax":
                    Snorlax.Visibility = Visibility.Visible;
                    break;
                case "Lapras":
                    Lapras.Visibility = Visibility.Visible;
                    break;
            }
            ballcount.Text = p1.Pokeball_count.ToString();
            CountdownTimer();
        }
        private void CountdownTimer()
        {
            timercounter = 4;
            starttimer.Tick += startTimer_Tick;
            starttimer.Interval = TimeSpan.FromSeconds(1);
            starttimer.Start();
        }
        private void startTimer_Tick(object sender, EventArgs e)
        {
            timercounter--;
            switch (timercounter)
            {
                case 3:
                    three.Visibility = Visibility.Visible;
                    break;
                case 2:
                    three.Visibility = Visibility.Collapsed;
                    two.Visibility = Visibility.Visible;
                    break;
                case 1:
                    two.Visibility = Visibility.Collapsed;
                    one.Visibility = Visibility.Visible;
                    break;
                case 0:
                    one.Visibility = Visibility.Collapsed;
                    instruction.Visibility = Visibility.Collapsed;
                    orgwidth = rand.Next(350, 520);
                    acceleration = rand.Next(1, 31) / (double)10;
                    moving.Width = orgwidth;
                    moving.Height = orgwidth;
                    EllipseX.SetCenter(moving, 384, 224);
                    moving.Visibility = Visibility.Visible;
                    gametimer.Tick += gameTimer_Tick;
                    gametimer.Interval = TimeSpan.FromSeconds(0.1);
                    gametimer.Start();
                    starttimer.Stop();
                    break;
            }
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {

        }
        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                gametimer.Stop();
            }
        }
        private void objective_MouseEnter(object sender, MouseEventArgs e)
        {
            WindowNavigation.NavigateBack();
        }
    }
}
