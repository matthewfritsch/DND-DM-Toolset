using System.Collections;
using System.Collections.Generic;
public class PlayerInfoList{

  private List<PlayerInfo> pilist;
//default constructor for a PlayerInfoList (creates an empty list)
  public PlayerInfoList(){
    this.pilist = new List<PlayerInfo>();
  }
//returns the entire list
  public List<PlayerInfo> getList(){
    return this.pilist;
  }
//removes all items from the list.
  public void clearList(){
    this.pilist.Clear();
  }
//adds a new player to the list of playerinfos.
  public void addPlayer(PlayerInfo pi){
    pilist.Add(pi);
  }
//deletes a player by object from the list
  public bool delPlayer(PlayerInfo pi){
    return pilist.Remove(pi);
  }
//deletes a player by name from the list
  public void delPlayerByName(string playername){
    for(int x = 0; x < pilist.Count; x++){
      if(pilist[x].getPlayerName() == playername){
        pilist.RemoveAt(x);
        break;
      }
    }
  }

  public PlayerInfo getPlayerInfo(string playerName) {
    foreach (PlayerInfo p in pilist) {
      if (p.getPlayerName() == playerName) {
        return p;
      }
    }

    return null;
  }

  public bool containsPlayer(PlayerInfo player) {
    return pilist.Contains(player);
  }
}