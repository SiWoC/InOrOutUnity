using System.Collections;
using System;
using Globals;
using Assets.Scripts.Classes.OpenAI;
using Assets.Scripts.Classes.Claude;
using UnityEngine.Localization;

public class WordSetGenerator
{
    public static event Action BuzzWordsGenerated;
    private static LocalizedString locOriginStaticList = new LocalizedString(GameManager.LOCALIZATION_TABLE_NAME, "WordSetGenerator-OriginStaticList");

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

    public static string GetOriginStaticListText()
    {
        return locOriginStaticList.GetLocalizedString();
    }

}
