using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor.TestTools;

public class Test_InitiativeTracker_Saving_EM
{
    // Variables for testing
    GameObject fakeInitiativeQueue;
    CombatInitiativeQueue fakeCombatInitiativeQueue;

    /// <summary>
    /// SetUp function - this is called before each test
    /// </summary>
    [SetUp]
    public void Setup() {
        // create a new gameobject
        fakeInitiativeQueue = new GameObject();

        // attach the CombatInitiativeQueue component
        fakeCombatInitiativeQueue = fakeInitiativeQueue.AddComponent<CombatInitiativeQueue>();
    }

    // =====================================
    // Tests go below here

    /// <summary>
    /// This is a basic test to ensure that the CreateSaveData function has been implemented
    /// for the CombatInitiativeQueue and has returned anything.
    /// </summary>
    [Test]
    public void InitiativeTracker_WhenTrackingNothing_ShouldReturnNotNull() {
        // retrieve the ISaveable
        ISaveable saveableData = fakeInitiativeQueue.GetComponent<CombatInitiativeQueue>();

        // create and hold onto the save data
        string saveData = saveableData.CreateSaveData();

        // assert that the save data is not null
        Assert.IsNotNull(saveData);
    }
    
    /// <summary>
    /// This is a test that tracks one being, and ensures that it is saved correctly
    /// </summary>
    [Test]
    public void InitiativeTracker_WhenTrackingOneBeing_ShouldBeSavedCorrectly() {
        // generate and add a new being
        fakeCombatInitiativeQueue.AddToCombat(GenerateRandomBeing());

        // retrieve the ISaveable
        ISaveable saveableData = fakeCombatInitiativeQueue;

        // get the save data (which is a list of being infos)
        string saveData = saveableData.CreateSaveData();

        // manually load from the save data, which should be a list of being infos
        BeingInfoList loadedData = JsonUtility.FromJson<BeingInfoList>(saveData);

        // now ensure that the randomly generated being is correctly generated

        // first, check if the expected list of combatants is the same size as the loaded list
        Assert.AreEqual(fakeCombatInitiativeQueue.GetCombatants().Count,
            loadedData.getList().Count);

        // get the expected and actual beings
        BeingInfo expected = fakeCombatInitiativeQueue.GetCombatants()[0];
        BeingInfo actual = loadedData.getList()[0];

        // assert that some stats are the same
        Assert.AreEqual(expected, actual);
    }

    /// <summary>
    /// This is a test that tracks multiple beings, and ensures that all are saved correctly
    /// </summary>
    [Test]
    public void InitiativeTracker_WhenTrackingManyBeings_ShouldBeSavedCorrectly() {
        // generate and add multiple new beings
        for (int i = 0; i < 7; i++) {
            fakeCombatInitiativeQueue.AddToCombat(GenerateRandomBeing());
        }

        // retrieve the ISaveable
        ISaveable saveableData = fakeCombatInitiativeQueue;

        // get the save data (which is a list of being infos)
        string saveData = saveableData.CreateSaveData();

        // manually load from the save data, which should be a list of being infos
        BeingInfoList loadedData = JsonUtility.FromJson<BeingInfoList>(saveData);

        // now ensure that the randomly generated being is correctly generated

        // first, check if the expected list of combatants is the same size as the loaded list
        Assert.AreEqual(fakeCombatInitiativeQueue.GetCombatants().Count,
            loadedData.getList().Count);

        // using the same index, loop through both lists and ensure that corresponding beings are the same
        for (int idx = 0; idx < fakeCombatInitiativeQueue.GetCombatants().Count; idx++) {
            // get the expected and actual beings
            BeingInfo expected = fakeCombatInitiativeQueue.GetCombatants()[idx];
            BeingInfo actual = loadedData.getList()[idx];

            // assert that some stats are the same
            Assert.AreEqual(expected, actual);
        }
    }

    // =====================================
    // Helper functions go below here


    /// <summary>
    /// Generates a completely random being (either monster or player) for testing
    /// </summary>
    /// <returns>a new being</returns>
    public BeingInfo GenerateRandomBeing() {
        // decide randomly whether the being should be a player or monster
        bool isPlayer = Random.Range(0, 2) % 2 == 0;

        // randomly generated numbers for strings
        int randomNumber = Random.Range(77, 178); // range is randomly chosen

        // the being to return later
        BeingInfo generatedBeing;

        // set up a randomly generated player
        if (isPlayer) {
            generatedBeing = new PlayerInfo(
                $"player_name_{randomNumber}",
                $"character_name_{randomNumber + 7}",
                $"class_name_{randomNumber + 13}",
                randomNumber / 7,
                randomNumber / 3
            );
        }
        // set up a randomly generated monster
        else {
            generatedBeing = new MonsterInfo(
                $"monster_name_{randomNumber}",
                $"monster_type_{randomNumber + 1}",
                $"monster_alignment_{randomNumber + 2}",
                Size.MEDIUM,
                (double) randomNumber + 3,
                (short) (randomNumber + 5),
                (short) (randomNumber + 7),
                (short) (randomNumber + 11),
                (short) (randomNumber + 13),
                (short) (randomNumber + 17),
                (short) (randomNumber + 19),
                (short) (randomNumber + 23),
                (short) (randomNumber + 29)
            );
        }

        // return the generated being
        return generatedBeing;
    }
}

[System.Serializable]
public class StringList {
    public List<string> strings;
}
