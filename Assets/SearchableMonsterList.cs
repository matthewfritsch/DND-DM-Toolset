using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchableMonsterList : MonoBehaviour {
    // Unity Objects
    [SerializeField]
    private GameObject scrollView;
    [SerializeField]
    private GameObject scrollViewContent;   // target location to add new entries
    [SerializeField]
    private GameObject entryPrefab;
    [SerializeField]
    private SelectedMonstersList selectedMonstersList; // selected monster handler

    // Fields
    private PlayerInfoList data; // Currently uses dummy data
    private Dictionary<Button, PlayerInfo> uiPlayerInfoMap; // maps UI elements to PlayerInfo Objects

    // Unity Methods
    void Start() {
        // initialize fields
        uiPlayerInfoMap = new Dictionary<Button, PlayerInfo>();

        // initialize the dummy data and create GameObjects for it
        InitDummyData();
        CreateGameObjectsUsingData();
    }

    // Methods

    // toggles the Scroll View between active and inactive
    public void ToggleScrollView() {
        // deactivate the main searchable scroll view
        scrollView.SetActive(!scrollView.activeInHierarchy);

        // deactivate the selectable scroll view
        selectedMonstersList.gameObject.SetActive(scrollView.activeInHierarchy);
    }

    // converts data into GameObjects using the entryPrefab to create it.
    //		this can be filtered by character name
    public void CreateGameObjectsUsingData(string filter = "") {
        // clear the current dictionary
        uiPlayerInfoMap.Clear();

        // clean up the scroll view content
        ClearScrollViewContent();

        // iterate through the data
        foreach (PlayerInfo playerInfo in data.getList()) {
            // apply filter (case insensitive)
            if (playerInfo.getCharacterName().ToLower().Contains(filter.ToLower())) {
                // create a newEntry in the scrollViewContent GameObject
                GameObject newEntry = Instantiate(entryPrefab, scrollViewContent.transform);

                // add any function listeners as needed to the UI element
                Button newEntryButton = newEntry.GetComponent<Button>();
                newEntryButton.onClick.AddListener(() => SelectMonster(newEntryButton));

                // update the GameObject's text data
                SearchableMonsterListEntryData currentEntryData = newEntry.GetComponent<SearchableMonsterListEntryData>();
                currentEntryData.MonsterName.text = playerInfo.getCharacterName();
                currentEntryData.MonsterHealth.text =
                    string.Format("HP: {0} / {1}", playerInfo.getCurrentHP(), playerInfo.getMaxHP());
                currentEntryData.MonsterArmorClass.text = string.Format("Armor Class: {0}", playerInfo.getArmorClass());

                // establish GameObject to PlayerInfo mapping
                uiPlayerInfoMap.Add(newEntry.GetComponent<Button>(), playerInfo);
            }
        }
    }

    // deletes everything from the scroll view content
    public void ClearScrollViewContent() {
        for (int idx = 0; idx < scrollViewContent.transform.childCount; idx++) {
            GameObject child = scrollViewContent.transform.GetChild(idx).gameObject;
            Destroy(child);
        }
    }

    // Callback function when a monster is selected to add it to the list of selected monsters
    public void SelectMonster(Button action) {
        PlayerInfo selectedMonster = uiPlayerInfoMap[action];

        // pass it onto the component that handles selected monsters
        selectedMonstersList.SelectMonster(selectedMonster);
    }

    // Helper Methods

    // initializes the dummy data
    private void InitDummyData() {
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
