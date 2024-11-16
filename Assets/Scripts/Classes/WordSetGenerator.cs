using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using Globals;
using System.Diagnostics.CodeAnalysis;

public class WordSetGenerator
{
    public static event Action BuzzWordsGenerated;

    private static string OPENAI_API_KEY = "";
    private static string OPENAI_URL = "https://api.openai.com/v1/chat/completions";
    //private static readonly string CLAUDE_API_KEY = "";

    internal static void GenerateBuzWordsSync()
    {
        GameManager.SetBuzzWords("Vakantie", "IJsje");
        BuzzWordsGenerated?.Invoke();
    }

    public static IEnumerator GenerateBuzzWordsAsync()
    {
        switch (GameManager.generatorType)
        {
            /*
             case GeneratorType.Claude:
                break;
            */
            default:
                return CallOpenAI();
        }
    }

    public static IEnumerator CallOpenAI()
    {
        /*
        string promptText = "Generate a single set of two loosely connected words suitable for a 14-year-old.\n\n" +
                        "- The first word should be one that a group of people (insiders) might be discussing in everyday life, not too focused on school subjects.\n" +
                        "- The second word is what someone listening nearby (an outsider) might catch. It should be related to the first word but not the main topic.\n\n" +
                        "Provide the response as:\nInsider word: [first word]\nOutsider word: [second word]";
        */
        string promptText = "Generate 500 sets of two loosely connected words suitable for a 14-year-old." +
            "- The first word should be one that a group of people (insiders) might be discussing in everyday life, not too focused on school subjects.\n" +
            "- The second word is what someone listening nearby (an outsider) might catch. It should be related to the first word but not the main topic.\n\n" +
            "Make sure the words in the response have the same casing, starting with a Capital.\n" +
            "Respond in Dutch by picking 1 set at random in the following format:\n[first word],[second word]";

        var jsonBody = new
        
        {
          model="gpt-3.5-turbo",
          messages = new [] {
              new { role = "system", content = new [] { new { type = "text", text = promptText } } }
              },
          temperature=1,
          max_tokens=2048,
          top_p=0.8,
          frequency_penalty=0,
          presence_penalty=0,
          response_format= new { type = "text" }
        };
        /*
        {
            model = "gpt-3.5-turbo",
            prompt = promptText,
            max_tokens = 20,
            temperature = 0.6
        }; */

        string jsonBodyString = JsonConvert.SerializeObject(jsonBody);

        using (UnityWebRequest request = new UnityWebRequest(OPENAI_URL, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBodyString);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + OPENAI_API_KEY);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseText = request.downloadHandler.text;
                Debug.Log("response: " + responseText);
                var responseJson = JsonConvert.DeserializeObject<OpenAIResponse>(responseText);
                string generatedText = responseJson.choices[0].message.content.Trim();
                Debug.Log("Generated words: " + generatedText);
                BuzzWordsGenerated?.Invoke();
            }
            else
            {
                Debug.LogError("Request failed: " + request.error);
                BuzzWordsGenerated?.Invoke();
            }
        }
    }

}

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Choice
{
    public int index;
    public Message message;
    public object logprobs;
    public string finish_reason;
}

public class CompletionTokensDetails
{
    public int reasoning_tokens;
    public int audio_tokens;
    public int accepted_prediction_tokens;
    public int rejected_prediction_tokens;
}

public class Message
{
    public string role;
    public string content;
    public object refusal;
}

public class PromptTokensDetails
{
    public int cached_tokens;
    public int audio_tokens;
}

public class OpenAIResponse
{
    public string id;
    public string @object;
    public int created;
    public string model;
    public List<Choice> choices;
    public Usage usage;
    public object system_fingerprint;
}

public class Usage
{
    public int prompt_tokens;
    public int completion_tokens;
    public int total_tokens;
    public PromptTokensDetails prompt_tokens_details;
    public CompletionTokensDetails completion_tokens_details;
}

