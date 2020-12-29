using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WpfAnimatedGif;


namespace PokemonGo
{
    /// <summary>
    /// Interaction logic for Navigation.xaml
    /// </summary>
    public partial class Navigation : Page
    {
        private Player p1;
        private Random rand;
        private Dictionary<location, Image> PokeballLoc;
        private Dictionary<location, WildPokemon> PokemonLoc;
        HashSet<PokemonType> common;
        HashSet<PokemonType> rare;
        HashSet<PokemonType> ultrarare;
        DispatcherTimer balltimer = new DispatcherTimer();
        DispatcherTimer regulartimer = new DispatcherTimer();
        DispatcherTimer pokemontimer = new DispatcherTimer();
        public Navigation(string name)
        {
            InitializeComponent();
            Program.status = 0;
            p1 = new Player(name);
            rand = new Random();
            PokeballLoc = new Dictionary<location, Image>();
            PokemonLoc = new Dictionary<location, WildPokemon>();
            common = new HashSet<PokemonType>();
            rare = new HashSet<PokemonType>();
            ultrarare = new HashSet<PokemonType>();
            Program.Init("pokemon.csv", common, rare, ultrarare);
            SpawnPokeball();
            SpawnPokemon();
            RegularTimer();
            PlayerPokemonList.ItemsSource = p1.GetPokemons();
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
            foreach (var pkmLoc in PokemonLoc)
            {
                if (Math.Abs(Canvas.GetLeft(player1) - pkmLoc.Key.left) < 75 && Math.Abs(Canvas.GetTop(player1) - pkmLoc.Key.top) < 75) //pokemon only appear when near player
                {
                    pkmLoc.Value.pokemonImage.Visibility = Visibility.Visible;
                }
                else
                {
                    pkmLoc.Value.pokemonImage.Visibility = Visibility.Hidden;
                }
            }
            foreach (var pkmLoc in PokemonLoc)
            {
                if (Math.Abs(Canvas.GetLeft(player1) - pkmLoc.Key.left) < 30 && Math.Abs(Canvas.GetTop(player1) - pkmLoc.Key.top) < 30 && Program.status == 0)
                {
                    pkmLoc.Value.pokemonImage.Visibility = Visibility.Collapsed;
                    PokemonLoc.Remove(pkmLoc.Key);
                    Program.status = 1;
                    this.NavigationService.Navigate(new Capture(p1, pkmLoc.Value.pokemonStat));
                    break;
                }
            }
            if (Math.Abs(Canvas.GetLeft(player1) - 140) < 20 && Math.Abs(Canvas.GetTop(player1) - 55) < 20 && Program.status == 0)
            {
                Canvas.SetTop(player1, 335);
                Canvas.SetLeft(player1, 229);
                Program.status = 1;
                this.NavigationService.Navigate(new Battle(p1));
            }
           
            if (Math.Abs(Canvas.GetLeft(player1) - 319) < 20 && Math.Abs(Canvas.GetTop(player1) - 225) < 20 && Program.status == 0)
            {
                Canvas.SetTop(player1, 335);
                Canvas.SetLeft(player1, 229);
                Program.status = 1;
                this.NavigationService.Navigate(new Manage(p1));
            }
            debug1.Text = p1.PokemonCount().ToString();
            debug2.Text = Canvas.GetTop(player1).ToString() + "," + Canvas.GetLeft(player1).ToString();
        }
        private void SpawnPokemon()
        {
            pokemontimer.Tick += pokemontimer_Tick;
            pokemontimer.Interval = TimeSpan.FromSeconds(2);//testing
            pokemontimer.Start();
        }
        private void pokemontimer_Tick(object sender, EventArgs e)
        {
            if (Program.status == 1)
                return;
            int decideRarity = rand.Next(0, 100);
            if (decideRarity<97 && PokemonLoc.Count <= 5)
            {
                int chosen = rand.Next(0, common.Count);
                int i = 0;
                PokemonType chosenPokemon = null;
                foreach(PokemonType x in common)
                {
                    if (i == chosen)
                    {
                        chosenPokemon = x;
                        break;
                    }
                    i++;
                }
                Image pkm1 = new Image();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri("Images/pokemon/" + chosenPokemon.Name+".gif", UriKind.Relative);
                bitmap.EndInit();
                pkm1.Source = bitmap;
                ImageBehavior.SetAnimatedSource(pkm1, bitmap);
                ImageBehavior.SetRepeatBehavior(pkm1,System.Windows.Media.Animation.RepeatBehavior.Forever);
                NavigationCanvas.Children.Add(pkm1);
                pkm1.Width = 32;
                int top = rand.Next(0, 360);
                int left = rand.Next(0, 740);
                while((top>=0&&top<=45&&left>=80&&left<=190)||(top >= 185 && top <= 235 && left >= 290 && left <= 350)||exists(top,left))//exclude battle gym and home
                {
                    top = rand.Next(0, 360);
                    left = rand.Next(0, 740);
                }
                Canvas.SetTop(pkm1, top);
                Canvas.SetLeft(pkm1, left);
                pkm1.Visibility = Visibility.Hidden;
                PokemonLoc.Add(new location(left, top), new WildPokemon(pkm1,chosenPokemon));
            }
            else if(PokemonLoc.Count <= 5)
            {
                int chosen = rand.Next(0, rare.Count);
                int i = 0;
                PokemonType chosenPokemon = null;
                foreach (PokemonType x in rare)
                {
                    if (i == chosen)
                    {
                        chosenPokemon = x;
                        break;
                    }
                    i++;
                }
                Image pkm1 = new Image();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri("Images/pokemon/" + chosenPokemon.Name + ".gif", UriKind.Relative);
                bitmap.EndInit();
                pkm1.Source = bitmap;
                ImageBehavior.SetAnimatedSource(pkm1, bitmap);
                ImageBehavior.SetRepeatBehavior(pkm1, System.Windows.Media.Animation.RepeatBehavior.Forever);
                NavigationCanvas.Children.Add(pkm1);
                pkm1.Width = 32;
                int top = rand.Next(0, 360);
                int left = rand.Next(0, 740);
                while ((top >= 0 && top <= 45 && left >= 80 && left <= 190) || (top >= 185 && top <= 235 && left >= 290 && left <= 350) || exists(top, left))//exclude battle gym and home
                {
                    top = rand.Next(0, 360);
                    left = rand.Next(0, 740);
                }
                Canvas.SetTop(pkm1, top);
                Canvas.SetLeft(pkm1, left);
                pkm1.Visibility = Visibility.Hidden;
                PokemonLoc.Add(new location(left, top), new WildPokemon(pkm1, chosenPokemon));
            }

        }
        private bool exists(int top, int left)
        {
            foreach(var x in PokemonLoc)
            {
                if(Math.Abs(x.Key.top - top) < 40 && Math.Abs(x.Key.left - left) < 40)
                    return true;
            }
            return false;
        }
        private void SpawnPokeball()
        {
            balltimer.Tick += balltimer_Tick;
            balltimer.Interval = TimeSpan.FromSeconds(10);
            balltimer.Start();
        }
        private void balltimer_Tick(object sender, EventArgs e)
        {
            if (Program.status == 1)
                return;
            if (PokeballLoc.Count <= 5)
            {
                Image ball1 = new Image();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri("Images/pokeball.png", UriKind.Relative);
                bitmap.EndInit();
                ball1.Source = bitmap;
                NavigationCanvas.Children.Add(ball1);
                ball1.Width = 28;
                int top = rand.Next(0, 360);
                int left = rand.Next(0, 740);
                Canvas.SetTop(ball1, top);
                Canvas.SetLeft(ball1, left);
                PokeballLoc.Add(new location(left, top), ball1);
            }
            
        }

        private void Nav_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
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
        private class WildPokemon
        {
            public Image pokemonImage;
            public PokemonType pokemonStat;
            public WildPokemon(Image x, PokemonType y)
            {
               pokemonImage = x;
               pokemonStat = y;
            }

        }
    }
}
