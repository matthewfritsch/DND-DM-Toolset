using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo{
  private string playerName, characterName, characterClass;
  private int armorClass, healthPoints, initiative;
//constructor assuming initiative already exists
  public PlayerInfo(string newPlayerName, string newCharacterName, string newCharacterClass, int newArmorClass, int newHealthPoints, int newInitiative){
    this.playerName = newPlayerName;
    this.characterName = newCharacterName;
    this.characterClass = newCharacterClass;
    this.armorClass = newArmorClass;
    this.healthPoints = newHealthPoints;
    this.initiative = newInitiative;
  }
//constructor assuming initiative does not yet exist
  public PlayerInfo(string newPlayerName, string newCharacterName, string newCharacterClass, int newArmorClass, int newHealthPoints){
    this.playerName = newPlayerName;
    this.characterName = newCharacterName;
    this.characterClass = newCharacterClass;
    this.armorClass = newArmorClass;
    this.healthPoints = newHealthPoints;
    this.initiative = -1;
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
//accessor for the AC of the character (int32)
  public int getArmorClass(){
    return this.armorClass;
  }
//accessor for the HP of the player (int32)
  public int getHealthPoints(){
    return this.healthPoints;
  }
/*
 * accessor for the initiative (int32).
 * returns -1 if the initiative does NOT exist.
*/ 
  public int getInitiative(){
    return this.initiative;
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
//mutator for the AC of the character
  public void setArmorClass(int newArmorClass){
    this.armorClass = newArmorClass;
  }
//mutator for the HP of the character
  public void setHealthPoints(int newHealthPoints){
    this.healthPoints = newHealthPoints;
  }
//mutator for the initiative of the character
  public void setInitiative(int newInitiative){
    this.initiative = newInitiative; 
  }
/*
 * sets the initiative back to default (-1)
*/
  public void resetInitiative(){
    this.initiative = -1;
  }
}

