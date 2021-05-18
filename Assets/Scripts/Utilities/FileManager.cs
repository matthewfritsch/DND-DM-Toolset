using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
///     Utility class to make saving/loading from files easier
///     Originally from the UniteNow20-Persistent-Data tutorial GitHub
/// </summary>

public static class FileManager
{
    public static bool WriteToFile(string a_FileName, string a_FileContents)
    {
        var fullPath = Utils.GetSaveFilePath(a_FileName);

        try
        {
            File.WriteAllText(fullPath, a_FileContents);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to write to {fullPath} with exception {e}");
            return false;
        }
    }

    public static bool LoadFromFile(string a_FileName, out string result)
    {
        var fullPath = Utils.GetSaveFilePath(a_FileName);

        try
        {
            result = File.ReadAllText(fullPath);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read from {fullPath} with exception {e}");
            result = "";
            return false;
        }
    }
}