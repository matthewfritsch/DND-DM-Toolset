using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventHandler_InitiativeTab : MonoBehaviour, IPointerClickHandler {

    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData) {
        // Debug.Log(this.name + " Game Object Clicked, ID: " + gameObject.GetInstanceID().ToString());
        gameObject.SendMessageUpwards("KillCombatant", gameObject);
        //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
        
    }
}
