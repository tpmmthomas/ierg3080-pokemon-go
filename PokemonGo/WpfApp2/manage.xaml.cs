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
using WpfAnimatedGif;

namespace PokemonGo
{
    /// <summary>
    /// Interaction logic for manage.xaml
    /// </summary>
    public partial class Manage : Page
    {
        private Player p1;
        private Pokemon selectedPokemon = null;
        public Manage(Player p1)
        {
            InitializeComponent();
            this.DataContext = this;
            this.p1 = p1;
            if (p1.GetPokemons().Count > 0)
            {
                selectedPokemon = selectPokemon(p1.GetPokemons().First());   // Default select the first pokemon to display the detail
            }
            else
            {
                MessageBox.Show("You don't have any pokemon!");
            }
        }
        public string[] Files
        { get; set; }
        private void PokemonSingleViewTiny_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private Pokemon selectPokemon(Pokemon selectedPokemon)
        {
            //selectedPokemonName.Text = selectedPokemon.Name; //Sorry since have error I temporarily comment out first.
            //selectedPokemonCP.Text = selectedPokemon.GetCP.ToString();

            // Update pokemon Image
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(@"Pokemon(ToBeUsed)/" + selectedPokemon.Name + ".gif", UriKind.Relative); // TODO, still has bug
            image.EndInit();
            //ImageBehavior.SetAnimatedSource(SelectedPokemonImage, image);

            return selectedPokemon;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
            //WindowNavigation.NavigateBack();
        }
        private void ButtonClickEvolve(object sender, RoutedEventArgs e)
        {
            if (selectedPokemon != null)
            {
                int envoleResult = p1.GetPokemons().Find(x => x.Id == selectedPokemon.Id).Evolve();
                if(envoleResult == 1)
                {
                    MessageBox.Show("The pokemon cannot be evolve anymore!");
                }
                else
                {
                    MessageBox.Show("The pokemon evolve successfully!");
                }
            }
        }
        private void ButtonClickPowerUp(object sender, RoutedEventArgs e)
        {
            if (selectedPokemon != null)
            {
                p1.GetPokemons().Find(x => x.Id == selectedPokemon.Id).PowerUP();
                MessageBox.Show("Power uped!");
            }
        }
        private void ButtonClickRename(object sender, RoutedEventArgs e)
        {
            String newName = "test";
            if (selectedPokemon != null)
            {
                p1.GetPokemons().Find(x => x.Id == selectedPokemon.Id).Rename(newName);
                //SelectedPokemonName.Text = newName; 
            }
            MessageBox.Show("Rename Developing!");
        }
        private void ButtonClickSell(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sell Developing!");
        }
    }
}
