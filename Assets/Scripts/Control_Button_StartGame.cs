using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
///     Control script for the button that starts a new game
/// </summary>
public class Control_Button_StartGame : MonoBehaviour {
    [Tooltip("Input field that stores the name of the new game to be made")]
    public InputField gameNameInput;

    public void StartNewGame() {
        string gameName = gameNameInput.GetComponent<InputField>().text;

        Debug.Log(gameName);

        // Do not allow the creation of a new game without a save name
        if  (gameName == null || gameName == "") {
            return;
        } else if (File.Exists(Utils.GetSaveFilePath(gameName))) {
            // ? Do we allow the creation with an automatically modified name,
            // ? or have the player change it to something new
            Debug.Log(gameName + " already exists, please choose different name");
        } else {
            // Set PlayerPrefs most recent game to this one
            // TODO: make new empty save for player
            SaveData sd = new SaveData();
            sd.gameName = gameName;
            sd.s_testingValue = 0;
            FileManager.WriteToFile(gameName, sd.ToJson());

            PlayerPrefs.SetString(Utils.S_PREF_KEY_SAVE, gameName);
            SceneManager.LoadScene("Menu");
        }
    }
}
