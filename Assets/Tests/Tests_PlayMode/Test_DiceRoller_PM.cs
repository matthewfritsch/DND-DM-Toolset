using NUnit.Framework;
using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

/// <summary>
/// This tests the Dice Roller in the DiceRollerScene, but in theory
/// this should apply to when the dice roller is used in Menu since the prefab
/// is the same
/// </summary>
public class Test_DiceRoller_PM {
    /// <summary>
    /// Code modified from this link: https://forum.unity.com/threads/loading-a-test-scene-without-adding-it-to-build-settings.771110/#post-5135591
    /// (reply #3).
    /// <br></br>
    /// <br></br>
    /// This code essentially loads the test scene without the need to add it to the full build,
    /// preventing the tests from failing if the scene were to be unloaded when doing a proper
    /// build.
    /// </summary>
    [UnitySetUp]
    public IEnumerator LoadTestScene() {
        yield return EditorSceneManager.LoadSceneAsyncInPlayMode("Assets/Scenes/DiceRollerScene.unity", new LoadSceneParameters(LoadSceneMode.Additive));
    }

    /// <summary>
    /// Code modified from this link: https://forum.unity.com/threads/loading-a-test-scene-without-adding-it-to-build-settings.771110/#post-5135591
    /// (reply #3).
    /// <br></br>
    /// <br></br>
    /// This code unloads the test scene that was previously added in the LoadTestScene() function.
    /// </summary>
    [UnityTearDown]
    public IEnumerator UnloadTestScene() {
        yield return SceneManager.UnloadSceneAsync("Assets/Scenes/DiceRollerScene.unity");
    }

    // == Tests ==

    /// <summary>
    /// This test simply checks if the setup is done correctly.
    /// </summary>
    [UnityTest]
    public IEnumerator DiceRoller_IsSetUpCorrectly() {
        // render 1 frame
        yield return null;

        // pass the test
        Assert.Pass();
    }

    /// <summary>
    /// Similar to the _IsSetUpCorrectly() test, this checks if the DiceRollerComponent exists
    /// in the current scene
    /// </summary>
    [UnityTest]
    public IEnumerator DiceRoller_ComponentIsInScene() {
        // render 1 frame
        yield return null;

        // try to find the dice roller component
        Assert.IsNotNull(GameObject.Find("DiceRollerComponent"));
    }

    /// <summary>
    /// This tests if rolling the D2 die 5 times results in values that are
    /// within the range [1, 2].
    /// </summary>
    [UnityTest]
    public IEnumerator DiceRoller_WhenClickingD2FiveTimes_ShouldDisplayValidValue() {
        // render 1 frame
        yield return null;

        // Find and roll the D2 die 5 times
        yield return FindAndRollDieMultipleTimes("D2Roller", 1, 2, 5);
    }

    /// <summary>
    /// This tests if rolling the D4 die 5 times results in values that are
    /// within the range [1, 4].
    /// </summary>
    [UnityTest]
    public IEnumerator DiceRoller_WhenClickingD4FiveTimes_ShouldDisplayValidValue() {
        // render 1 frame
        yield return null;

        // Find and roll the D2 die 5 times
        yield return FindAndRollDieMultipleTimes("D4Roller", 1, 4, 5);
    }

    /// <summary>
    /// This tests if rolling the D6 die 5 times results in values that are
    /// within the range [1, 6].
    /// </summary>
    [UnityTest]
    public IEnumerator DiceRoller_WhenClickingD6FiveTimes_ShouldDisplayValidValue() {
        // render 1 frame
        yield return null;

        // Find and roll the D2 die 5 times
        yield return FindAndRollDieMultipleTimes("D6Roller", 1, 6, 5);
    }

    /// <summary>
    /// This tests if rolling the D8 die 5 times results in values that are
    /// within the range [1, 8].
    /// </summary>
    [UnityTest]
    public IEnumerator DiceRoller_WhenClickingD8FiveTimes_ShouldDisplayValidValue() {
        // render 1 frame
        yield return null;

        // Find and roll the D2 die 5 times
        yield return FindAndRollDieMultipleTimes("D8Roller", 1, 8, 5);
    }

    /// <summary>
    /// This tests if rolling the D10 die 5 times results in values that are
    /// within the range [1, 10].
    /// </summary>
    [UnityTest]
    public IEnumerator DiceRoller_WhenClickingD10FiveTimes_ShouldDisplayValidValue() {
        // render 1 frame
        yield return null;

        // Find and roll the D2 die 5 times
        yield return FindAndRollDieMultipleTimes("D10Roller", 1, 10, 5);
    }

    /// <summary>
    /// This tests if rolling the D12 die 5 times results in values that are
    /// within the range [1, 12].
    /// </summary>
    [UnityTest]
    public IEnumerator DiceRoller_WhenClickingD12FiveTimes_ShouldDisplayValidValue() {
        // render 1 frame
        yield return null;

        // Find and roll the D2 die 5 times
        yield return FindAndRollDieMultipleTimes("D12Roller", 1, 12, 5);
    }

    /// <summary>
    /// This tests if rolling the D20 die 5 times results in values that are
    /// within the range [1, 20].
    /// </summary>
    [UnityTest]
    public IEnumerator DiceRoller_WhenClickingD20FiveTimes_ShouldDisplayValidValue() {
        // render 1 frame
        yield return null;

        // Find and roll the D2 die 5 times
        yield return FindAndRollDieMultipleTimes("D20Roller", 1, 20, 5);
    }

    // == Other Functions (e.g., Helper functions) ==

    /// <summary>
    /// Coroutine that finds and rolls a die a number of times, asserting
    /// that it is within bounds.
    /// </summary>
    /// <param name="dieGameObjectName">Name of the GameObject (e.g., D2Roller)</param>
    /// <param name="expectedLowestValue">The lowest value that can be rolled, usually 1</param>
    /// <param name="expectedHighestValue">The highest value that can be rolled, (e.g., 2, 4, 6, 8, 10, 12, 20)</param>
    /// <param name="iterations">The number of times to roll the die, defaulting to 5 times.</param>
    private IEnumerator FindAndRollDieMultipleTimes(string dieGameObjectName,
        int expectedLowestValue,
        int expectedHighestValue,
        int iterations = 5) {


        // get the GameObject of the die we want to roll
        GameObject dxRollerObject = GameObject.Find(dieGameObjectName);

        // press the button that rolls this die 5 times
        for (int currentIteration = 0; currentIteration < iterations; currentIteration++) {
            // get the button component
            Button dxRollerButton = dxRollerObject.GetComponent<Button>();

            // press the button
            dxRollerButton.onClick.Invoke();

            // wait for the button to be interactable (indicating that the roll is complete)
            yield return new WaitUntil(() => dxRollerButton.interactable);

            // then check the displayed rolled value to ensure that it is within bounds
            RollableDie dxRollerDie = dxRollerObject.GetComponent<RollableDie>();
            int rolledValue = int.Parse(dxRollerDie.rollResultText.text);
            Assert.IsTrue(rolledValue >= expectedLowestValue);
            Assert.IsTrue(rolledValue <= expectedHighestValue);
        }
    }
}
