using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using Globals;

namespace Assets.Scripts.Classes.Claude
{
    internal class ClaudeGenerator
    {
        // API Configuration
        private const string API_ENDPOINT = "https://api.anthropic.com/v1/messages";

        public static IEnumerator CallClaudeApi()
        {
            string promptText = "Generate 500 sets of two loosely connected words suitable for a 14-year-old." +
                "- The first word should be one that a group of people (insiders) might be discussing in everyday life, not too focused on school subjects.\n" +
                "- The second word is what someone listening nearby (an outsider) might catch. It should be related to the first word but not the main topic.\n\n" +
                "Make sure the words in the response have the same casing, starting with a Capital.\n" +
                "Respond in Dutch by picking 1 set at random in the following format:\n[first word],[second word]";

            // Construct the request payload
            string jsonPayload = JsonUtility.ToJson(new ClaudeRequestBody
            {
                model = "claude-3-5-sonnet-20241022",
                max_tokens = 1024,
                messages = new[]
                {
                    new Message
                    {
                        role = "user",
                        content = promptText
                    }
                }
            });

            //Debug.Log("apikey: " + PlayerPrefs.GetString("ClaudeApiKey"));
            // Create the web request
            using (UnityWebRequest request = new UnityWebRequest(API_ENDPOINT, "POST"))
            {
                // Set request headers
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("x-api-key", PlayerPrefs.GetString("ClaudeApiKey"));
                request.SetRequestHeader("Anthropic-Version", "2023-06-01");

                // Convert payload to byte array
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();

                // Send the request
                yield return request.SendWebRequest();

                // Check for errors
                if (request.result == UnityWebRequest.Result.ConnectionError ||
                    request.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError($"Claude API Error: {request.error}");
                    Debug.LogError($"Claude API Error: {request.result}");
                    WordSetGenerator.GenerateBuzWordsSync("Vaste lijst, Claude API error");
                }
                else
                {
                    // Parse the response
                    try
                    {
                        string responseJson = request.downloadHandler.text;
                        Debug.Log("responseJson: " + responseJson);
                        ClaudeResponseBody responseBody = JsonUtility.FromJson<ClaudeResponseBody>(responseJson);

                        // Extract the text response from the first content block
                        string responseText = responseBody.content[0].text;
                        Debug.Log("response: " + responseText);
                        string[] words = responseText.Split(',');
                        if (words.Length == 2)
                        {
                            GameManager.SetBuzzWords(new WordSet { InsiderWord = words[0], OutsiderWord = words[1] }, "Claude");
                            WordSetGenerator.GeneratorReady();
                        }
                        else
                        {
                            WordSetGenerator.GenerateBuzWordsSync("Vaste lijst, Claude invalid response");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Claude API Parse Error: {ex.Message}");
                        WordSetGenerator.GenerateBuzWordsSync("Vaste lijst, Claude Parse error");
                    }
                }
            }
        }

        // Callback method to handle the response
        private void HandleClaudeResponse(string response)
        {
            if (response != null)
            {
                Debug.Log($"Claude API Response: {response}");
            }
            else
            {
                Debug.LogError("Failed to get response from Claude API");
            }
        }

        // Data structures to match Claude API v1/messages endpoint
        [Serializable]
        private class ClaudeRequestBody
        {
            public string model;
            public Message[] messages;
            public int max_tokens;
        }

        [Serializable]
        private class Message
        {
            public string role;
            public string content;
        }

        [Serializable]
        private class ClaudeResponseBody
        {
            public ContentBlock[] content;
        }

        [Serializable]
        private class ContentBlock
        {
            public string type;
            public string text;
        }
    }

}
