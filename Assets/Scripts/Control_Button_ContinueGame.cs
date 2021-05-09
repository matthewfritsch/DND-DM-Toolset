using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Button_ContinueGame : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        // TODO: Check PlayerPrefs for a recently active game session
        // TODO: Check if the named session is actually available in the local save directory
        // TODO: Enable button
    }

    // Update is called once per frame
    void Update() {
        
    }

    /// <summary>
    ///     Will load the save file of the name stored in PlayerPrefs.
    ///     Assume that the button will only be capable of calling this if there is a vaild save to be loaded
    /// </summary>
    public void ContinueMostRecentGame() {
        // TODO: Load in the last played save game
        // TODO: Change the scene to the main game scene
    }
}
