using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Control_PlayerList : MonoBehaviour,
    IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

    private GameObject initiativeQueue;
    public void Start() {
        initiativeQueue = GameObject.FindWithTag("InitiativeList").gameObject;
        // Debug.Log("Found " + initiativeQueue);
    }
    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData) {
        var playerName = transform.GetComponent<TextMeshProUGUI>().text;
        var player = GlobalPlayers.Instance.list.getPlayerInfo(playerName);
        Debug.Log("Found character " + player.getCharacterName());
        // Debug.Log(this.name + " Game Object Clicked, ID: " + gameObject.GetInstanceID().ToString());
        Assert.IsNotNull(player);
        initiativeQueue.GetComponent<InitiativeTracker>().AddCombatant(player);
        
    }
    
    // Detect if mouse hovers over InitativeTab
    public void OnPointerEnter(PointerEventData pointerEventData) {
        // TODO: Fancy Visual stuff when mouse is over a tab
        // gameObject.GetComponent<Image>().color = Color.gray;

    }

    public void OnPointerExit(PointerEventData pointerEventData) {
        // gameObject.GetComponent<Image>().color = defaultColor;
    }
}
