using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Control_Button_SaveGame : MonoBehaviour {
    public void SaveCurrentGame() {
        // Assume that there is a game stored in the playerprefs
        string saveName = PlayerPrefs.GetString(Utils.S_PREF_KEY_SAVE);

        SaveData sd = new SaveData();
        sd.gameName = saveName;
        // Just some numbers to see different stuff
        sd.s_testingValue = GlobalPlayers.Instance.playerNames.Count;

        // Save all data to the SaveData object
        GlobalSaveManager.Instance.SaveAllData(sd);
        // Write all the data to file
        Debug.Log(sd.ToJson());
        FileManager.WriteToFile(saveName, sd.ToJson());
    }
}
