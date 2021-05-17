using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///     Global manager for all items that need to be saved.
///     If something wants to be saved, it must add itself to the list on Awake() or similar
/// </summary>
public class GlobalSaveManager : Singleton<GlobalSaveManager> {
    protected GlobalSaveManager() {}

    private HashSet<ISaveable> toBeSaved = new HashSet<ISaveable>();

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