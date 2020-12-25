using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace pokemon
{
    public class program
    {
        public static void init(string file, HashSet<PokemonType> common, HashSet<PokemonType> rare, HashSet<PokemonType> ultrarare)
        {
            using (var reader = new StreamReader(file))
            {
                var firstline = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',', (char)StringSplitOptions.None); //don't know why this method is missing using local mono compiler? Must test again in WPF.
                    PokemonType p1 = new PokemonType(int.Parse(values[1]), values[0], float.Parse(values[2]), float.Parse(values[3]), float.Parse(values[4]), float.Parse(values[5]), int.Parse(values[6]), int.Parse(values[7]), values[8], int.Parse(values[9]), values[10], int.Parse(values[11]), values[12], int.Parse(values[13]));
                    if (p1.Rarity == 1)
                        common.Add(p1);
                    else if (p1.Rarity == 2)
                        rare.Add(p1);
                    else
                        ultrarare.Add(p1);
                }
            }
        }
        public static void SpawnPokemon()
        {
            //to be implemented with presenter since mostly about using timer and a simple random to gen commpn/rare pokemon
            //ultra rare pokemons only in battle gym
        }

        //[STAThread]
        public static void Main()
        {
            HashSet<PokemonType> c1 = new HashSet<PokemonType>();
            HashSet<PokemonType> c2 = new HashSet<PokemonType>();
            HashSet<PokemonType> c3 = new HashSet<PokemonType>();
            init("pokemon.csv", c1, c2, c3);
            foreach (PokemonType x in c1)
            {
                Console.WriteLine("{0},{1},{2}", x.Name, x.InitialCP, x.UpperHeight);
            }
            foreach (PokemonType x in c2)
            {
                Console.WriteLine("{0},{1},{2}", x.Name, x.InitialCP, x.UpperHeight);
            }
            foreach (PokemonType x in c3)
            {
                Console.WriteLine("{0},{1},{2}", x.Name, x.InitialCP, x.UpperHeight);
            }
            /*
            // WPF
            wpf _window = new wpf();  //Create an instance of your window.
            System.Windows.Application _wpfApplication = new System.Windows.Application();  //Create an instance of a new Application
            _wpfApplication.Run(_window);   //Run this Application by passing the window object as the argument
			*/
        }
    }
}