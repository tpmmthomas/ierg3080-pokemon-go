using System;


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

public class Pokemon{

}