using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Class with general functions/variables that may be useful in multiple locations.
///     Functions and variable must be static.
///     Reduce unneccessary duplication!
/// </summary>
public static class Utils {
    public static string S_SAVE_FILE_EXTENSION = ".dat";
    public static string S_PREF_KEY_SAVE = "recentSaveFile";

    /// <summary>
    ///     Returns the path string for a given save file
    /// </summary>
    public static string GetSaveFilePath(string saveFileName) {
        // Append the extension to the save file name
        saveFileName += S_SAVE_FILE_EXTENSION;
        return Path.Combine(Application.persistentDataPath, saveFileName);
    }

    /// <summary>
    ///     Returns the name of a save file out of a valid save file path
    /// </summary>
    public static string GetSaveFileName(string saveFilePath) {
        string result = Path.GetFileNameWithoutExtension(saveFilePath);
        return result;
    }
}
