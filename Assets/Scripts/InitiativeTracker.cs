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

	// Replace this with a self sorting list or something
	// Contains the built prefabs that will go into visual queue
	List<GameObject> linkedListStandIn = new List<GameObject>();
	
	[Tooltip("This is the name of the initiative GameObject with the Text component to modify.")]
	public string initiativeTextObjectName;
	[Tooltip("This is the name of the character name GameObject with the Text component to modify.")]
	public string characterTextObjectName;
	public string characterPlayerName;
	public string characterHealth;
	public string characterImage;

	PlayerInfoList base_list;


	// UNITY METHODS
	void Awake()
	{
		// Warning that any children of this GameObject may be deleted
		if (transform.childCount > 0)
		{
			Debug.LogWarningFormat("Warning: Any children of gameobject \"{0}\" may be deleted, especially if their name contains" +
				" the string \"{1}\".", name, entryPrefab.name);
		}

		// Initialize any fields
		dummyData = new List<DummyStruct>();
		base_list = new PlayerInfoList();
		GenerateNewParty();
	}

	void Update()
	{
	}

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
			newEntry.transform.Find(initiativeTextObjectName).GetComponent<Text>().text = p.getInitiative().ToString();
			// TODO: set fill amount to ratio of current over max health
			newEntry.transform.Find(characterHealth).GetComponent<Image>().fillAmount = 1;// p.getHealthPoints();
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
		newEntry.transform.Find(characterHealth).GetComponent<Image>().fillAmount = 1;// p.getHealthPoints();
		// TODO: set image to one related to char class, likely from some image dict
		// newEntry.transform.Find(characterImage).GetComponent<Image>().sourceImage = ;

		// add this new entry into the list
		linkedListStandIn.Add(newEntry);

		// sort the list
		UpdateQueue();

		// DisplayQueue();
	}

	// METHODS
	void GenerateRandomData()
	{
		// Create the dummyData
		dummyData = new List<DummyStruct>();
		// Create the dummy data
		for (int i = 0; i < Random.Range(4, 8); i++)
		{
			// string name = (Random.Range(1, 3) % 2 == 0) ? "Player" : "Enemy";
			string name = "Enemy";
			name += string.Format(" {0}", i);
			dummyData.Add(new DummyStruct(name, Random.Range(1, 21)));
		}
		// Sort the dummy data
		dummyData.Sort((elem1, elem2) => elem2.initiative - elem1.initiative);
		// Make new entries and update data
		foreach (DummyStruct data in dummyData)
		{
			GameObject newEntry = Instantiate(entryPrefab, gameObject.transform);
			newEntry.GetComponent<Text>().text = string.Format("{0}\t\tI: {1}", data.name, data.initiative);
		}
	}

	public void AddEntryRandomInitiative(string name)
	{
		// generate random initiative
		int initiative = Random.Range(1, 21);

		// add this new entry into the list
		dummyData.Add(new DummyStruct(name, initiative));

		// sort the list
		dummyData.Sort((elem1, elem2) => elem2.initiative - elem1.initiative);

		// refresh the initiative UI
		RefreshInitiativeUI();
	}

	// When the current entry's turn is completed
	public void CompleteTurn()
	{
		if (dummyData.Count > 0)
		{
			// delete front of list
			dummyData.RemoveAt(0);

			// refresh UI
			RefreshInitiativeUI();
		}
	}

	// When a new round begins (clearing the initiative list)
	public void BeginNewRound()
	{
		if (dummyData.Count > 0)
		{
			// delete all data from list
			dummyData.Clear();

			// refresh UI
			RefreshInitiativeUI();
		}
	}

	// This function should generally be called every time the internal data is updated
	public void RefreshInitiativeUI()
	{
		// Delete the currently shown data (condition: its name contains the same name as entryPrefab)
		for (int i = 0; i < transform.childCount; i++)
		{
			if (transform.GetChild(i).name.Contains(entryPrefab.name))
			{
				Destroy(transform.GetChild(i).gameObject);
			}
		}

		// Refresh it with current data
		foreach (DummyStruct data in dummyData)
		{
			// instantiate new GameObject
			GameObject newEntry = Instantiate(entryPrefab, gameObject.transform);
			// update the text components for name and initiative
			newEntry.transform.Find(initiativeTextObjectName).GetComponent<Text>().text = data.initiative.ToString();
			newEntry.transform.Find(characterTextObjectName).GetComponent<Text>().text = data.name;
		}
	}
}
