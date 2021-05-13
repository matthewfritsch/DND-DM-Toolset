/// <summary>
/// Components that implement ISaveable will have a function that returns
/// the data that is to be saved
/// </summary>
public interface ISaveable
{
    /// <summary>
    /// Creates a string that holds the data to be saved.
    /// </summary>
    /// <returns>String representation of data that is saved.</returns>
    public string CreateSaveData();

    /// <summary>
    ///     Populate a SaveData class with the information that is important and needs to be saved
    ///     Idea from the unity presistent data tutorial
    /// </summary>
    public void PopulateSaveData(SaveData sd);
    // void LoadFromSaveData(SaveData sd);
}
