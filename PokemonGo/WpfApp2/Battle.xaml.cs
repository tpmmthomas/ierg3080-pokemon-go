using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WpfAnimatedGif;

namespace PokemonGo
{
    public partial class Battle : Page
    {
        DispatcherTimer battleTimer = new DispatcherTimer();
        DispatcherTimer playerTimer = new DispatcherTimer();
        DispatcherTimer opponentTimer = new DispatcherTimer();
        public Battle(Player p1)
        {
            InitializeComponent();
            List<Pokemon> playerPokemon = p1.GetPokemons();
            if(playerPokemon.Count > 0)
            {
                BattlePokemonName.Text = playerPokemon[0].Name;

                // Update pokemon Image
                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(@"Images/pokemon/back/" + playerPokemon[0].Name + ".gif", UriKind.Relative); // TODO, still has bug
                image.EndInit();
                ImageBehavior.SetAnimatedSource(BattlePokemonImage, image);
                GridChangePokemon.Visibility = Visibility.Collapsed;
            }
        }
        private void ButtonRunAway(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
            //WindowNavigation.NavigateBack();
        }
        private void ButtonConfirmSwitchPokemon(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Developing!");
        }
        private void ButtonSwitchPokemon(object sender, RoutedEventArgs e)
        {
            GridChangePokemon.Visibility = Visibility.Visible;

            // Animation to show the pokemon list
            var sb = new Storyboard();
            var ta = new ThicknessAnimation();
            ta.BeginTime = new TimeSpan(0);
            ta.SetValue(Storyboard.TargetNameProperty, "GridChangePokemon");
            Storyboard.SetTargetProperty(ta, new PropertyPath(MarginProperty));

            ta.From = new Thickness(0, 0, 0, -300);
            ta.To = new Thickness(0, 0, 0, -10);
            ta.Duration = new Duration(TimeSpan.FromSeconds(0.3));

            sb.Children.Add(ta);
            sb.Begin(this);

        }
        private void ButtonCloseSwitchPokemon(object sender, RoutedEventArgs e)
        {
            // Animation to hide the pokemon list
            var sb = new Storyboard();
            var ta = new ThicknessAnimation();
            ta.BeginTime = new TimeSpan(0);
            ta.SetValue(Storyboard.TargetNameProperty, "GridChangePokemon");
            Storyboard.SetTargetProperty(ta, new PropertyPath(MarginProperty));

            ta.From = new Thickness(0, 0, 0, -10);
            ta.To = new Thickness(0, 0, 0, -300);
            ta.Duration = new Duration(TimeSpan.FromSeconds(0.3));

            sb.Children.Add(ta);
            sb.Begin(this);

            //GridChangePokemon.Visibility = Visibility.Collapsed;
        }
    }
}
