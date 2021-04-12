using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// A definitely not psychotic way of phrasing: remove stuff from the parent object
// Used mainly to clear the initiative list

public class kill_children : MonoBehaviour {

	public GameObject murderous_parent;
	public void kill_all() {
		foreach (Transform child in murderous_parent.transform) {
			Debug.Log("Killing child " + child.name);
			GameObject.Destroy(child.gameObject);
		}
		Debug.Log ("You have killed them all!");
	}
}
