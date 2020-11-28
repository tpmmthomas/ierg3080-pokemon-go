using System;
using System.Collections.Generic;

public class Player{
    private string name;
    public string Name{
        get{ return name;}
    }
    private int pokeball_count;
    public int Pokeball_count{
        get{return pokeball_count;}
    }

    private List<Pokemon> OwnedPokemons;

    private int stardust;
    public int Stardust{
        get{return stardust;}
    }

    public Player(string pname){
        name = pname;
        OwnedPokemons = new List<Pokemon>();
        stardust = 0;
        pokeball_count = 10;
    }

    public List<Pokemon> GetPokemons(){
        return OwnedPokemons;
    }

    public int PokemonCount(){
        return OwnedPokemons.Count;
    }

    public void AddPokemon(Pokemon a){
        OwnedPokemons.Add(a);
    }

    public void RemovePokemon(Pokemon a){
        int index = OwnedPokemons.IndexOf(a);
        if(index!= -1){
            OwnedPokemon.RemoveAt(index);
        }
    }

    public void AddPokeball(){
        pokeball_count++;
    }

    public void RemovePokeball(int x){
        if(pokeball_count-x>0)
            pokeball_count -= x;
        else
        {
            pokeball_count = 0;
        }
    }

}

public enum Rarity{
    Common,
    Rare,
    UltraRare
}

public class PokemonType{
    private Rarity raretype;
    public Rarity Raretype{
        get{return raretype;}
    }
    private string name;
    public string Name{
        get{return name;}
    }
    HashSet<AttackMoves> AtkMovesList;
    private int upperWeight;
    public int UpperWeight{
        get{return upperWeight};
    }
    private int lowerWeight;
    public int LowerWeight{
        get{return lowerWeight};
    }
    private int upperHeight;
    public int UpperHeight{
        get{return upperHeight};
    }
    private int lowerHeight;
    public int LowerHeight{
        get{return lowerHeight};
    }
    private int initialCP;
    public int InitialCP{
        get{return initialCP};
    }
    private int value; //in CP
    public int Value{
        get{return value};
    }
    private int initialHP;
    public int InitialHP{
        get{return initialHP};
    }

    public PokemonType(){
        //To be implemented, get a list
    }

}

public class AttackMoves{
    private string name;
    public string Name{
        return name;
    }
}


public class Pokemon{
    private string name;
    public 
}