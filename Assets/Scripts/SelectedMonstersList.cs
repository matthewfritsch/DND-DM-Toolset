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

    // field for mapping Buttons to MonsterInfos
    private Dictionary<Button, MonsterInfo> uiMonsterInfoMap;
    // field for mapping monsters to their to count information
    private Dictionary<MonsterInfo, int> monsterInfoCountMap;

    // Unity Methods
    void Awake() {
        // Initialize Fields
        uiMonsterInfoMap = new Dictionary<Button, MonsterInfo>();
        monsterInfoCountMap = new Dictionary<MonsterInfo, int>();
    }

    void Update() {
        // set the controls object inactive if there are no selected monsters
        if (uiMonsterInfoMap.Count <= 0 && controlsObject.activeInHierarchy) {
            // set the toggle to be off first
            deleteModeToggle.isOn = false;

            controlsObject.SetActive(false);
        }
        // set the controls object active if there are selected monsters
        else if (uiMonsterInfoMap.Count >= 1 && !controlsObject.activeInHierarchy) {
            controlsObject.SetActive(true);
        }
    }

    // Methods

    // Creates GameObjects based off the saved data (similar to a similarly-named function in SearchableMonsterList)
    public void CreateGameObjectsUsingData() {
        // clear the current dictionary
        uiMonsterInfoMap.Clear();

        // clean up the scroll view content
        ClearScrollViewContent();

        // iterate through the data in the map
        foreach (KeyValuePair<MonsterInfo, int> monsterInfoCountPair in monsterInfoCountMap) {
            MonsterInfo monster = monsterInfoCountPair.Key;
            int count = monsterInfoCountPair.Value;
            // create a newEntry in the scrollViewContent GameObject
            GameObject newEntry = Instantiate(entryPrefab, scrollViewContent.transform);

            // update the GameObject's text data and other info
            SearchableMonsterListEntryData currentEntryData = newEntry.GetComponent<SearchableMonsterListEntryData>();
            currentEntryData.MonsterName.text = monster.getMonsterName();
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


            // establish GameObject to MonsterInfo mapping
            uiMonsterInfoMap.Add(newEntry.GetComponent<Button>(), monster);
        }
    }

    // Called by SearchableMonsterList, receives the selected monster and saves its information
    public void SelectMonster(MonsterInfo selectedMonster) {
        // if the map contains this monster, add 1 to its count value
        if (monsterInfoCountMap.ContainsKey(selectedMonster)) {
            monsterInfoCountMap[selectedMonster]++;
        }
        // if the map doesn't contain this monster, add it to the map with a default value of 1
        else {
            monsterInfoCountMap.Add(selectedMonster, 1);
        }

        // Update the UI
        CreateGameObjectsUsingData();
    }

    // Delete a monster (when DeleteMode is selected)
    public void DeleteMonster(MonsterInfo monster) {
        if (deleteModeToggle.isOn) {
            // Delete the monster from the monster info count map
            monsterInfoCountMap.Remove(monster);

            // Update the UI
            CreateGameObjectsUsingData();
        }
    }

    // Retrieves all of the selected monsters
    public BeingInfoList GetSelectedMonsters() {
        BeingInfoList selectedMonstersList = new BeingInfoList();

        // go through the Dictionary of selected monsters
        foreach (KeyValuePair<MonsterInfo, int> pair in monsterInfoCountMap) {
            // retrieve the monster info
            MonsterInfo monster = pair.Key;

            // add as many copies of the monster in this loop into the list
            for (int count = 0; count < pair.Value; count++) {
                selectedMonstersList.addBeing(monster);
            }
        }

        // return the final result 
        return selectedMonstersList;
    }

    // Prints all of the selected monsters in the Debug console
    public void PrintSelectedMonsters() {
        BeingInfoList selectedMonstersList = GetSelectedMonsters();

        // string to output later into a single Debug message
        string output = "DEBUG: Here are the list of monsters to be added... \n";

        // Prints the relevant monster data
        foreach (MonsterInfo monster in selectedMonstersList.getList()) {
            // append the info to the output string
            output += string.Format("{0}:    HP: {1} / {2}   AC: {3}\n",
                monster.getMonsterName(),
                monster.getCurrentHP(), monster.getHP(),
                monster.getAC());
        }

        // output the output string
        Debug.Log(output);
    }

    // Adds all of the selected monsters to the initiative queue
    public void AddSelectedMonstersToInitiativeQueue() {
        // Get the list of all selected monsters
        BeingInfoList selectedMonsters = GetSelectedMonsters();

        // use the initiative tracker's function to add the monsters to the initiative queue
        foreach (BeingInfo monster in selectedMonsters.getList()) {
            initiativeTracker.AddCombatant(monster);
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
        // the monsterInfoCountMap should be emptied, since no monsters are selected
        monsterInfoCountMap.Clear();

        // then the UI should be updated
        CreateGameObjectsUsingData();
    }

    // Updates the count for the monster using an integer
    public void UpdateMonsterCount(MonsterInfo monster, int newCount) {
        // if newCount <= 0, treat this as if it was deleted
        if (newCount <= 0) {
            Debug.Log("Monster was deleted");
            monsterInfoCountMap.Remove(monster);
        }
        else {
            monsterInfoCountMap[monster] = newCount;
        }

        // Update the UI
        CreateGameObjectsUsingData();
    }

    // Updates the count for the monster using a string
    //		inputField is meant to 
    public void UpdateMonsterCount(MonsterInfo monster, string newCount) {
        try {
            // Do a conversion
            UpdateMonsterCount(monster, int.Parse(newCount));
        }
        catch (System.FormatException fe) {
            // Log the error
            Debug.LogError(fe.Message);

            // revert the value of the inputfield
            foreach (KeyValuePair<Button, MonsterInfo> pair in uiMonsterInfoMap) {
                if (pair.Value == monster) {
                    /*
					 * This next line of code (yep, just 1 line) is very long so I should explain briefly:
					 * - Find the Button that the MonsterInfo monster is mapped to
					 * - Get the GameObject of the Button
					 * - Get the SearchableMonsterListEntryData Component
					 * - Get the InputField
					 * - Set the text of the InputField to its original value in the monsterInfoCountMap.
					 */
                    pair.Key.gameObject.GetComponent<SearchableMonsterListEntryData>().MonsterCount.text
                        = monsterInfoCountMap[monster].ToString();
                    break;
                }
            }
        }
    }
}
