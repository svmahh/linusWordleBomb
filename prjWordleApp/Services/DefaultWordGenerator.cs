namespace prjWordleApp.Services;

/// <summary>
/// Default implementation of the <see cref="IWordGenerator"/> interface.
/// </summary>
public class DefaultWordGenerator : IWordGenerator
{
    /// <summary>
    /// List of words to be used for word generation.
    /// </summary>
    private static readonly List<string> WordList = new()
    {
        "apple", "banana", "grape", "peach", "melon"
    };

    /// <summary>
    /// Instance of <see cref="Random"/> used for selecting a random word.
    /// </summary>
    private readonly Random _random = new();

    /// <summary>
    /// Generates a random word from the predefined list.
    /// </summary>
    /// <returns>A randomly selected word from the list.</returns>
    public string GenerateWord()
    {
        return WordList[_random.Next(WordList.Count)];
    }
}