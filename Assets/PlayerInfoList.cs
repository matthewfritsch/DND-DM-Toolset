using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class PlayerInfoList{

  private ArrayList pilist;

  public PlayerInfoList(){
    this.pilist = new ArrayList();
  }
  public ArrayList getList(){
    return this.pilist;
  }
  public void clearList(){
    this.pilist.Clear();
  }
  public void addPlayer(PlayerInfo pi){
    pilist.Add(pi);
  }

  public void delPlayer(PlayerInfo pi){
    pilist.Remove(pi);
  }

  public void delPlayerByName(string playername){
    for(int x = 0; x < pilist.Count; x++){
      if(pilist[x].getPlayerName() == playername){
        pilist.removeAt(x);
        break;
      }
    }
  }
}