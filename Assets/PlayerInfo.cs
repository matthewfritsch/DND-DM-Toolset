using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

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

public class PlayerInfo{
  private StatusCondition statusCondition;
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
    this.statusCondition = 0;
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
  /*
 * accessor for the status (StatusCondition).
*/ 
  public StatusCondition getStatus(){
    return this.statusCondition;
  }
  public int getStatusAsInt(){
    return (int)this.getStatus();
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
  //mutator for the status of the character
  public void setStatus(StatusCondition newStatus){
    this.statusCondition = newStatus;
  }
  public void setStatus(int newStatus){
    this.statusCondition = (StatusCondition)newStatus;
  }
/*
 * sets the initiative back to default (-1)
*/
  public void resetInitiative(){
    this.initiative = -1;
  }
}

