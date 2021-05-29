using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
///     Control script that allows easy change of scene on function call.
///     New function for each scene, should be attached to Main Camera.
/// </summary>
public class Control_SceneManagement : MonoBehaviour {
    public void GoToGameStartScene() {
        SceneManager.LoadScene("Menu_Start");
    }

    public void GoToGamePlayScene() {
        SceneManager.LoadScene("Menu");
    }
}
