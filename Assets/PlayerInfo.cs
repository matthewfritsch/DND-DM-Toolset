using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerInfo : BeingInfo{
  
  private string playerName, characterName, characterClass;
//constructor assuming initiative already exists
  public PlayerInfo(string newPlayerName, string newCharacterName, string newCharacterClass, short newArmorClass, short newMaxHealth, short newInitiative) : base(){
    this.playerName = newPlayerName;
    this.characterName = newCharacterName;
    this.characterClass = newCharacterClass;
    this.AC =  newArmorClass;
    this.HP =  newMaxHealth;
    this.currentHP = this.HP;
    this.initiative = newInitiative;
  }
//constructor assuming initiative does not yet exist
  public PlayerInfo(string newPlayerName, string newCharacterName, string newCharacterClass, int newArmorClass, int newMaxHealth) : base(){
    this.playerName = newPlayerName;
    this.characterName = newCharacterName;
    this.characterClass = newCharacterClass;
    this.AC = (short)newArmorClass;
    this.HP = (short)newMaxHealth;
    this.currentHP = (short)this.HP;
  }
//accessor for the player name (string)
  public string getPlayerName(){
    return this.playerName;
  }
//accessor for the character's name (string)
  public string getCharacterName(){
    return this.characterName;
  }
//accessor for the character's class type (string)
  public string getCharacterClass(){
    return this.characterClass;
  }

//mutator for the name of the player
  public void setPlayerName(string newPlayerName){
    this.playerName = newPlayerName;
  }
//mutator for the name of the character
  public void setCharacterName(string newCharacterName){
    this.characterName = newCharacterName;
  }
//mutator for the class type of the character
  public void setCharacterClass(string newCharacterClass){
    this.characterClass = newCharacterClass;
  }
/*
 * sets the initiative back to default (-1)
*/
  public void resetInitiative(){
    this.initiative = -1;
  }
}

