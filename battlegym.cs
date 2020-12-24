using System;
using System.Collections.Generic;

public delegate void WinMethod(Pokemon a, Pokemon b);
public delegate void LoseMethod(Pokemon a, Pokemon b);

public class BattleGym{
    private Pokemon PlayerPokemon;  
    public Pokemon GetPlayerPokemon{
        get{return PlayerPokemon;}
    }
    private Pokemon OpponentPokemon; //computer
    public Pokemon GetOpponentPokemon{
        get{return OpponentPokemon;}
    }
    private int CurrentTurn; //1: human 2: copmuter
    public int GetCurrentTurn{
        get{return CurrentTurn;}
    }
    private float PlayerCriticalRate;
    private float OpponentCriticalRate;
    private Random rand;
    private WinMethod win;
    private LoseMethod lose;

    public BattleGym(Pokemon x, WinMethod a, LoseMethod b){ //Winmethod and Losemethod passed in from presenter module (Define the change in view?)
        rand = new Random();
        PlayerPokemon = x;
        win = a;
        lose = b;
        CurrentTurn = 1;
        if(PlayerPokemon.GetCP > 2000){
            PlayerCriticalRate = 0.3;
        }
        else if (PlayerPokemon.GetCP > 1000){
            PlayerCriticalRate = 0.2;
        }
        else{
            PlayerCriticalRate = 0.1;
        }
        if(OpponentPokemon.GetCP > 2000){
            OpponentCriticalRate = 0.3;
        }
        else if (OpponentPokemon.GetCP > 1000){
            OpponentCriticalRate = 0.2;
        }
        else{
            OpponentCriticalRate = 0.1;
        }
    }
    public int PlayerMove(int move){ //pass in index of next attack move
        //to be implemented. 
        int critical = rand.Next(0,100)/(float)100 < PlayerCriticalRate ? 1:0;
        if(critical){
            if(OpponentPokemon.Hit(PlayerPokemon.moveslist[move].attackPoints*2)){
                win();
            }
        }
        else{
            if(OpponentPokemon.Hit(PlayerPokemon.moveslist[move].attackPoints){
                win();
            }
        }
        return critical;
    }

    public int OpponentMove(){ //return the attack move chosen.

    }
}