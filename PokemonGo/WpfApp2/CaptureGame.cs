using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGo
{
    public class CaptureGame
    {
        private Random rand;
        private double Acceleration;
        public int Orgwidth { get; }
        private int currentWidth;
        private int countelapsed;
        private PokemonType pkm;
        private int result;
        private bool isSuccessful;
        private int targetWidth;
        public PokemonType CurPokemon
        {
            get { return pkm; }
        }

        public CaptureGame(PokemonType x)
        {
            pkm = x;
            rand = new Random();
            Acceleration = rand.Next(1, 31) / (double)10;
            Orgwidth = rand.Next(350, 520);
            currentWidth = Orgwidth;
            targetWidth = 180;
            countelapsed = 0;
            result = 0;
        }
        public int NextWidth()
        {
            countelapsed++;
            currentWidth = (int) (currentWidth - 0.5 * Acceleration * countelapsed * countelapsed / (double)100);
            return currentWidth;
        }
        public int checkresult(int finalwidth)
        {
            if (Math.Abs(finalwidth - targetWidth)<25)
            { //90% common, 70% rare
                result = 1;
            }
            else if (Math.Abs(finalwidth - targetWidth) < 65)
            { //50% common, 30% rare
                result = 2;
            }
            else
            { //30% common, 10% rare, if cannot catch, probability 
                result = 3;
            }
            return result;
        }
        public bool IsSuccessful() 
        {
            if(result == 1)
            {
                if(pkm.Rarity == 1) //common
                {
                    if (rand.Next(0, 10) < 7)
                        isSuccessful = true;
                    else
                        isSuccessful = false;
                }
                else
                {
                    if (rand.Next(0, 10) < 5)
                        isSuccessful = true;
                    else
                        isSuccessful = false;
                }
            }
            else if (result == 2)
            {
                if (pkm.Rarity == 1) 
                {
                    if (rand.Next(0, 10) < 5)
                        isSuccessful = true;
                    else
                        isSuccessful = false;
                }
                else
                {
                    if (rand.Next(0, 10) < 2)
                        isSuccessful = true;
                    else
                        isSuccessful = false;
                }
            }
            else
            {
                if (pkm.Rarity == 1)
                {
                    if (rand.Next(0, 10) < 1)
                        isSuccessful = true;
                    else
                        isSuccessful = false;
                }
                else
                {
                    if (rand.Next(0, 10) < 0)
                        isSuccessful = true;
                    else
                        isSuccessful = false;
                }
            }
            return isSuccessful;
        }
        public bool Escaped()
        {
            if (isSuccessful) return false;
            int decider = rand.Next(0, 10);
            if (pkm.Rarity == 2 && decider < 5)
                return true;
            if (pkm.Rarity == 1 && decider < 2)
                return true;
            return false;
        }
    }
    
}
