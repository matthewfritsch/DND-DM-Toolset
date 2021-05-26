using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitiativeTracker : MonoBehaviour {
// public class InitiativeTracker : Singleton<InitiativeTracker> {

	// FIELDS
	// Unity Objects
	public GameObject entryPrefab;
	// Can potentially be used to move them into another visual location
	[Tooltip("Optional Transform to hold characters that have completed turn")]
	public Transform endTurnHoldingPen;


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

		// Load any combatants that have been prepared by save
		StartNewCombat(CombatInitiativeQueue.Instance.GetCombatants());
	}

	// void GenerateRandomPlayers() {
	// 	// TODO: Remove
	// 	List<string> tmpList = new List<string> {"Gunslinger", "Paladin", "Ranger", "Wizard", "Priest"};
	// 	for (int i = 0; i < Random.Range(1,4); i++) {
	// 		string pn 	= string.Format("Player {0}", i);
	// 		string cn 	= string.Format("Chumpo {0}", i);
	// 		string cl 	= tmpList[Random.Range(0, tmpList.Count-1)];
	// 		// string cl 	= "Gunslinger";
	// 		int ac 		= 3*i;
	// 		int hp 		= 20 + 3*i;

	// 		PlayerInfo newPlayer = new PlayerInfo(pn, cn, cl, ac, hp);
	// 		short initiative = (short) Random.Range(1,100);
	// 		short status    = (short) Random.Range(0, 16384*2 - 1);
	// 		newPlayer.setInitiative(initiative);
	// 		newPlayer.setStatusCondition((StatusCondition) status);

	// 		AddCombatant(newPlayer);
	// 		// CombatInitiativeQueue.Instance.AddToCombat(newPlayer);
	// 		// pList.addPlayer(newplayer);
	// 	}
	// }

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

	public void KillCombatant(GameObject combatant) {
		// TODO: Change to common parent
		// Debug.Log("Killing combatant ID: " + combatant.GetInstanceID().ToString());
		BeingInfo combatantInfo = combatant.GetComponent<Control_InitiativePanel>().GetCombatant();
		linkedListStandIn.Remove(combatant);
		Destroy(combatant);
		CombatInitiativeQueue.Instance.RemoveCombatant(combatantInfo);
	}

	// TODO: Replace return with common parent type
	/// <summary>
	/// 	Adds to global combat queue, creates InitiativePanel and displays it in the initiative queue
	/// </summary>
	public void AddCombatant(BeingInfo combatant) {
		bool addedToCombat = CombatInitiativeQueue.Instance.AddToCombat(combatant);

		// Combatant was not added to combat, no InitiativePanel needed
		if (!addedToCombat) {
			return;
		}

		GameObject newEntry = Instantiate(entryPrefab, gameObject.transform);
		newEntry.GetComponent<Control_InitiativePanel>().SetInitiativePanel(combatant);
		linkedListStandIn.Add(newEntry);

		UpdateQueue();
	}

	public void StartNewRound() {
		// Clean board of any remaining players
		while (gameObject.transform.childCount > 0) {
			CompleteCombatantTurn();
		}
		// Debug.Log ("You have killed them all!");

		DisplayQueue();
	}

	public void StartNewCombat(List<BeingInfo> combatants) {
		CleanQueue();
		foreach (BeingInfo c in combatants) {
			GameObject newEntry = Instantiate(entryPrefab, gameObject.transform);
			newEntry.GetComponent<Control_InitiativePanel>().SetInitiativePanel(c);
			linkedListStandIn.Add(newEntry);
		}

		UpdateQueue();
	}


	// // TODO: Remove and replace with enemy selection
	// public void AddRandomEnemy() {

	// 	// Create Enemy and add to storage list
	// 	string mn 	= string.Format("The Dungeonmaster");
	// 	string mt 	= string.Format("Enemy dude");
	// 	string ma 	= "N";
	// 	Size   ms	= TINY;
	// 	double cr 	= ;
	// 	int ac 		= Random.Range(1, 43);
	// 	int hp 		= Random.Range(20, 38);

	// 	MonsterInfo newEnemy = new MonsterInfo(pn, cn, cl, ac, hp);
	// 	short initiative = (short) Random.Range(1, 71);
    //     short status    = (short) Random.Range(0, 16384*2 - 1);
	// 	newEnemy.setInitiative(initiative);
	// 	newEnemy.setStatusCondition((StatusCondition) status);

	// 	AddCombatant(newEnemy, true);
	// }
}
