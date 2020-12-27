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
    /// Interaction logic for Battle.xaml
    /// </summary>
    public partial class Battle : Window
    {
        public Battle()
        {
            InitializeComponent();
            GridChangePokemon.Visibility = Visibility.Collapsed;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigation.NavigateBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GridChangePokemon.Visibility = Visibility.Visible;
        }
        private void ButtonCloseSwitchPokemon(object sender, RoutedEventArgs e)
        {
            GridChangePokemon.Visibility = Visibility.Collapsed;
        }
    }
}
