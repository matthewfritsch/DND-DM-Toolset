using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class BeingInfoList{
  [UnityEngine.SerializeField]
  private List<BeingInfo> bilist;
//default constructor for a BeingInfoList (creates an empty list)
  public BeingInfoList(){
    this.bilist = new List<BeingInfo>();
  }
//returns the entire list
  public List<BeingInfo> getList(){
    return this.bilist;
  }
//removes all items from the list.
  public void clearList(){
    this.bilist.Clear();
  }
//adds a new being to the list of BeingInfos.
  public void addBeing(BeingInfo pi){
    bilist.Add(pi);
  }
//deletes a being by object from the list
  public void delBeing(BeingInfo pi){
    bilist.Remove(pi);
  }
//deletes a player by name from the list
  public void delPlayerByName(string playername){
    for(int x = 0; x < bilist.Count; x++){
        if(bilist[x].GetType() != typeof(PlayerInfo)) continue;
        PlayerInfo pi = (PlayerInfo) bilist[x];
        if(pi.getPlayerName() == playername){
            bilist.RemoveAt(x);
            break;
        }
    }
  }
//deletes an object by index
  public void delPlayerByName(int idx){
    bilist.RemoveAt(idx);
  }

}