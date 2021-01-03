using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGo
{
    public class Player
    {
        private string name;
        public string Name
        {
            get { return name; }
        }
        private int pokeball_count;
        public int Pokeball_count
        {
            get { return pokeball_count; }
        }

        private List<Pokemon> OwnedPokemons;
        private int currentSerial;
        public int CurrentSerial
        {
            get { currentSerial++; return currentSerial - 1; }
        }
        private int stardust;
        public int Stardust
        {
            get { return stardust; }
        }
        public Player(string pname)
        {
            name = pname;
            currentSerial = 1;
            OwnedPokemons = new List<Pokemon>();
            stardust = 0;
            pokeball_count = 10;
        }

        public List<Pokemon> GetPokemons()
        {
            return OwnedPokemons;
        }

        public int PokemonCount()
        {
            return OwnedPokemons.Count;
        }

        public void AddPokemon(Pokemon a)
        {
            OwnedPokemons.Add(a);
        }

        public void RemovePokemon(Pokemon a)
        {
            int index = OwnedPokemons.IndexOf(a);
            if (index != -1)
            {
                OwnedPokemons.RemoveAt(index);
            }
        }

        public void AddPokeball()
        {
            pokeball_count += 5;
            if (pokeball_count > 500)
                pokeball_count = 500;
        }

        public void RemovePokeball(int x)
        {
            if (pokeball_count - x > 0)
                pokeball_count -= x;
            else
            {
                pokeball_count = 0;
            }
        }
        public void AddStardust(int x)
        {
            stardust += x;
        }
        public void ConsumeStardust(int x)
        {
            stardust -= x;
            if (stardust < 0)
                stardust = 0;
        }
    }
}
