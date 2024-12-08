using System.Collections;
using System;
using Globals;
using Assets.Scripts.Classes.OpenAI;
using Assets.Scripts.Classes.Claude;

public class WordSetGenerator
{
    public static event Action BuzzWordsGenerated;

    internal static void GenerateBuzWordsSync(string origin)
    {
        WordSet wordset = WordSets.GetRandomSet();
        GameManager.SetBuzzWords(wordset, origin);
        GeneratorReady();
    }

    public static IEnumerator GenerateBuzzWordsAsync()
    {
        switch (GameManager.GetGeneratorType())
        {
            case GeneratorType.Claude:
                return ClaudeGenerator.CallClaudeApi();
            default:
                return OpenAIGenerator.CallOpenAI();
        }
    }

    public static void GeneratorReady()
    {
        BuzzWordsGenerated?.Invoke();
    }

}
