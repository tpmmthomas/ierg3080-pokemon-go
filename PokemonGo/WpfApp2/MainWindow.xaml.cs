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
        public wpf()
        {
            InitializeComponent();
            Navigation.Navigate(new Welcome());
        }

    }
}