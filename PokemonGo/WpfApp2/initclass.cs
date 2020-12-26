﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace PokemonGo
{
    public class Program
    {
        public static void Init(string file, HashSet<PokemonType> common, HashSet<PokemonType> rare, HashSet<PokemonType> ultrarare)
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
    }
}