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

	// UNITY METHODS
	void Awake()
	{
		GenerateNewData();
	}

	void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			// delete the old gameobjects
			for (int i = 0; i < transform.childCount; i++)
			{
				Destroy(transform.GetChild(i).gameObject);
			}
			GenerateNewData();
		}
	}

	// METHODS
	void GenerateNewData()
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
}
