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
    [SerializeField]
    private Image backgroundImage; // image used for the background of the menu when it is open

    // Fields
    private BeingInfoList data; // Currently uses dummy data
    private Dictionary<Button, MonsterInfo> uiMonsterInfoMap; // maps UI elements to MonsterInfo Objects
    private MonsterParser parser;

    // Unity Methods
    void Start() {
        // initialize fields
        uiMonsterInfoMap = new Dictionary<Button, MonsterInfo>();

        // -- initialize data --
        data = new BeingInfoList();

        // parse through the data
        parser = new MonsterParser(Application.dataPath + "/monsters.csv");
        List<MonsterInfo> parsedData = parser.read();

        // iterate through parsed data to add to data
        foreach(MonsterInfo monster in parsedData) {
            data.addBeing(monster);
        }

        // update UI
        CreateGameObjectsUsingData();
    }

    // Methods

    // toggles the Scroll View between active and inactive
    public void ToggleScrollView() {
        // toggle the main searchable scroll view
        scrollView.SetActive(!scrollView.activeInHierarchy);

        // boolean for checking if the other elements should be active or not, based off the searchable scroll view's active state
        bool scrollViewActive = scrollView.activeInHierarchy;

        // match the active state of the selectable scroll view with the searchable scroll view
        selectedMonstersList.gameObject.SetActive(scrollViewActive);

        // match the active state of the background sprite with the searchable scroll view
        backgroundImage.gameObject.SetActive(scrollViewActive);

    }

    // converts data into GameObjects using the entryPrefab to create it.
    //		this can be filtered by character name
    public void CreateGameObjectsUsingData(string filter = "") {
        // clear the current dictionary
        uiMonsterInfoMap.Clear();

        // clean up the scroll view content
        ClearScrollViewContent();

        // iterate through the data
        foreach (MonsterInfo monster in data.getList()) {
            // apply filter (case insensitive)
            if (monster.getMonsterName().ToLower().Contains(filter.ToLower())) {
                // create a newEntry in the scrollViewContent GameObject
                GameObject newEntry = Instantiate(entryPrefab, scrollViewContent.transform);

                // add any function listeners as needed to the UI element
                Button newEntryButton = newEntry.GetComponent<Button>();
                newEntryButton.onClick.AddListener(() => SelectMonster(newEntryButton));

                // update the GameObject's text data
                SearchableMonsterListEntryData currentEntryData = newEntry.GetComponent<SearchableMonsterListEntryData>();
                currentEntryData.MonsterName.text = monster.getMonsterName();
                currentEntryData.MonsterHealth.text =
                    string.Format("HP: {0} / {1}", monster.getCurrentHP(), monster.getHP());
                currentEntryData.MonsterArmorClass.text = string.Format("Armor Class: {0}", monster.getAC());

                // establish GameObject to monster mapping
                uiMonsterInfoMap.Add(newEntry.GetComponent<Button>(), monster);
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
        MonsterInfo selectedMonster = uiMonsterInfoMap[action];

        // pass it onto the component that handles selected monsters
        selectedMonstersList.SelectMonster(selectedMonster);
    }
}
