using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PokemonGo
{
    class WildPokemon
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
