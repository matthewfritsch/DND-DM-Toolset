using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class MonsterInfo : BeingInfo 
{
    [UnityEngine.SerializeField]
    private string monsterName; //Ancient Gold Dragon
    [UnityEngine.SerializeField]
    private string type; //Dragon
    [UnityEngine.SerializeField]
    private double CR; //Challenge Rating (30)

    public MonsterInfo() : base(){
        monsterName = "NAMELESS";
        type = "TYPELESS";
        CR = -1;
    }

    public MonsterInfo(string MName, string MType, string MAlignment, Size MSize, double MChallengeRating, short MArmorClass, short MHP, short MSTR, short MDEX, short MCON, short MINT, short MWIS, short MCHA) : base(MAlignment, MSize, MHP, MArmorClass, MSTR, MDEX, MCON, MINT, MWIS, MCHA){
        monsterName = MName;
        type = MType;
        CR = MChallengeRating;
    }

    public string getMonsterName(){
        return monsterName;
    }
    public void setMonsterName(string newMN){
        monsterName = newMN;
    }

    public string getType(){
        return type;
    }
    public void setType(string newType){
        type = newType;
    }

    public double getChallengeRating(){
        return CR;
    }
    public void setChallengeRating(double newCR){
        CR = newCR;
    }

}
