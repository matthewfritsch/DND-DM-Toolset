using System.Collections;
using System.Collections.Generic;
using System;

[Flags]
public enum StatusCondition : short{
    NONE          = 0b0000000000000000, // 0
    BLINDED       = 0b0000000000000001, // 1
    CHARMED       = 0b0000000000000010, // 2
    DEAFENED      = 0b0000000000000100, // 4
    FRIGHTENED    = 0b0000000000001000, // 8
    GRAPPLED      = 0b0000000000010000, // 16
    INCAPACITATED = 0b0000000000100000, // 32
    INVISIBLE     = 0b0000000001000000, // 64
    PARALYZED     = 0b0000000010000000, // 128
    PETRIFIED     = 0b0000000100000000, // 256
    POISONED      = 0b0000001000000000, // 512
    PRONE         = 0b0000010000000000, // 1024
    RESTRAINED    = 0b0000100000000000, // 2048
    STUNNED       = 0b0001000000000000, // 4096
    UNCONSCIOUS   = 0b0010000000000000, // 8192
    EXHAUSTION    = 0b0100000000000000  // 16384
};

public enum Alignment{
    LAWFUL_GOOD     ,
    NEUTRAL_GOOD    ,
    CHAOTIC_GOOD    ,
    LAWFUL_NEUTRAL  ,
    TRUE_NEUTRAL    ,
    CHAOTIC_NEUTRAL ,
    LAWFUL_EVIL     ,
    NEUTRAL_EVIL    ,
    CHAOTIC_EVIL    ,
    UNALIGNED
}

public enum Size{
    TINY,
    SMALL,
    MEDIUM,
    LARGE,
    HUGE,
    GARGANTUAN
}

public abstract class BeingInfo{

    protected StatusCondition statusCondition;
    protected Alignment alignment;
    protected Size size;
    protected short HP, AC, currentHP, initiative;
    protected short STR, DEX, CON, INT, WIS, CHA;

    public BeingInfo(){
        statusCondition = StatusCondition.NONE;
        alignment = Alignment.UNALIGNED;
        size = Size.MEDIUM;
        HP = AC = currentHP = STR = DEX = CON = INT = WIS = CHA = -1;
    }

    public BeingInfo(string newAlignment, Size newSize, short newHP, short newAC, short newSTR, short newDEX, short newCON, short newINT, short newWIS, short newCHA){
        alignment = toAlignment(newAlignment);
        size = newSize;
        HP = newHP;
        AC = newAC;
        STR = newSTR;
        DEX = newDEX;
        CON = newCON;
        INT = newINT;
        WIS = newWIS;
        CHA = newCHA;
        currentHP = newHP;
        statusCondition = StatusCondition.NONE;
    }
    

    public StatusCondition getStatusCondition(){
        return statusCondition;
    }
    public void setStatusCondition(StatusCondition toSet){
        statusCondition = toSet;
    }

    public Alignment getAlignment(){
        return alignment;
    }
    public void setAlignment(Alignment toSet){
        alignment = toSet;
    }

    public Size getSize(){
        return size;
    }
    public void setSize(Size toSet){
        size = toSet;
    }

    public short getHP()
    {
        return HP;
    }
    public void setHP(short toSet)
    {
        HP = toSet;
    }

    public short getAC()
    {
        return AC;
    }
    public void setAC(short toSet)
    {
        AC = toSet;
    }

    public short getCurrentHP()
    {
        return currentHP;
    }
    public void setCurrentHP(short toSet)
    {
        currentHP = toSet;
    }

    public short getInitiative(){
        return initiative;
    }
    public void setInitiative(short toSet){
        initiative = toSet;
    }

    public short getStat_STR()
    {
        return STR;
    }
    public void setStat_STR(short toSet)
    {
        STR = toSet;
    }

    public short getStat_DEX()
    {
        return DEX;
    }
    public void setStat_DEX(short toSet)
    {
        DEX = toSet;
    }

    public short getStat_CON()
    {
        return CON;
    }
    public void setStat_CON(short toSet)
    {
        CON = toSet;
    }

    public short getStat_INT()
    {
        return INT;
    }
    public void setStat_INT(short toSet)
    {
        INT = toSet;
    }

    public short getStat_WIS()
    {
        return WIS;
    }
    public void setStat_WIS(short toSet)
    {
        WIS = toSet;
    }

    public short getStat_CHA()
    {
        return CHA;
    }
    public void setStat_CHA(short toSet)
    {
        CHA = toSet;
    }

    private Alignment toAlignment(string newAL){
        switch (newAL){
            case "LG":
                return Alignment.LAWFUL_GOOD;
            case "LN":
                return Alignment.LAWFUL_NEUTRAL;
            case "LE":
                return Alignment.LAWFUL_EVIL;
            case "NG":
                return Alignment.NEUTRAL_GOOD;
            case "TN":
                return Alignment.TRUE_NEUTRAL;
            case "NE":
                return Alignment.NEUTRAL_EVIL;
            case "CG":
                return Alignment.CHAOTIC_GOOD;
            case "CN":
                return Alignment.CHAOTIC_NEUTRAL;
            case "CE":
                return Alignment.CHAOTIC_EVIL;
        };
        return Alignment.UNALIGNED;
    }
}