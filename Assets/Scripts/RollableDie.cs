using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Functionality for each of the possible dice that can be rolled.
/// </summary>
public class RollableDie : MonoBehaviour
{
    // == Unity Object References ==

    /// <summary>
    /// Text object that displays what was rolled.
    /// This must be assigned in the inspector.
    /// </summary>
    [SerializeField]
    protected TMP_Text rollResultText;

    /// <summary>
    /// Component that is responsible for handling the total sum of die rolls.
    /// This is assigned in the Awake() function using GameObject.Find().
    /// </summary>
    protected RollableDieSum sumObject;

    // == Fields ==

    /// <summary>
    /// This is the highest number that can be rolled on the die, which
    /// is set in the inspector (e.g. D2 should have maxNumber = 2).
    /// </summary>
    [SerializeField]
    protected int maxNumber = 2; // default value is 2

    /// <summary>
    /// This is the minimum roll value that can be rolled on the die.
    /// </summary>
    private const int MINIMUM_ROLL_VALUE = 1;

    // == Unity Methods ==

    /// <summary>
    /// For RollableDie, this function is used to instantiate fields.
    /// </summary>
    protected void Awake() {
        sumObject = GameObject.Find("TotalSumText").GetComponent<RollableDieSum>();
    }

    // == Methods ==

    /// <summary>
    /// This function is called when the user presses the button
    /// to roll the die. This calls the Coroutine that handles
    /// the process of a roll.
    /// </summary>
    public void DoRoll() {
        // Call the Coroutine that handles the roll
        StartCoroutine(RollDieCR());
    }

    /// <summary>
    /// Routine for rolling a die and visualizing the result.
    /// </summary>
    private IEnumerator RollDieCR() {
        // first, prevent the user from re-rolling during this coroutine
        Button rollDieButton = GetComponent<Button>();
        rollDieButton.interactable = false;

        // run the visual effect of rolling a die and wait for it to finish
        yield return ShuffleResultNumberCR();

        // then roll the die and provide an actual output
        int rollResult = GenerateRollValue();
        rollResultText.text = rollResult.ToString();

        // add it to the total value of rolls
        sumObject.AddToSum(rollResult);

        // allow the user to be able to roll the die again
        rollDieButton.interactable = true;
    }

    /// <summary>
    /// This Coroutine creates a visual effect in which the result text output
    /// appears to change values rapidly.
    /// </summary>
    private IEnumerator ShuffleResultNumberCR() {
        // duration of visual effect in seconds
        float duration = 0.5f;

        // end time calculated using the duration
        float endTime = Time.time + duration;

        // while this effect is being displayed, change the output text visual to indicate that it is being rolled
        // first, save the original color and create a new color that is faded
        Color originalResultTextColor = rollResultText.color;
        Color fadedResultTextColor = new Color(originalResultTextColor.r,
            originalResultTextColor.g, originalResultTextColor.b, originalResultTextColor.a * 0.5f);
        // then update the result text element to have the faded effect
        rollResultText.color = fadedResultTextColor;

        // loop - generate a random number and display it until duration expires
        while (Time.time < endTime) {
            // calculate a random number
            int randomNumber = GenerateRollValue();

            // update the result text element
            rollResultText.text = randomNumber.ToString();

            // let a frame pass
            yield return new WaitForSeconds(0.05f);
        }

        // at the end, return the result text color to its original color
        rollResultText.color = originalResultTextColor;
    }

    /// <summary>
    /// Helper function that generates a random number between
    /// 1 and the maxNumber (inclusive) (i.e. [1, maxNumber])
    /// </summary>
    /// <returns></returns>
    private int GenerateRollValue() {
        return Random.Range(MINIMUM_ROLL_VALUE, maxNumber + 1);
    }
}
