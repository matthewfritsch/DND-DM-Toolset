using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class updatePlayerList : MonoBehaviour
{
    public GameObject addPlayerMenu;
    public GameObject playerTextPrefab;

    // public void Awake() {
    //     // while (transform.childCount > 0) {
    //     //     Destroy(transform.GetChild(0));
    //     // }
    //     // foreach (string name in GlobalPlayers.Instance.playerNames) {
    //     //     TextMeshProUGUI newName = Instantiate(playerTextPrefab, gameObject.transform).GetComponent<TextMeshProUGUI>();
    //     //     newName.text = name;                

    //     // }
    // }

    public void Update() {
        if (GlobalPlayers.Instance.playerNames.Count != transform.childCount) {
            foreach (Transform child in transform) {
                GameObject.Destroy(child.gameObject);
            }

            foreach (string name in GlobalPlayers.Instance.playerNames) {
                Debug.Log($"Creating player {name}");
                TextMeshProUGUI newName = Instantiate(playerTextPrefab, gameObject.transform).GetComponent<TextMeshProUGUI>();
                newName.text = name;                
            }

        }
    }

    // public void UpdatePL(string PlayerName)
    // {
    //     TextMeshProUGUI newName = Instantiate(playerTextPrefab, gameObject.transform).GetComponent<TextMeshProUGUI>();
    //     newName.text = PlayerName;
    // }

    // Update is called once per frame
    /*void Update()
    {
        TextMeshProUGUI playerText = playerTextObj.GetComponent<TextMeshProUGUI>();
        SaveContent content = addPlayerMenu.GetComponent<SaveContent>();
        string names = "";
        foreach(PlayerInfo player in content.globalPlayerList.getList()) {
            names += player.getCharacterName() + "\n";
        }
        playerText.text = names;
    }*/
}
