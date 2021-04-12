using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativeListControl : MonoBehaviour {
    public GameObject fast_player;
    public GameObject slow_player;
    public GameObject InitiativeList;
    // GameObject InitiativeList = GameObject.FindWithTag("InitiativeList");

    public void add_quick() {
        Instantiate(fast_player, InitiativeList.transform);
        Debug.Log("Added player " + fast_player.name);
    }

    public void add_slow() {
        Instantiate(slow_player, InitiativeList.transform);
        Debug.Log("Added player " + slow_player.name);
    }

    public void complete_turn() {
        if (InitiativeList.transform.childCount > 0) {
            // Child at the top of the list
            Transform child_to_kill = InitiativeList.transform.GetChild(0);

            Debug.Log("Removing player " + child_to_kill.name);
            GameObject.Destroy(child_to_kill.gameObject);
        }
        
    }
}
