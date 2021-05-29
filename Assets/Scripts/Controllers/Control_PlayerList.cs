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
        // Necessary as pointer is lost on scene change
        // initiativeQueue = GameObject.FindWithTag("InitiativeList").gameObject;
        // Assert.IsNotNull(initiativeQueue);

        var playerName = transform.GetComponent<TextMeshProUGUI>().text;
        var player = GlobalPlayers.Instance.GetPlayerInfo(playerName);
        // Debug.Log($"Player Count: {GlobalPlayers.Instance.list.getList().Count}");
        Debug.Log("Found character " + player.getCharacterName());
        // Debug.Log(this.name + " Game Object Clicked, ID: " + gameObject.GetInstanceID().ToString());
        Assert.IsNotNull(player);
        // Assert.IsNull(player);
        GameObject tst1 = GameObject.FindWithTag("InitiativeList");
        var tst2 = tst1.GetComponent<InitiativeTracker>();
        tst2.AddCombatant(player);
        // initiativeQueue.GetComponent<InitiativeTracker>().AddCombatant(player);
        
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
