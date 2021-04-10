using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo{
  private string playerName, characterName, characterClass;
  private int armorClass, healthPoints, initiative;

  public PlayerInfo(string newPlayerName, string newCharacterName, string newCharacterClass, int newArmorClass, int newHealthPoints, int newInitiative){
    this.playerName = newPlayerName;
    this.characterName = newCharacterName;
    this.characterClass = newCharacterClass;
    this.armorClass = newArmorClass;
    this.healthPoints = newHealthPoints;
    this.initiative = newInitiative;
  }

  public PlayerInfo(string newPlayerName, string newCharacterName, string newCharacterClass, int newArmorClass, int newHealthPoints){
    this.playerName = newPlayerName;
    this.characterName = newCharacterName;
    this.characterClass = newCharacterClass;
    this.armorClass = newArmorClass;
    this.healthPoints = newHealthPoints;
    this.initiative = -1;
  }

  public string getPlayerName(){
    return this.playerName;
  }
  public string getCharacterName(){
    return this.characterName;
  }
  public string getCharacterClass(){
    return this.characterClass;
  }
  public int getArmorClass(){
    return this.armorClass;
  }
  public int getHealthPoints(){
    return this.healthPoints;
  }
  public int getInitiative(){
    return this.initiative;
  }
  
  public void setPlayerName(string newPlayerName){
    this.playerName = newPlayerName;
  }
  public void setCharacterName(string newCharacterName){
    this.characterName = newCharacterName;
  }
  public void setCharacterClass(string newCharacterClass){
    this.characterClass = newCharacterClass;
  }
  public void setArmorClass(int newArmorClass){
    this.armorClass = newArmorClass;
  }
  public void setHealthPoints(int newHealthPoints){
    this.healthPoints = newHealthPoints;
  }
  public void setInitiative(int newInitiative){
    this.initiative = newInitiative; 
  }

}

