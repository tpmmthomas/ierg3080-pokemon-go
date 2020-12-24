using System;
using System.Collections.Generic;

public delegate void WinMethod();
public delegate void LoseMethod();

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

    public BattleGym(Pokemon x, WinMethod a, LoseMethod b){
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
    public void PlayerMove(int move){ //pass in index of next attack move
        //to be implemented. 

        if(OpponentPokemon.Hit(PlayerPokemon.)
    }

    public int OpponentMove(){ //return the attack move chosen.

    }
}