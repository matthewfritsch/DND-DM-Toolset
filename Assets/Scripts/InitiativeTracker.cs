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
	}

	void Update()
	{
	}

	// METHODS
	void GenerateRandomData()
	{
		// Create the dummyData
		dummyData = new List<DummyStruct>();
		// Create the dummy data
		for (int i = 0; i < Random.Range(4, 8); i++)
		{
			string name = (Random.Range(1, 3) % 2 == 0) ? "Player" : "Enemy";
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
