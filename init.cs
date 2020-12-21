using System;
using System.Collections.Generic;

public class program{
    public static void init(string file,HashSet<PokemonType> common, HashSet<PokemonType> rare, HashSet<PokemonType> ultrarare){
        using(var reader = new StreamReader(file)){
            var firstline = reader.ReadLine();
            while(!reader.EndOfStream){
                var line = reader.ReadLine();
                var values = line.Split(',');
                PokemonType p1 = new PokemonType(values[1],values[0],values[2],values[3],values[4],values[5],values[6],values[7],values[8],values[9],values[10],values[11],values[12],values[13]);
                if(p1.Rarity == 1)
                    common.Add(p1);
                else if (p1.Rarity == 2)
                    rare.Add(p1);
                else
                    ultrarare.Add(p1);
            }
        }
    }

    
}


//for unit testing
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
        get{return upperWeight;}
    }
    private int lowerWeight;
    public int LowerWeight{
        get{return lowerWeight;}
    }
    private int upperHeight;
    public int UpperHeight{
        get{return upperHeight;}
    }
    private int lowerHeight;
    public int LowerHeight{
        get{return lowerHeight;}
    }
    private int initialCP;
    public int InitialCP{
        get{return initialCP;}
    }
    private int initialHP;
    public int InitialHP{
        get{return initialHP;}
    }

    public PokemonType(int rar, string nam, int uw, int lw, int uh, int lh, int icp, int val, int ihp,string mv1, int dmg1, string mv2,int dmg2, string mv3, int dmg3){
        rarity = rar;
        name = nam;
        lowerWeight = lw;
        upperWeight = uw;
        lowerHeight = lh;
        upperHeight = uh;
        initialCP = icp;
        initialHP = ihp;
        atkMovesList = new HashSet<AttackMoves>();
        atkMovesList.Add(new AttackMoves(mv1,dmg1));
        atkMovesList.Add(new AttackMoves(mv2,dmg2));
        atkMovesList.Add(new AttackMoves(mv3,dmg3));
    }

}