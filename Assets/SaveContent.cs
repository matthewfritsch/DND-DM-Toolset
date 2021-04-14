using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveContent : MonoBehaviour
{
    public GameObject playerNameInput;
    // Start is called before the first frame update
    public void SaveInput()
    {
        Debug.Log(playerNameInput.GetComponent<TMP_InputField>().text);
    }
}
