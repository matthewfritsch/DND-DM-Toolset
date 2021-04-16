using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitiativeTracker : MonoBehaviour
{
	// Dummy struct for holding data (in a generic collection)
	public struct DummyStruct
	{
		public string name;
		public int initiative;

		public DummyStruct(string name, int initiative)
		{
			this.name = name;
			this.initiative = initiative;
		}
	}

	// FIELDS
	// Unity Objects
	public GameObject entryPrefab;
	// Other fields
	List<DummyStruct> dummyData; // temp stuff
	[Tooltip("This is the name of the initiative GameObject with the Text component to modify.")]
	public string initiativeTextObjectName;
	[Tooltip("This is the name of the character name GameObject with the Text component to modify.")]
	public string characterTextObjectName;
	public string characterPlayerName;
	public string characterClass;
	public string characterHealth;
	public string characterImage;
	public string characterArmor;

	PlayerInfoList base_list;

	public void GenerateNewParty() {
		base_list.clearList();
		// Clean visual queue
		linkedListStandIn.Clear();
		StartNewRound();
		
		GenerateRandomPlayers(base_list);
		LoadPlayersIntoQueue(base_list);
	}

	void GenerateRandomPlayers(PlayerInfoList list) {
		for (int i = 0; i < Random.Range(1,4); i++) {
			string pn 	= string.Format("Player {0}", i);
			string cn 	= string.Format("Chumpo {0}", i);
			string cl 	= "Gunslinger";
			int ac 		= 3*i;
			int hp 		= 20 + 3*i;

			PlayerInfo newplayer = new PlayerInfo(pn, cn, cl, ac, hp);
			int initiative = Random.Range(1,100);
			int status    = Random.Range(0, 16384*2 - 1);
			newplayer.setInitiative(initiative);
			newplayer.setStatus(status);

			list.addPlayer(newplayer);
		}
	}

	void LoadPlayersIntoQueue(PlayerInfoList list) {
		foreach (PlayerInfo p in list.getList()){
			GameObject newEntry = Instantiate(entryPrefab, gameObject.transform);
			// update the text components for name and initiative
			newEntry.transform.Find(characterPlayerName).GetComponent<Text>().text = p.getPlayerName();
			newEntry.transform.Find(characterTextObjectName).GetComponent<Text>().text = p.getCharacterName();
			newEntry.transform.Find(characterClass).GetComponent<Text>().text = p.getCharacterClass();
			newEntry.transform.Find(initiativeTextObjectName).GetComponent<Text>().text = p.getInitiative().ToString();
			newEntry.transform.Find(characterArmor).GetComponent<Text>().text = p.getArmorClass().ToString();
			// TODO: set fill amount to ratio of current over max health
			Transform newHealth = newEntry.transform.Find(characterHealth);
			newHealth.GetComponentInChildren<Image>().fillAmount = 1;// p.getHealthPoints();
			newHealth.GetComponentInChildren<Text>().text = string.Format("{0}/{1}", p.getHealthPoints(), p.getHealthPoints());
			// TODO: set image to one related to char class, likely from some image dict
			// newEntry.transform.Find(characterImage).GetComponent<Image>().sourceImage = ;

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

	int InitiativeComparitor(GameObject left, GameObject right) {
		int leftInit = System.Convert.ToInt32(left.transform.Find(initiativeTextObjectName).GetComponent<Text>().text);
		int rightInit = System.Convert.ToInt32(right.transform.Find(initiativeTextObjectName).GetComponent<Text>().text);

		return leftInit.CompareTo(rightInit);
	}

	public void CompletePlayerTurn() {
		if (gameObject.transform.childCount > 0) {
            // Child at the top of the list
            Transform child_to_kill = gameObject.transform.GetChild(0);

			// Remove top player from visual queue, without removing from storage queue
			child_to_kill.SetParent(null);
			child_to_kill.gameObject.SetActive(false);
			// linkedListStandIn.removeAt(0);

            Debug.Log("Removing player " + child_to_kill.name);
            // GameObject.Destroy(child_to_kill.gameObject);
		}
	}

	public void StartNewRound() {
		// Clean board of any remaining players
		while (gameObject.transform.childCount > 0) {
			CompletePlayerTurn();
		}
		Debug.Log ("You have killed them all!");

		DisplayQueue();
	}

	public void AddRandomEnemy() {
		// generate random initiative
		int initiative = Random.Range(1, 71);
		GameObject newEntry = Instantiate(entryPrefab, gameObject.transform);
		// update the text components for name and initiative
		newEntry.transform.Find(characterPlayerName).GetComponent<Text>().text = "Enemy!";
		newEntry.transform.Find(characterTextObjectName).GetComponent<Text>().text = "OMG ENEMY";
		newEntry.transform.Find(initiativeTextObjectName).GetComponent<Text>().text = initiative.ToString();
		// TODO: set fill amount to ratio of current over max health
		Transform newHealth = newEntry.transform.Find(characterHealth);
		newHealth.GetComponentInChildren<Image>().fillAmount = 1;// p.getHealthPoints();
		newHealth.GetComponentInChildren<Text>().text = string.Format("{0}/{1}", 10, 10);
		// TODO: set image to one related to char class, likely from some image dict
		// newEntry.transform.Find(characterImage).GetComponent<Image>().sourceImage = ;

		// add this new entry into the list
		linkedListStandIn.Add(newEntry);

		// sort the list
		UpdateQueue();

		// DisplayQueue();
	}

