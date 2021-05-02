using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedMonstersList : MonoBehaviour {
    // Unity Objects
    [SerializeField]
    private GameObject scrollViewContent;
    [SerializeField]
    private GameObject entryPrefab;
    [SerializeField]
    private GameObject controlsObject;
    [SerializeField]
    private Toggle deleteModeToggle;
    [SerializeField]
    private InitiativeTracker initiativeTracker;

    // Fields

    // field for mapping Buttons to PlayerInfos
    private Dictionary<Button, PlayerInfo> uiPlayerInfoMap;
    // field for mapping PlayerInfos to count information
    private Dictionary<PlayerInfo, int> playerInfoCountMap;

    // Unity Methods
    void Awake() {
        // Initialize Fields
        uiPlayerInfoMap = new Dictionary<Button, PlayerInfo>();
        playerInfoCountMap = new Dictionary<PlayerInfo, int>();
    }

    void Update() {
        // set the controls object inactive if there are no selected monsters
        if (uiPlayerInfoMap.Count <= 0 && controlsObject.activeInHierarchy) {
            // set the toggle to be off first
            deleteModeToggle.isOn = false;

            controlsObject.SetActive(false);
        }
        // set the controls object active if there are selected monsters
        else if (uiPlayerInfoMap.Count >= 1 && !controlsObject.activeInHierarchy) {
            controlsObject.SetActive(true);
        }
    }

    // Methods

    // Creates GameObjects based off the saved data (similar to a similarly-named function in SearchableMonsterList)
    public void CreateGameObjectsUsingData() {
        // clear the current dictionary
        uiPlayerInfoMap.Clear();

        // clean up the scroll view content
        ClearScrollViewContent();

        // iterate through the data in the map
        foreach (KeyValuePair<PlayerInfo, int> playerInfoCountPair in playerInfoCountMap) {
            PlayerInfo monster = playerInfoCountPair.Key;
            int count = playerInfoCountPair.Value;
            // create a newEntry in the scrollViewContent GameObject
            GameObject newEntry = Instantiate(entryPrefab, scrollViewContent.transform);

            // update the GameObject's text data and other info
            SearchableMonsterListEntryData currentEntryData = newEntry.GetComponent<SearchableMonsterListEntryData>();
            currentEntryData.MonsterName.text = monster.getCharacterName();
            currentEntryData.MonsterHealth.text =
                string.Format("HP: {0} / {1}", monster.getCurrentHP(), monster.getHP());
            currentEntryData.MonsterArmorClass.text = string.Format("Armor Class: {0}", monster.getAC());
            currentEntryData.MonsterCount.gameObject.SetActive(true);
            currentEntryData.MonsterCount.text = count.ToString();

            // -- add any function listeners as needed to the UI element --
            // update monster count if the inputfield is updated
            currentEntryData.MonsterCount.onEndEdit.AddListener(delegate {
                UpdateMonsterCount(monster, currentEntryData.MonsterCount.text);
            });

            // if a selected monster is clicked and delete mode is on, then delete the monster
            Button currentEntryButton = newEntry.GetComponent<Button>();
            currentEntryButton.onClick.AddListener(delegate {
                DeleteMonster(monster);
            });


            // establish GameObject to PlayerInfo mapping
            uiPlayerInfoMap.Add(newEntry.GetComponent<Button>(), monster);
        }
    }

    // Called by SearchableMonsterList, receives the selected monster and saves its information
    public void SelectMonster(PlayerInfo selectedMonster) {
        // if the map contains this monster, add 1 to its count value
        if (playerInfoCountMap.ContainsKey(selectedMonster)) {
            playerInfoCountMap[selectedMonster]++;
        }
        // if the map doesn't contain this monster, add it to the map with a default value of 1
        else {
            playerInfoCountMap.Add(selectedMonster, 1);
        }

        // Update the UI
        CreateGameObjectsUsingData();
    }

    // Delete a monster (when DeleteMode is selected)
    public void DeleteMonster(PlayerInfo monster) {
        if (deleteModeToggle.isOn) {
            // Delete the monster from the player info count map
            playerInfoCountMap.Remove(monster);

            // Update the UI
            CreateGameObjectsUsingData();
        }
    }

    // Retrieves all of the selected monsters
    public PlayerInfoList GetSelectedMonsters() {
        PlayerInfoList selectedMonstersList = new PlayerInfoList();

        // go through the Dictionary of selected monsters
        foreach (KeyValuePair<PlayerInfo, int> pair in playerInfoCountMap) {
            // retrieve the monster info
            PlayerInfo monster = pair.Key;

            // add as many copies of the monster in this loop into the list
            for (int count = 0; count < pair.Value; count++) {
                selectedMonstersList.addPlayer(monster);
            }
        }

        // return the final result 
        return selectedMonstersList;
    }

    // Prints all of the selected monsters in the Debug console
    public void PrintSelectedMonsters() {
        PlayerInfoList selectedMonstersList = GetSelectedMonsters();

        // string to output later into a single Debug message
        string output = "DEBUG: Here are the list of monsters to be added... \n";

        // Prints the relevant monster data
        foreach (PlayerInfo monster in selectedMonstersList.getList()) {
            // append the info to the output string
            output += string.Format("{0}:    HP: {1} / {2}   AC: {3}\n",
                monster.getCharacterName(),
                monster.getCurrentHP(), monster.getHP(),
                monster.getAC());
        }

        // output the output string
        Debug.Log(output);
    }

    // Adds all of the selected monsters to the initiative queue
    public void AddSelectedMonstersToInitiativeQueue() {
        // get the list of all selected monsters
        PlayerInfoList selectedMonsters = GetSelectedMonsters();

        // use the initiative tracker's function to add the monsters to the initiative queue
        foreach (PlayerInfo monster in selectedMonsters.getList()) {
            initiativeTracker.AddCombatant(monster, true /* is monster */);
        }

        // then all of the selected monsters should be unselected (i.e. cleared up)
        ClearSelectedMonsters();
    }

    // Clears the scroll view content
    public void ClearScrollViewContent() {
        for (int idx = 0; idx < scrollViewContent.transform.childCount; idx++) {
            GameObject child = scrollViewContent.transform.GetChild(idx).gameObject;
            Destroy(child);
        }
    }

    // Clears all of the selected monsters
    public void ClearSelectedMonsters() {
        // the playerInfoCountMap should be emptied, since no monsters are selected
        playerInfoCountMap.Clear();

        // then the UI should be updated
        CreateGameObjectsUsingData();
    }

    // Updates the count for the monster using an integer
    public void UpdateMonsterCount(PlayerInfo monster, int newCount) {
        // if newCount <= 0, treat this as if it was deleted
        if (newCount <= 0) {
            Debug.Log("Monster was deleted");
            playerInfoCountMap.Remove(monster);
        }
        else {
            playerInfoCountMap[monster] = newCount;
        }

        // Update the UI
        CreateGameObjectsUsingData();
    }

    // Updates the count for the monster using a string
    //		inputField is meant to 
    public void UpdateMonsterCount(PlayerInfo monster, string newCount) {
        try {
            // Do a conversion
            UpdateMonsterCount(monster, int.Parse(newCount));
        }
        catch (System.FormatException fe) {
            // Log the error
            Debug.LogError(fe.Message);

            // revert the value of the inputfield
            foreach (KeyValuePair<Button, PlayerInfo> pair in uiPlayerInfoMap) {
                if (pair.Value == monster) {
                    /*
					 * This next line of code (yep, just 1 line) is very long so I should explain briefly:
					 * - Find the Button that the PlayerInfo monster is mapped to
					 * - Get the GameObject of the Button
					 * - Get the SearchableMonsterListEntryData Component
					 * - Get the InputField
					 * - Set the text of the InputField to its original value in the playerInfoCountMap.
					 */
                    pair.Key.gameObject.GetComponent<SearchableMonsterListEntryData>().MonsterCount.text
                        = playerInfoCountMap[monster].ToString();
                    break;
                }
            }
        }
    }
}
