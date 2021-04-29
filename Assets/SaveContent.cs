using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveContent : MonoBehaviour
{
    public GameObject playerNameInput;
    public GameObject characterNameInput;
    public GameObject characterClassInput;
    public GameObject armorClassInput;
    public GameObject maxHPInput;

    public PlayerInfo newPlayerInfo;
    public PlayerInfoList globalPlayerList = new PlayerInfoList();
    public updatePlayerList updatePlayerList;
    // Start is called before the first frame update
    public void SaveInput()
    {
        string playerName = playerNameInput.GetComponent<TMP_InputField>().text;
        string characterName = characterNameInput.GetComponent<TMP_InputField>().text;
        string characterClass = characterClassInput.GetComponent<TMP_InputField>().text;
        string armorClass = armorClassInput.GetComponent<TMP_InputField>().text;
        string maxHP = maxHPInput.GetComponent<TMP_InputField>().text;

        if (playerName == "" || characterName == "" || characterClass == "" || armorClass == "" || maxHP == "")
        {
            return;
        }

        int intArmorClass;
        int intMaxHP;
        if (!int.TryParse(armorClass, out intArmorClass))
        {
            Debug.Log("invalid Armor Class entry (not integer)");
            return;
        }
        if (!int.TryParse(maxHP, out intMaxHP))
        {
            Debug.Log("invalid Max HP entry (not integer)");
            return;
        }

        newPlayerInfo = new PlayerInfo(playerName, characterName, characterClass, intArmorClass, intMaxHP);
        globalPlayerList.addPlayer(newPlayerInfo);

        updatePlayerList.UpdatePL(playerName);

        playerNameInput.GetComponent<TMP_InputField>().text = "";
        characterNameInput.GetComponent<TMP_InputField>().text = "";
        characterClassInput.GetComponent<TMP_InputField>().text = "";
        armorClassInput.GetComponent<TMP_InputField>().text = "";
        maxHPInput.GetComponent<TMP_InputField>().text = "";
    }

   
}
