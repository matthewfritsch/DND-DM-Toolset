using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Control_SaveList : MonoBehaviour {
    private string[] saveArray;
    [Tooltip("GameObject that will contain the list of save games")]
    public GameObject container_saveGame;
    public GameObject saveGame_listEntry;
    // Start is called before the first frame update
    void Start() {
        UpdateList();
    }

    public void DeleteSaveGame() {
        Toggle selectedSaveFile = GetSelectedToggle();
        if (selectedSaveFile == null) {
            return;
        }

        File.Delete(Utils.GetSaveFilePath(selectedSaveFile.GetComponentInChildren<Text>().text));
        UpdateList();

    }

    /// <summary>
    ///     Will load the selected file from the list
    /// </summary>
    public void LoadSaveGame() {
        Toggle selectedSaveFile = GetSelectedToggle();
        if (selectedSaveFile == null) {
            return;
        }
        var saveFile = selectedSaveFile.GetComponentInChildren<Text>().text;

        string dataString;
        FileManager.LoadFromFile(saveFile, out dataString);
        Debug.Log($"Loading {dataString}");
        SaveData sd = new SaveData();

        sd.LoadFromJson(dataString);
        GlobalSaveManager.Instance.LoadAllData(sd);
        Debug.Log($"Loading {sd.gameName} with {sd.s_testingValue} characters");

        PlayerPrefs.SetString(Utils.S_PREF_KEY_SAVE, saveFile);
        SceneManager.LoadScene("Menu");
    }

    private void UpdateList() {
        foreach (Transform save in container_saveGame.transform) {
            Destroy(save.gameObject);
        }

        // Get all files that end with the save file extension
        saveArray = Directory.GetFiles(Application.persistentDataPath, "*" + Utils.S_SAVE_FILE_EXTENSION);
        for (int i = 0; i < saveArray.Length; ++i) {
            Debug.Log(saveArray[i]);
            GameObject newSaveName = Instantiate(saveGame_listEntry, container_saveGame.transform);
            newSaveName.GetComponent<Toggle>().group = container_saveGame.GetComponent<ToggleGroup>();
            newSaveName.GetComponentInChildren<Text>().text = Utils.GetSaveFileName(saveArray[i]);
        }
        
    }
    private Toggle GetSelectedToggle() {
        Toggle[] toggles = GetComponentsInChildren<Toggle>();

        foreach (Toggle t in toggles) {
            if (t.isOn) return t;
        }

        return null;
    }
}
