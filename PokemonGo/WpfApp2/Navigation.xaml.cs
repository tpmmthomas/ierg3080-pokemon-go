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
        public Navigation(string name)
        {
            InitializeComponent();
            p1 = new Player(name);
            //SpawnPokemon
            //SpawnPokeball
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
