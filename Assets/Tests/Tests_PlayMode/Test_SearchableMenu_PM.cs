using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Test_SearchableMenu_PM {
    // FIELDS
    const string searchableMenuName = "SearchableMenu";


    /// <summary>
    /// Loads up the scene to be tested for every test
    /// </summary>
    [SetUp]
    public void Setup() {
        SceneManager.LoadScene("Menu");
    }
    /// <summary>
    /// Unloads the scene after every test
    /// </summary>
    [TearDown]
    public void TearDown() {
        SceneManager.UnloadSceneAsync("Menu");
    }

    // Tests will go down here


    
    /// <summary>
    /// Simple test to see if the SearchableMenu is in the scene
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator SearchableMenu_ShouldExistInScene() {
        // let one frame render
        yield return null;

        // the scene should be loaded, so we can just immediately try to find the SearchableMenu
        Assert.IsNotNull(GameObject.Find(searchableMenuName));
    }

    /// <summary>
    /// Simple test to see if the SearchableMenu GameObject contains the SearchableMonsterList component
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator SearchableMenu_ShouldHaveSearchableMonsterListComponent() {
        // let one frame render
        yield return null;

        // find the SearchableMenu GO
        GameObject searchableMenuObject = GameObject.Find(searchableMenuName);

        // activate the searchable menu by invoking the onClick functionality
        searchableMenuObject.GetComponentInChildren<Button>().onClick.Invoke();

        // verify that it contains the SearchableMonsterListComponent
        Assert.IsNotNull(searchableMenuObject.GetComponentInChildren<SearchableMonsterList>());
    }
}

