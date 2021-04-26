using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitiativeTracker : MonoBehaviour {

	// FIELDS
	// Unity Objects
	public GameObject entryPrefab;
	// Can potentially be used to move them into another visual location
	[Tooltip("Optional Transform to hold characters that have completed turn")]
	public Transform endTurnHoldingPen;

	[Tooltip("A ScriptableObject that holds the class name, class image dictionary")]
	public CharClassImage classDict;

	// Other fields
	List<GameObject> linkedListStandIn;
	List<GameObject> toBeDeleted;
	// PlayerInfoList base_list;
	// public Dictionary<string, Sprite> class_to_image = new Dictionary<string, Sprite>();

	[Tooltip("This is the name of the initiative GameObject with the Text component to modify.")]
	public string initiativeTextObjectName;
	[Tooltip("This is the name of the character name GameObject with the Text component to modify.")]
	public string characterTextObjectName;
	public string characterPlayerName;
	public string characterClass;
	public string characterHealth;
	public string characterImage;
	public string characterArmor;

	private void Awake() {
		linkedListStandIn = new List<GameObject>();
		toBeDeleted = new List<GameObject>();
		// base_list = new PlayerInfoList();
		// foreach (var item in class_image) {
		// 	class_to_image[item.class_name] = item.class_image;
		// }
	}

	public void GenerateNewParty() {
		CombatInitiativeQueue.Instance.EndCombat();
		// base_list.clearList();
		// Clean visual queue
		CleanQueue();
		StartNewRound();
		
		GenerateRandomPlayers();
		LoadPlayersIntoQueue();
	}

	void GenerateRandomPlayers() {
		// TODO: Remove
		List<string> tmpList = new List<string> {"Gunslinger", "Paladin", "Ranger", "Wizard", "Priest"};
		for (int i = 0; i < Random.Range(1,4); i++) {
			string pn 	= string.Format("Player {0}", i);
			string cn 	= string.Format("Chumpo {0}", i);
			string cl 	= tmpList[Random.Range(0, tmpList.Count-1)];
			// string cl 	= "Gunslinger";
			int ac 		= 3*i;
			int hp 		= 20 + 3*i;

			PlayerInfo newPlayer = new PlayerInfo(pn, cn, cl, ac, hp);
			int initiative = Random.Range(1,100);
			int status    = Random.Range(0, 16384*2 - 1);
			newPlayer.setInitiative(initiative);
			newPlayer.setStatus(status);

			CombatInitiativeQueue.Instance.AddPlayerToCombat(newPlayer);
			// pList.addPlayer(newplayer);
		}
	}

	void LoadPlayersIntoQueue() {
		// foreach (PlayerInfo p in CombatInitiativeQueue.Instance.GetCombatants()){
		foreach (PlayerInfo p in CombatInitiativeQueue.Instance.GetPlayersInCombat()) {
			GameObject newEntry = Instantiate(entryPrefab, gameObject.transform);
			// var tmpImage = DefaultCharacterImage;
			// update the text components for name and initiative
			newEntry.transform.Find(characterPlayerName).GetComponent<Text>().text = p.getPlayerName();
			newEntry.transform.Find(characterTextObjectName).GetComponent<Text>().text = p.getCharacterName();
			newEntry.transform.Find(characterClass).GetComponent<Text>().text = p.getCharacterClass();
			newEntry.transform.Find(initiativeTextObjectName).GetComponent<Text>().text = p.getInitiative().ToString();
			newEntry.transform.Find(characterArmor).GetComponent<Text>().text = p.getArmorClass().ToString();
			Transform newHealth = newEntry.transform.Find(characterHealth);
			newHealth.GetComponentInChildren<Image>().fillAmount = (p.getCurrentHP()/p.getMaxHP());
			newHealth.GetComponentInChildren<Text>().text = string.Format("{0}/{1}", p.getCurrentHP(), p.getMaxHP());
			newEntry.transform.Find(characterImage).GetComponent<Image>().sprite = classDict.GetClassImage(p.getCharacterClass());

			linkedListStandIn.Add(newEntry);
		}

		UpdateQueue();
		DisplayQueue();

	}

	// Call when there is a change in the queue order; new entry, change in init
	// Preferably after all changes are complete
	void UpdateQueue() {
		linkedListStandIn.Sort(InitiativeComparitor);
		// Sort is from smallest to largest, we want bigger first		
		linkedListStandIn.Reverse();
		for (int i = 0; i < linkedListStandIn.Count; i++) {
			// Sets the index position of the graphic list to be the same as the storage list
			linkedListStandIn[i].transform.SetSiblingIndex(i);
		}
	}

	// TODO: If new entries can show up mid round, then that needs to be handled
	void DisplayQueue() {
		foreach (GameObject item in linkedListStandIn) {
			// Add the items to the visual queue
			item.transform.SetParent(gameObject.transform);
			// Ensure they are visible
			item.SetActive(true);
		}
	}

	void CleanQueue() {
		foreach (GameObject item in linkedListStandIn) {
			Destroy(item);
		}
		linkedListStandIn.Clear();
	}

	int InitiativeComparitor(GameObject left, GameObject right) {
		int leftInit = System.Convert.ToInt32(left.transform.Find(initiativeTextObjectName).GetComponent<Text>().text);
		int rightInit = System.Convert.ToInt32(right.transform.Find(initiativeTextObjectName).GetComponent<Text>().text);

		return leftInit.CompareTo(rightInit);
	}

	public void CompleteCombatantTurn() {
		if (gameObject.transform.childCount > 0) {
            // Child at the top of the list
            Transform child_to_kill = gameObject.transform.GetChild(0);

			// Remove top player from visual queue, without removing from storage queue
			child_to_kill.SetParent(endTurnHoldingPen);
			child_to_kill.gameObject.SetActive(false);
			// linkedListStandIn.removeAt(0);

            // Debug.Log("Removing player " + child_to_kill.name);
            // GameObject.Destroy(child_to_kill.gameObject);
		}
	}

	public void StartNewRound() {
		// Clean board of any remaining players
		while (gameObject.transform.childCount > 0) {
			CompleteCombatantTurn();
		}
		// Debug.Log ("You have killed them all!");

		DisplayQueue();
	}

	public void AddRandomEnemy() {
		// generate random initiative
		int initiative = Random.Range(1, 71);
		GameObject newEntry = Instantiate(entryPrefab, gameObject.transform);
		// var tmpImage = DefaultCharacterImage;

		// update the text components for name and initiative
		newEntry.transform.Find(characterPlayerName).GetComponent<Text>().text = "Enemy!";
		newEntry.transform.Find(characterTextObjectName).GetComponent<Text>().text = "OMG ENEMY";
		newEntry.transform.Find(initiativeTextObjectName).GetComponent<Text>().text = initiative.ToString();
		// TODO: set fill amount to ratio of current over max health
		Transform newHealth = newEntry.transform.Find(characterHealth);
		newHealth.GetComponentInChildren<Image>().fillAmount = 1;// p.getHealthPoints();
		newHealth.GetComponentInChildren<Text>().text = string.Format("{0}/{1}", 10, 10);
		// TODO: set image to one related to char class, likely from some image dict
		// class_to_image.TryGetValue("enemy", out tmpImage);
		newEntry.transform.Find(characterImage).GetComponent<Image>().sprite = classDict.GetClassImage("enemy");

		// add this new entry into the list
		linkedListStandIn.Add(newEntry);

		// sort the list
		UpdateQueue();

		// DisplayQueue();
	}
}
