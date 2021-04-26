using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchableMonsterList : MonoBehaviour
{
	// Unity Objects
	[SerializeField]
	private GameObject scrollView;
	[SerializeField]
	private GameObject scrollViewContent;	// target location to add new entries
	[SerializeField]
	private GameObject entryPrefab;

	// Fields
	private PlayerInfoList data; // Currently uses dummy data
	private Dictionary<Toggle, PlayerInfo> uiPlayerInfoMap; // maps UI elements to PlayerInfo Objects

	// Unity Methods
	void Start()
	{
		// initialize fields
		uiPlayerInfoMap = new Dictionary<Toggle, PlayerInfo>();

		// initialize the dummy data and create GameObjects for it
		InitDummyData();
		CreateGameObjectsUsingData();
	}
	void Update()
	{
	}

	// Methods

	// toggles the Scroll View between active and inactive
	public void ToggleScrollView()
	{
		scrollView.SetActive(!scrollView.activeInHierarchy);
	}

	// converts data into GameObjects using the entryPrefab to create it.
	//		this can be filtered by character name
	public void CreateGameObjectsUsingData(string filter = "")
	{
		// clear the current dictionary
		uiPlayerInfoMap.Clear();

		// clean up the scroll view content
		ClearScrollViewContent();

		// iterate through the data
		foreach (PlayerInfo playerInfo in data.getList())
		{
			// apply filter (case insensitive)
			if (playerInfo.getCharacterName().ToLower().Contains(filter.ToLower()))
			{
				// create a newEntry in the scrollViewContent GameObject
				GameObject newEntry = Instantiate(entryPrefab, scrollViewContent.transform);

				// update the GameObject's text data
				SearchableListEntryData currentEntryData = newEntry.GetComponent<SearchableListEntryData>();
				currentEntryData.MonsterName.text = playerInfo.getCharacterName();
				currentEntryData.MonsterHealth.text =
					string.Format("HP: {0} / {1}", playerInfo.getCurrentHP(), playerInfo.getMaxHP());
				currentEntryData.MonsterArmorClass.text = string.Format("Armor Class: {0}", playerInfo.getArmorClass());

				// establish GameObject to PlayerInfo mapping
				uiPlayerInfoMap.Add(newEntry.GetComponent<Toggle>(), playerInfo);
			}
		}
	}

	// deletes everything from the scroll view content
	public void ClearScrollViewContent(bool ignoreSelected = false)
	{
		for (int idx = 0; idx < scrollViewContent.transform.childCount; idx++)
		{
			GameObject child = scrollViewContent.transform.GetChild(idx).gameObject;
			Destroy(child);
		}
	}

	// reads from the Content GameObject and returns a PlayerInfoList of all selected PlayerInfos
	public PlayerInfoList GetSelectedMonsters()
	{
		// create a new empty list
		PlayerInfoList selectedMonsters = new PlayerInfoList();

		// loop through the mapping dictionary
		foreach (KeyValuePair<Toggle, PlayerInfo> pair in uiPlayerInfoMap)
		{
			// if a Toggle is switched to On, add that to the list to return
			if (pair.Key.isOn)
			{
				selectedMonsters.addPlayer(pair.Value);
			}
		}

		// return the selected monsters
		return selectedMonsters;
	}

	// runs GetSelectedMonsters and prints the value that it returns
	public void PrintSelectedMonsters()
	{
		// get the selected monsters
		PlayerInfoList selectedMonsters = GetSelectedMonsters();

		// create an output string
		string output = "Selected Monsters: ...\n";

		foreach (PlayerInfo playerInfo in selectedMonsters.getList())
		{
			output += string.Format("{0}: HP({1}/{2}), ArmorClass({3})\n",
				playerInfo.getCharacterName(),
				playerInfo.getCurrentHP(),
				playerInfo.getMaxHP(),
				playerInfo.getArmorClass());
		}

		// print the output
		Debug.Log(output);
	}

	// Helper Methods

	// initializes the dummy data
	private void InitDummyData()
	{
		// Construct new dummyData
		data = new PlayerInfoList();

		// Add new data
		data.addPlayer(new PlayerInfo("MONSTER", "Tanky Being", "MONSTER", 20, 100));
		data.addPlayer(new PlayerInfo("MONSTER", "Tanky Being 2", "MONSTER", 40, 60));
		data.addPlayer(new PlayerInfo("MONSTER", "Weak Being", "MONSTER", 5, 20));
		data.addPlayer(new PlayerInfo("MONSTER", "Weak Being 2", "MONSTER", 10, 10));
		data.addPlayer(new PlayerInfo("MONSTER", "Boss Monster", "MONSTER", 45, 200));
		data.addPlayer(new PlayerInfo("MONSTER", "Boss Monster 2", "MONSTER", 65, 400));
		data.addPlayer(new PlayerInfo("MONSTER", "Boss Monster 3", "MONSTER", 80, 800));
	}
}
