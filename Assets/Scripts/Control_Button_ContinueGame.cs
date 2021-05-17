using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Control_Button_ContinueGame : MonoBehaviour {
    // Start is called before the first frame update
    // ? Maybe run on update, bug when save is deleted on load, and return to main menu
    // void Start() {
    void Update() {
        if (PlayerPrefs.HasKey(Utils.S_PREF_KEY_SAVE)) {
            // Get the save file with extension
            var saveFilePath = Utils.GetSaveFilePath(PlayerPrefs.GetString(Utils.S_PREF_KEY_SAVE));
            // Check if there is a save file of the stored name in the save file directory
            if (File.Exists(saveFilePath)) {
                gameObject.GetComponent<Button>().interactable = true;
                return;

            // If there is no file by that name, then clean up PlayerPrefs to reflect that
            } else {
                PlayerPrefs.DeleteKey(Utils.S_PREF_KEY_SAVE);
            }
        }
        // Ensure that button is not active if there is no valid recent save
        gameObject.GetComponent<Button>().interactable = false;
    }

    /// <summary>
    ///     Will load the save file of the name stored in PlayerPrefs.
    ///     Assume that the button will only be capable of calling this if there is a vaild save to be loaded
    /// </summary>
    public void ContinueMostRecentGame() {
        string saveFile = PlayerPrefs.GetString(Utils.S_PREF_KEY_SAVE);
        SaveData sd = new SaveData();
        string dataString;

        try {
            // Read in data from file and store it in string
            FileManager.LoadFromFile(saveFile, out dataString);
            // Copy data from string into SaveData object
            sd.LoadFromJson(dataString);
            GlobalSaveManager.Instance.LoadAllData(sd);

            SceneManager.LoadScene("Menu");
        } catch (System.Exception) {
            throw;
        }
    }
}
