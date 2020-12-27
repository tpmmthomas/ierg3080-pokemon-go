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
using WpfAnimatedGif;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace PokemonGo
{
    /// <summary>
    /// Interaction logic for Capture.xaml
    /// </summary>
    public partial class Capture : Window
    {
        private Player p1;
        private PokemonType pkm;
        private CaptureGame game;
        DispatcherTimer starttimer = new System.Windows.Threading.DispatcherTimer();
        DispatcherTimer gametimer = new System.Windows.Threading.DispatcherTimer();
        DispatcherTimer balltimer = new System.Windows.Threading.DispatcherTimer();
        private int timercounter;
        private int ballfalltime;
        private int status; //1: playing, 2: win, 3: lose
        public Capture(Player p, PokemonType x)
        {
            InitializeComponent();
            EllipseX.SetCenter(objective, 384,224);
            EllipseX.SetCenter(moving, 384,224);
            p1 = p;
            pkm = x;
            game = new CaptureGame(x);
            status = 1;
            balltimer.Tick += ballTimer_Tick;
            balltimer.Interval = TimeSpan.FromSeconds(0.1);
            gametimer.Tick += gameTimer_Tick;
            gametimer.Interval = TimeSpan.FromSeconds(0.02);
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
            if (p1.Pokeball_count > 0)
            {
                CountdownTimer();
            }
            else
            {
                PokeballExhaust();
            }
            
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
                    moving.Width = game.Orgwidth;
                    moving.Height = game.Orgwidth;
                    EllipseX.SetCenter(moving, 384, 224);
                    moving.Visibility = Visibility.Visible;
                    gametimer.Start();
                    starttimer.Stop();
                    break;
            }
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            int newwidth = game.NextWidth();
            if (newwidth < 0)
            {
                gametimer.Stop();
                p1.RemovePokeball(1);
                ballcount.Text = p1.Pokeball_count.ToString();
                checkresult();
                return;
            }
            moving.Width = newwidth;
            moving.Height = newwidth;
            EllipseX.SetCenter(moving, 384, 224);
        }
        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return && gametimer.IsEnabled)
            {
                gametimer.Stop();
                p1.RemovePokeball(1);
                ballcount.Text = p1.Pokeball_count.ToString();
                checkresult();
            }
            if(e.Key == Key.A && status == 3)
            {
                losetext.Visibility = Visibility.Hidden;
                poor.Visibility = Visibility.Hidden;
                good.Visibility = Visibility.Hidden;
                excellent.Visibility = Visibility.Hidden;
                if (p1.Pokeball_count > 0)
                {
                    status = 1;
                    game = new CaptureGame(pkm);
                    objective.Visibility = Visibility.Visible;
                    timercounter = 4;
                    starttimer.Start();
                }
                else
                {
                    PokeballExhaust();
                }

            }
            if (e.Key == Key.B && status == 3)
            {
                WindowNavigation.NavigateBack();
            }

        }
        private void checkresult()
        {
            int result = game.checkresult((int)moving.Width);
            if (result == 1)
            {
                excellent.Visibility = Visibility.Visible;
            }
            else if (result == 2)
            { 
                good.Visibility = Visibility.Visible;
            }
            else
            {
                poor.Visibility = Visibility.Visible; 
            }
            catchresult();    
        }
        private void catchresult()
        {
            if (game.IsSuccessful())
            {
                status = 2;
                objective.Visibility = Visibility.Hidden;
                moving.Visibility = Visibility.Hidden;
                WinAnim.Visibility = Visibility.Visible;
                ImageBehavior.SetRepeatBehavior(WinAnim, new RepeatBehavior(1));
                var quicktimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(4) };
                quicktimer.Start();
                quicktimer.Tick += (sender,args) => { 
                    quicktimer.Stop();
                    p1.AddPokemon(new Pokemon(p1.CurrentSerial, pkm));
                    p1.AddStardust(500);
                    wintext.Visibility = Visibility.Visible;
                    var endtimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2.5) };
                    endtimer.Start();
                    endtimer.Tick += (sender2, args2) => { endtimer.Stop();  WindowNavigation.NavigateBack(); };
                };
            }
            else
            {
                status = 3;
                objective.Visibility = Visibility.Hidden;
                moving.Visibility = Visibility.Hidden;
                Moveball.Visibility = Visibility.Visible;
                ballfall();
                var quicktimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
                quicktimer.Start();
                quicktimer.Tick += (sender, args) => {
                    quicktimer.Stop();
                    losetext.Visibility = Visibility.Visible;
                    if (game.Escaped())
                    {
                        escapeAnimate();
                    }
                };
            }
        }
        private void escapeAnimate()
        {
            losetext.Visibility = Visibility.Hidden;
            escapetext.Text = pkm.Name + " has escaped!";
            escapetext.Visibility = Visibility.Visible;
            Program.MoveTo(Bulbasaur, -100, Canvas.GetTop(Bulbasaur), 1);
            Program.MoveTo(Charmander, -100, Canvas.GetTop(Charmander), 1);
            Program.MoveTo(Pikachu, -100, Canvas.GetTop(Pikachu), 1);
            Program.MoveTo(Squirtle, -100, Canvas.GetTop(Squirtle), 1);
            Program.MoveTo(Lapras, -100, Canvas.GetTop(Lapras), 1);
            Program.MoveTo(Snorlax, -100, Canvas.GetTop(Snorlax), 1);
            var quicktimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
            quicktimer.Start();
            quicktimer.Tick += (sender, args) => {
                quicktimer.Stop();
                WindowNavigation.NavigateBack();
            };
        }
        private void ballfall()
        {
            Canvas.SetTop(Moveball, 0);
            ballfalltime = 0;
            balltimer.Start();
        }
        private void ballTimer_Tick(object sender, EventArgs e)
        {
            int movement = (int)(0.5 * 0.1 * ballfalltime * ballfalltime);
            Canvas.SetTop(Moveball, Canvas.GetTop(Moveball) + movement);
            if (Canvas.GetTop(Moveball) > 210)
            {
                balltimer.Stop();
                Moveball.Visibility = Visibility.Hidden;
            }
            ballfalltime++;
        }
        private void PokeballExhaust()
        {
            instruction.Visibility = Visibility.Hidden;
            notenough.Visibility = Visibility.Visible;
            var quicktimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
            quicktimer.Start();
            quicktimer.Tick += (sender, args) => {
                quicktimer.Stop();
                WindowNavigation.NavigateBack();
            };
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
