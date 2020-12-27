using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PokemonGo
{
    public partial class wpf : Window
    {
        HashSet<PokemonType> common;
        HashSet<PokemonType> rare;
        HashSet<PokemonType> ultrarare;

        public wpf()
        {
            InitializeComponent();
            common = new HashSet<PokemonType>();
            rare = new HashSet<PokemonType>();
            ultrarare = new HashSet<PokemonType>();
            Program.Init("pokemon.csv",common,rare,ultrarare);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            GridCursor.Margin = new Thickness(10 + (150 * index), 0, 0, 0);

            switch (index)
            {
                case 0:
                    GridMain.Background = Brushes.Aquamarine;
                    break;
                case 1:
                    GridMain.Background = Brushes.Beige;
                    break;
                case 2:
                    GridMain.Background = Brushes.CadetBlue;
                    break;
                case 3:
                    GridMain.Background = Brushes.DarkBlue;
                    break;
                case 4:
                    GridMain.Background = Brushes.Firebrick;
                    break;
                case 5:
                    GridMain.Background = Brushes.Gainsboro;
                    break;
                case 6:
                    GridMain.Background = Brushes.HotPink;
                    break;
            }
        }

        private void exitbutton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Return)
            {
                WindowNavigation.NavigateTo(new Navigation(yourname.Text));
            }
        }
    }
}