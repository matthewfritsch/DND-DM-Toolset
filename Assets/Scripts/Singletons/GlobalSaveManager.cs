using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///     Global manager for all items that need to be saved.
///     If something wants to be saved, either explicitly add a reference to the list, or 
///     it must add itself to the list on Awake() or similar.
///     ! Careful of duplicate entires, and of order of saving/loading if there are dependencies
/// </summary>
public class GlobalSaveManager : Singleton<GlobalSaveManager> {
    protected GlobalSaveManager() {}

    private List<ISaveable> toBeSaved = new List<ISaveable>();
    void Awake() {
        toBeSaved.Add(GlobalPlayers.Instance);
        toBeSaved.Add(CombatInitiativeQueue.Instance);
    }

    public void RegisterToBeSaved(ISaveable reference) {
        toBeSaved.Add(reference);
    }

    public void SaveAllData(SaveData sd) {
        foreach (var item in toBeSaved) {
            item.PopulateSaveData(sd);
        }
    }

    public void LoadAllData(SaveData sd) {
        foreach (var item in toBeSaved) {
            item.LoadFromSaveData(sd);
        }
    }

}