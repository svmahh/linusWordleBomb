namespace prjWordleApp.Services;

/// <summary>
/// Interface for generating words.
/// </summary>
public interface IWordGenerator
{
    /// <summary>
    /// Generates a random word.
    /// </summary>
    /// <returns>A randomly generated word.</returns>
    string GenerateWord();
}