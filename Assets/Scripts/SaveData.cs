using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///     Main container class that will store all the information that needs to be kept between sessions.
///     Anything that needs to save must put their info here, and be able to collect it from here.
/// </summary>
[System.Serializable]
public class SaveData {
    public string gameName;
    public int s_testingValue;
    public List<BeingInfo> s_characters;
    // TODO: Smarter handling; names of players only, key enemy info only
    public List<BeingInfo> s_combatants;
    // [System.Serializable]
    // public struct CombatantInfo {
    //     public string c_name;
    //     public short c_health;
    //     public short c_init;
    //     public StatusCondition c_cond;
    // }

    public string ToJson() {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string savedGame) {
        JsonUtility.FromJsonOverwrite(savedGame, this);
    }
}