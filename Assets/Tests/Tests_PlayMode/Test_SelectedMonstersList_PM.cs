using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Test_SelectedMonstersList_PM : MonoBehaviour
{
    // FIELDS
    const string selectedMonstersName = "Selected Scroll View";


    /// <summary>
    /// Loads up the scene to be tested for every test. Also resets CombatInitiativeQueue.
    /// </summary>
    [SetUp]
    public void Setup() {
        SceneManager.LoadScene("Menu");

        // At the moment, reseting the CombatInitiativeQueue involves getting all combatants and removing them
        foreach(BeingInfo combatant in CombatInitiativeQueue.Instance.GetCombatants()) {
            CombatInitiativeQueue.Instance.RemoveCombatant(combatant);
        }
    }
    /// <summary>
    /// Unloads the scene after every test
    /// </summary>
    [TearDown]
    public void TearDown() {
        SceneManager.UnloadSceneAsync("Menu");
    }

    /// <summary>
    /// This test will insert a fake monster into the selected monsters list, and then add that monster
    /// to the combat initiative queue where it is verified if that monster exists and is correct
    /// </summary>
    [UnityTest]
    public IEnumerator AddSelectedButton_WithOneSelectedMonster_ShouldAddItToInitiativeTracker() {
        // let one frame render
        yield return null;
        // activate the entire searchable menu component
        ActivateSearchableMenu();
        // get the selected monsters list
        SelectedMonstersList selectedMonstersList = GetSelectedMonstersList();

        // create a new monster 
        MonsterInfo monster = new MonsterInfo("monsterName", "monsterType", "monsterAlignment", Size.GARGANTUAN,
            10d, 15, 20, 25, 17, 19, 41, 43, 47);
        // add it to the selected monsters list (simulating SearchableMonsterList action)
        selectedMonstersList.SelectMonster(monster);
        // simulate clicking the "Add Selected Monsters" button
        Button addSelectedButton = GameObject.Find("AddSelectedButton").GetComponent<Button>();
        addSelectedButton.onClick.Invoke();

        // verify that the initiative tracker has the correct information
        Assert.AreEqual(CombatInitiativeQueue.Instance.GetMonstersInCombat()[0], monster);
    }

    /// <summary>
    /// This test is similar to the AddSelectedButton_WithOneSelectedMonster_ShouldAddItToInitiativeTracker
    /// test, but with multiple monsters
    /// </summary>
    [UnityTest]
    public IEnumerator AddSelectedButton_WithMultipleSelectedMonsters_ShouldAddThemToInitiativeTracker() {
        // let one frame render
        yield return null;
        // activate the entire searchable menu component
        ActivateSearchableMenu();
        // get the selected monsters list
        SelectedMonstersList selectedMonstersList = GetSelectedMonstersList();

        // create a list of monsters and add a few monsters to that list. Also select the monsters as well
        List<MonsterInfo> expectedMonsters = new List<MonsterInfo>();
        for (int i = 1; i <= 4; i++) {
            // create a monster and use the loop variable to change some values when making the monster
            MonsterInfo monster = new MonsterInfo($"monsterName{i}", $"monsterType{i}", $"monsterAlignment{i}", Size.GARGANTUAN,
                10d * i, (short) (15 * i), (short) (20 * i), (short) (25 * i),
                (short) (17 * i), (short) (19 * i), (short) (41 * i), (short) (43 * i), (short) (47 * i));
            // add the monster to the list
            expectedMonsters.Add(monster);
            // "select" the monster as well
            selectedMonstersList.SelectMonster(monster);
        }
        // simulate clicking the "Add Selected Monsters" button
        Button addSelectedButton = GameObject.Find("AddSelectedButton").GetComponent<Button>();
        addSelectedButton.onClick.Invoke();

        // verify that the initiative tracker has the correct information
        foreach (MonsterInfo monster in expectedMonsters) {
            Assert.IsTrue(CombatInitiativeQueue.Instance.GetMonstersInCombat().Contains(monster));
        }
    }




    /// ===================================
    /// Helper Functions
    /// ===================================
    
    /// <summary>
    /// This helper function activates the searchable menu
    /// </summary>
    private void ActivateSearchableMenu() {
        // activate the entire searchable menu component
        Button toggleSearchableMenuButton = GameObject.Find("ToggleMenuButton").GetComponent<Button>();
        toggleSearchableMenuButton.onClick.Invoke();
    }

    /// <summary>
    /// Retrieves the selected monsters list
    /// </summary>
    /// <returns></returns>
    private SelectedMonstersList GetSelectedMonstersList() {
        // get the GameObject associated with the selected monsters list
        GameObject selectedMonstersListObject = GameObject.Find(selectedMonstersName);
        // get the SelectedMonstersList component and return it
        SelectedMonstersList selectedMonstersList = selectedMonstersListObject.GetComponent<SelectedMonstersList>();
        return selectedMonstersList;
    }
}
