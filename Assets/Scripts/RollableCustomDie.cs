using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This takes the RollableDie component and adds functionality
/// so that it supports an input field.
/// </summary>
public class RollableCustomDie : RollableDie
{
    // == Unity Object References
    /// <summary>
    /// This is the input field for inputting custom rolls. This is
    /// defined in Awake().
    /// </summary>
    private TMP_InputField customRollInputField;

    // == Unity Methods

    /// <summary>
    /// This Awake function calls the parent's Awake function and then
    /// does anything else that is needed. The "new" keyword is meant to indicate
    /// that hiding the parent's Awake function is intended
    /// </summary>
    new private void Awake() {
        // call the parent's Awake() first to instantiate fields
        base.Awake();

        // then instantiate any other fields as needed
        
        // instantiate input field by finding the component in children
        customRollInputField = GetComponentInChildren<TMP_InputField>();
        customRollInputField.text = MINIMUM_CUSTOM_ROLL.ToString(); // set default value to minimum possible value
    }

    // == Fields ==
    private const int MINIMUM_CUSTOM_ROLL = 2;
    private const int MAXIMUM_CUSTOM_ROLL = 999;

    // == Methods ==

    /// <summary>
    /// Called by the input field (onEndEdit) to set the max number
    /// </summary>
    /// <param name="input"></param>
    public void SetMaxNumber(string input) {
        // convert it to an int
        int newMaxNumber;

        // try to convert it to an int
        try {
            newMaxNumber = int.Parse(input);

            // set the max number, clamped within the bounds
            maxNumber = Mathf.Clamp(newMaxNumber, MINIMUM_CUSTOM_ROLL, MAXIMUM_CUSTOM_ROLL);
        }
        catch (System.FormatException exc) {
            // Log the error as a warning (since nothing actually broke)
            Debug.LogWarning($"RollableCustomDie: The received input, \"{input}\" could not be converted to " +
                $"an integer, so the value has not been changed.\n Error message: \n{exc.Message}");

            // reset the inputfield text to be what the current max number is
            customRollInputField.text = maxNumber.ToString();
        }

    }

    /// <summary>
    /// Called by the input field (onValueChanged) to prevent the negative character
    /// from appearing in the first position
    /// </summary>
    /// <param name="input"></param>
    public void ValidateInputNoNegative(string input) {
        // First, prevent any negative values by ignoring the negative character ('-') in the first position
        if (input.Length > 0 && input[0].Equals('-')) {
            // remove it from the input text using the Substring method
            customRollInputField.text = input.Substring(1);
        }
    }
}
