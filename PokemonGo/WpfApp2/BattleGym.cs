using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGo
{
    public delegate void WinMethod(Pokemon _PlayerPokemon, Pokemon _OpponentPokemon);//if this is too difficult to do can delete, ask OpponentMOve and Playermove to return status instead.
    public delegate void LoseMethod(Pokemon _PlayerPokemon, Pokemon _OpponentPokemon);

    class BattleGym
    {
        private Pokemon PlayerPokemon;
        public Pokemon GetPlayerPokemon
        {
            get { return PlayerPokemon; }
        }
        private Pokemon OpponentPokemon; //computer
        public Pokemon GetOpponentPokemon
        {
            get { return OpponentPokemon; }
        }
        private int CurrentTurn; //1: human 2: copmuter
        public int GetCurrentTurn
        {
            get { return CurrentTurn; }
        }
        public int[] GetSkillTime
        {
            get { return skilltime; }
        }
        private double PlayerCriticalRate;
        private double OpponentCriticalRate;
        private Random rand;
        private WinMethod win;
        private LoseMethod lose;
        private int[] skilltime;
        private int[] opponentSkilltime;

        public BattleGym(Pokemon _PlayerPokemon,Pokemon _OpponentPokemon, WinMethod _Win, LoseMethod _Lose)
        { //Winmethod and Losemethod passed in from presenter module (Define the change in view?)
            rand = new Random();
            PlayerPokemon = _PlayerPokemon;
            OpponentPokemon = _OpponentPokemon;
            win = _Win;
            lose = _Lose;
            skilltime = new int[3] { 20, 5, 1 }; // How many times can be used for each skill
            opponentSkilltime = new int[3] { 20, 5, 1 }; // How many times can be used for each skill for opponentPokemon
            CurrentTurn = 1;
            if (PlayerPokemon.GetCP > 2000)
            {
                PlayerCriticalRate = 0.3;
            }
            else if (PlayerPokemon.GetCP > 1000)
            {
                PlayerCriticalRate = 0.2;
            }
            else
            {
                PlayerCriticalRate = 0.1;
            }
            if (OpponentPokemon.GetCP > 2000)
            {
                OpponentCriticalRate = 0.3;
            }
            else if (OpponentPokemon.GetCP > 1000)
            {
                OpponentCriticalRate = 0.2;
            }
            else
            {
                OpponentCriticalRate = 0.1;
            }
        }
        public bool PlayerMove(int move)
        { //pass in index of next attack move
            if (CurrentTurn != 1)
                return false;
            bool critical = rand.Next(0, 100) / (double)100 < PlayerCriticalRate ? true : false;
            skilltime[move]--;
            if (critical)
            {
                if (OpponentPokemon.Hit(PlayerPokemon.Moveslist[move].attackPoints * 2))
                {
                    win(PlayerPokemon, OpponentPokemon);
                }
            }
            else
            {
                if (OpponentPokemon.Hit(PlayerPokemon.Moveslist[move].attackPoints)){
                    win(PlayerPokemon, OpponentPokemon);
                }
            }
            CurrentTurn = 2;
            return critical;
        }

        public bool OpponentMove(ref string moveChosen)
        {
            if (CurrentTurn != 2)
                return false;
            int move = rand.Next(0, 3);
            moveChosen = OpponentPokemon.Moveslist[move].name;
            bool critical = rand.Next(0, 100) / (float)100 < OpponentCriticalRate ? true : false;
            opponentSkilltime[move]--;
            if (critical)
            {
                if (PlayerPokemon.Hit(OpponentPokemon.Moveslist[move].attackPoints * 2))
                {
                    lose(PlayerPokemon, OpponentPokemon);
                }
            }
            else
            {
                if (PlayerPokemon.Hit(OpponentPokemon.Moveslist[move].attackPoints)){
                    lose(PlayerPokemon, OpponentPokemon);
                }
            }
            CurrentTurn = 1;
            return critical;
        }
    }
}
