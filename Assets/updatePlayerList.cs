using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class updatePlayerList : MonoBehaviour
{
    public GameObject addPlayerMenu;
    public GameObject playerTextObj;

    private void Start()
    {
        
        //playerText.text = "test";
    }

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI playerText = playerTextObj.GetComponent<TextMeshProUGUI>();
        SaveContent content = addPlayerMenu.GetComponent<SaveContent>();
        string names = "";
        foreach(PlayerInfo player in content.globalPlayerList.getList()) {
            names += player.getCharacterName() + "\n";
        }
        playerText.text = names;
    }
}
