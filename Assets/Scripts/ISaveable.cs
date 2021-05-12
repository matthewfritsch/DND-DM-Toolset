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
}
