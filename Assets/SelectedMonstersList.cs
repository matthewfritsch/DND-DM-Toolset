using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedMonstersList : MonoBehaviour
{
	// Unity Objects
	[SerializeField]
	public GameObject scrollViewContent;
	[SerializeField]
	public GameObject entryPrefab;

	// Fields

	// field for mapping Buttons to PlayerInfos
	private Dictionary<Button, PlayerInfo> uiPlayerInfoMap;
	// field for mapping PlayerInfos to count information
	private Dictionary<PlayerInfo, int> playerInfoCountMap;

	// Unity Methods
	void Awake()
	{
		// Initialize Fields
		uiPlayerInfoMap = new Dictionary<Button, PlayerInfo>();
		playerInfoCountMap = new Dictionary<PlayerInfo, int>();
	}

	// Methods

	// Creates GameObjects based off the saved data (similar to a similarly-named function in SearchableMonsterList)
	public void CreateGameObjectsUsingData()
	{
		// clear the current dictionary
		uiPlayerInfoMap.Clear();

		// clean up the scroll view content
		ClearScrollViewContent();

		// iterate through the data in the map
		foreach (KeyValuePair<PlayerInfo, int> playerInfoCountPair in playerInfoCountMap)
		{
			PlayerInfo playerInfo = playerInfoCountPair.Key;
			int count = playerInfoCountPair.Value;
			// create a newEntry in the scrollViewContent GameObject
			GameObject newEntry = Instantiate(entryPrefab, scrollViewContent.transform);

			// update the GameObject's text data and other info
			SearchableMonsterListEntryData currentEntryData = newEntry.GetComponent<SearchableMonsterListEntryData>();
			currentEntryData.MonsterName.text = playerInfo.getCharacterName();
			currentEntryData.MonsterHealth.text =
				string.Format("HP: {0} / {1}", playerInfo.getCurrentHP(), playerInfo.getMaxHP());
			currentEntryData.MonsterArmorClass.text = string.Format("Armor Class: {0}", playerInfo.getArmorClass());
			currentEntryData.MonsterCount.gameObject.SetActive(true);
			currentEntryData.MonsterCount.text = count.ToString();

			// TODO: add any function listeners as needed to the UI element
			currentEntryData.MonsterCount.onEndEdit.AddListener(delegate {
				UpdateMonsterCount(playerInfo, int.Parse(currentEntryData.MonsterCount.text));
			});

			// establish GameObject to PlayerInfo mapping
			uiPlayerInfoMap.Add(newEntry.GetComponent<Button>(), playerInfo);
		}
	}

	// Called by SearchableMonsterList, receives the selected monster and saves its information
	public void SelectMonster(PlayerInfo selectedMonster)
	{
		// if the map contains this monster, add 1 to its count value
		if (playerInfoCountMap.ContainsKey(selectedMonster))
		{
			playerInfoCountMap[selectedMonster]++;
		}
		// if the map doesn't contain this monster, add it to the map with a default value of 1
		else
		{
			playerInfoCountMap.Add(selectedMonster, 1);
		}

		// Update the UI
		CreateGameObjectsUsingData();
	}

	// Clears the scroll view content
	public void ClearScrollViewContent()
	{
		for (int idx = 0; idx < scrollViewContent.transform.childCount; idx++)
		{
			GameObject child = scrollViewContent.transform.GetChild(idx).gameObject;
			Destroy(child);
		}
	}

	// Updates the count for the monster
	public void UpdateMonsterCount(PlayerInfo monster, int newCount)
	{
		// if newCount <= 0, treat this as if it was deleted
		if (newCount <= 0)
		{
			Debug.Log("Monster was deleted");
			playerInfoCountMap.Remove(monster);
		}
		else
		{
			playerInfoCountMap[monster] = newCount;
		}

		// Update the UI
		CreateGameObjectsUsingData();
	}
}
