using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Component handles calculating the sum of rolls.
/// </summary>
public class RollableDieSum : MonoBehaviour
{
    // == Unity Object References ==

    /// <summary>
    /// This is the Text object that is used when displaying the sum, and also
    /// stores the current sum as well.
    /// </summary>
    private TMP_Text sumText;

    /// <summary>
    /// This is the Toggle that verifies if the user wants to calculate the sum
    /// </summary>
    private Toggle enableSumToggle;

    // == Unity Functions ==

    /// <summary>
    /// For RollableDieSum, this function instantiates the sumText field and
    /// sets its text value to "0". This also initializes any
    /// other fields as needed.
    /// </summary>
    private void Awake() {
        // initialize sumText TMP_Text field
        sumText = GetComponent<TMP_Text>();
        sumText.text = "0";

        // initialize enableSumToggle
        enableSumToggle = GetComponentInChildren<Toggle>();
    }

    // == Functions ==

    /// <summary>
    /// Retrieves the current sum
    /// </summary>
    /// <returns></returns>
    public int GetCurrentSum() {
        return int.Parse(sumText.text);
    }

    /// <summary>
    /// Adds a value to the current sum, but only if sum calculation is enabled.
    /// </summary>
    /// <param name="valueToAdd"></param>
    public void AddToSum(int valueToAdd) {
        // if sum calculation is not allowed, do not modify sumText
        if (!enableSumToggle.isOn) {
            return;
        }

        // calculate the new sum using the current sum and the value that was passed in
        int newSum = GetCurrentSum() + valueToAdd;

        // update the text element to have this new sum
        sumText.text = newSum.ToString();
    }

    /// <summary>
    /// Resets the current sum to be 0.
    /// </summary>
    public void ResetSum() {
        // set the sumText object to be "0"
        sumText.text = "0";
    }
}
