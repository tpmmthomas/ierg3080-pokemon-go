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
using System.Windows.Shapes;
using System.Windows.Navigation;
using WpfAnimatedGif;

namespace PokemonGo
{
    public partial class Manage : Page
    {
        private Player p1;
        private Pokemon selectedPokemon = null;
        private int powerUpRequestedStardust = 300;
        private int evolveRequestedStardust = 1000;
        private int sellObtainStardust = 100;
        public Manage(Player p1)
        {
            InitializeComponent();
            this.p1 = p1;
            PlayerPokemonAmount.Text = p1.PokemonCount().ToString();
            stardustAmount.Text = p1.Stardust.ToString();
            if (p1.PokemonCount() > 0)
            {
                PlayerPokemonList.ItemsSource = p1.GetPokemons();
                selectedPokemon = selectPokemon(p1.GetPokemons().First());   // Default select the first pokemon to display the detail
            }
        }
        private Pokemon selectPokemon(Pokemon selectedPokemon)
        {
            this.selectedPokemon = selectedPokemon;
            SelectedPokemonName.Text = selectedPokemon.Name;
            SelectedPokemonCP.Text = selectedPokemon.GetCP.ToString();
            pokemonHP.Text = "HP " + selectedPokemon.GetHP.ToString() + "/" + selectedPokemon.MaxHP.ToString();
            atkmv1.Text = selectedPokemon.Moveslist[0].name;
            atkdmg1.Text = selectedPokemon.Moveslist[0].attackPoints.ToString();
            atkmv2.Text = selectedPokemon.Moveslist[1].name;
            atkdmg2.Text = selectedPokemon.Moveslist[1].attackPoints.ToString();
            atkmv3.Text = selectedPokemon.Moveslist[2].name;
            atkdmg3.Text = selectedPokemon.Moveslist[2].attackPoints.ToString();
            SelectedPokemonHPCurrent.Width = SelectedPokemonHPFull.Width * (selectedPokemon.GetHP / (double)selectedPokemon.MaxHP);
            pokemonWeight.Text = selectedPokemon.Weight.ToString()+"kg";
            pokemonHeight.Text = selectedPokemon.Height.ToString()+"m";

            // Update pokemon Image
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(@"Images/pokemon/" + selectedPokemon.TypeName + ".gif", UriKind.Relative); // TODO, still has bug
            image.EndInit();
            ImageBehavior.SetAnimatedSource(SelectedPokemonImage, image);

            // Update button
            ButtonPowerup.Opacity = (p1.Stardust >= powerUpRequestedStardust) ? 1 : 0.5;
            ButtonEvolve.Opacity = (p1.Stardust >= evolveRequestedStardust) ? 1 : 0.5;
            ButtonSell.Opacity = 1;
            ButtonRename.Opacity = 1;

            // Refresh my pokemon list & my info
            stardustAmount.Text = p1.Stardust.ToString();
            CollectionViewSource.GetDefaultView(PlayerPokemonList.ItemsSource).Refresh();

            return selectedPokemon;
        }

        private void ButtonClickEvolve(object sender, RoutedEventArgs e)
        {
            if (selectedPokemon != null)
            {
                if (p1.Stardust >= evolveRequestedStardust)
                {
                    int envoleResult = p1.GetPokemons().Find(x => x.Id == selectedPokemon.Id).Evolve();
                    if (envoleResult == 1)
                    {
                        MessageBox.Show("The pokemon cannot be evolve anymore!");
                    }
                    else 
                    {
                        p1.ConsumeStardust(evolveRequestedStardust);
                        selectPokemon(p1.GetPokemons().Find(x => x.Id == selectedPokemon.Id));
                        this.NavigationService.Refresh();
                        MessageBox.Show("The pokemon evolve successfully!");
                    }
                }
                else
                {
                    MessageBox.Show("You need 1000 Stardust to power up the pokemon!");
                }
            }
        }

        private void ButtonClickPowerUp(object sender, RoutedEventArgs e)
        {
            if (selectedPokemon != null)
            {
                if (p1.Stardust >= powerUpRequestedStardust)
                {
                    p1.AddStardust(-powerUpRequestedStardust);
                    p1.GetPokemons().Find(x => x.Id == selectedPokemon.Id).PowerUP();
                    selectPokemon(p1.GetPokemons().Find(x => x.Id == selectedPokemon.Id));
                    MessageBox.Show("Power uped!");
                }
                else
                {
                    MessageBox.Show("You need 200 Stardust to power up the pokemon!");
                }
            }
        }

        private void ButtonClickRename(object sender, RoutedEventArgs e)
        {
            String newName = "test";
            if (selectedPokemon != null)
            {
                p1.GetPokemons().Find(x => x.Id == selectedPokemon.Id).Rename(newName);
                SelectedPokemonName.Text = newName;
            }
            MessageBox.Show("Rename Developing!");
        }
        private void ButtonClickSell(object sender, RoutedEventArgs e)
        {
            if (selectedPokemon != null)
            {
                p1.GetPokemons().RemoveAll(x => x.Id == selectedPokemon.Id);
                p1.AddStardust(sellObtainStardust);
                if (p1.GetPokemons().Count > 0)
                {
                    MessageBox.Show("Sold out successfully!, obtain " + sellObtainStardust + "Stardust!");
                    selectPokemon(p1.GetPokemons().First());
                }
                else
                {
                    MessageBox.Show("All of your pokemons has been sold out! Let go to catach some :D");
                    Program.status = 0;
                    NavigationService.GoBack();
                }
            }
        }
        private void ButtonClickSelectPokemon(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            Pokemon selectedPkm = button.DataContext as Pokemon;
            selectPokemon(selectedPkm);
        }

        private void exitButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Program.status = 0;
            NavigationService.GoBack();
        }
    }
}