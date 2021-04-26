using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventHandler_PlayerList : MonoBehaviour,
    IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
    public GameObject initiativeQueue;
    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData) {
        // Debug.Log(this.name + " Game Object Clicked, ID: " + gameObject.GetInstanceID().ToString());

        gameObject.SendMessageUpwards("KillCombatant", gameObject);
        //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
        
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
