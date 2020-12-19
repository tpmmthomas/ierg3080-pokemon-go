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

public class PokemonType{
    private int rarity; //1: common, 2: rare, 3: ultra-rare
    public int Rarity{
        get{return rarity;}
    }
    private string name;
    public string Name{
        get{return name;}
    }
    private HashSet<AttackMoves> atkMovesList;
    public HashSet<AttackMoves> AtkMovesList{
        get{return atkMovesList;}
    }
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

    public PokemonType(int rar, string nam, int uw, int lw, int lh, int uh, int icp, int val, int ihp,string mv1, int dmg1, string mv2,int dmg2, string mv3, int dmg3){
        rarity = rar;
        name = nam;
        lowerWeight = lw;
        upperWeight = uw;
        lowerHeight = lh;
        upperHeight = uh;
        initialCP = icp;
        value = val;
        initialHP = ihp;
        atkMovesList = new HashSet<AttackMoves>();
        atkMovesList.Add(new AttackMoves(mv1,dmg1));
        atkMovesList.Add(new AttackMoves(mv2,dmg2));
        atkMovesList.Add(new AttackMoves(mv3,dmg3));
    }

}

public class AttackMoves{
    public string name;
    public int attackPoints;
    public AttackMoves(string n,int ap){
        name = n;
        attackPoints = ap;
    }

}


public class Pokemon{
    private string name;
    public string Name{
        get{return name;}
    }
    private HashSet<AttackMoves> moveslist;
    public HashSet<AttackMoves> Moveslist{
        get{return moveslist;}
    }
    private int weight;
    public int Weight{
        get{return weight;}
    }
    private int height;
    public int Height{
        get{return height;}
    }
    private int CP;
    public int GetCP{
        get{return CP;}
    }
    private int value;
    public int Value{
        get{return value;}
    }
    private int maxHP;
    public int MaxHP{
        get{return maxHP;}
    }
    private int evolvestate; //1,2,3, 1 lowest, 3 highest
    private int HP;
    public int GetHP{
        get{return HP;}
    }
    public Pokemon(PokemonType x){
        Random rand = new Random();
        name = x.Name;
        moveslist = x.AtkMovesList;
        weight = rand.Next(x.LowerWeight,x.UpperWeight+1);
        height = rand.Next(x.LowerHeight,x.UpperHeight+1);
        CP = x.InitialCP;
        value = x.Value;
        maxHP = x.InitialHP;
        evolvestate = 1;
        HP = maxHP;
    }

    public int Hit(int atkhp){
        if(HP-atkhp>0){
            HP -= atkhp;
            return 0;
        }
        else{
            HP=0;
            return 1;
        }
    }

    public void Heal(){
        HP = maxHP;
    }

    public int evolve(){
        if(evolvestate == 3)
            return 1;
        evolvestate++;
        name = name+"+";
        weight += rand.Next(1,10);
        height += rand.Next(1,10)/10;
        foreach(AttackMoves x in moveslist){
            x.attackPoints += rand.Next(5,20);
        }
        CP += rand.Next(60,200);
        if(CP>2500) CP = 2500;
        maxHP += rand.Next(40,100);
        if(maxHP>300) maxHP = 300;
        HP = maxHP;
        return 0;
    }

    public int PowerUP(){
        CP += rand.Next(30,100);
        if(CP>2500) CP = 2500;
        foreach(AttackMoves x in moveslist){
            x.attackPoints += rand.Next(0,2);
        }
    }

}