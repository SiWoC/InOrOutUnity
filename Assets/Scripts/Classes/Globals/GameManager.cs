using System;
using System.Collections.Generic;
using UnityEngine;

namespace Globals
{

    public enum State
    {
        Rules,
        EnteringPlayers,
        GeneratingWords,
        ShowChosenWord,
        ToEnteringFirstWord,
        EnteringFirstWord,
        ToShowingFirstWords,
        ShowingFirstWords,
        ToEnteringSecondWord,
        EnteringSecondWord,
        ToShowingSecondWords,
        ShowingSecondWords,
        ToEnteringOutsider,
        EnteringOutsider,
        ToShowingOutsider,
        ShowingOutsider,
        EndOverviewRestart
    }

    public enum GeneratorType
    {
        File,
        OpenAI,
        Claude
    }

    public static class GameManager
    {

        public static event Action NextStateEvent;

        private static State currentState = State.Rules;
        private static GeneratorType generatorType = GeneratorType.File;
        private static List<GameObject> playerObjects;
        private static readonly System.Random getrandom = new System.Random();
        private static int currentIndex = -1;
        private static WordSet wordSet;
        private static string outsiderName;
        private static string wordsOrigin;
        private static List<string> playerNames = new List<string>();

        public static List<string> GetPlayerNames() {
            return playerNames;
        }

        internal static GeneratorType GetGeneratorType()
        {
            return generatorType;
        }

        internal static void SetGeneratorType(GeneratorType newGeneratorType)
        {
            generatorType = newGeneratorType;
        }

        public static WordSet GetWordSet() { 
            return wordSet;
        }
        
        public static string GetWordsOrigin()
        {
            return wordsOrigin;
        }

        public static void Initialize()
        {
            generatorType = (GeneratorType)PlayerPrefs.GetInt("GeneratorType",0);
            SetState(State.Rules);
        }

        public static State GetState()
        {
            return currentState;
        }

        internal static void SetState(State state)
        {
            currentState = state;
            if (currentState == State.Rules && PlayerPrefs.GetInt("ShowRulesAtStartup", 1) == 0) {
                NextState();
            }
            currentIndex = -1; // restarting player index for next singleplayerloop
            NextStateEvent?.Invoke();
        }

        internal static void NextState()
        {
            /* debugging skipping states 
            switch (currentState)
            {
                case State.ShowChosenWord:
                    currentState += 10;
                    break;
            }
            */
            currentState++;
            currentIndex = -1; // restarting player index for next singleplayerloop
            NextStateEvent?.Invoke();
        }

        public static void SetPlayers(List<GameObject> playerObjectsIn)
        {
            Debug.Log("GameManager SetPlayers" + playerObjectsIn.Count);
            playerObjects = playerObjectsIn;
        }

        public static List<GameObject> GetPlayerObjects()
        {
            return playerObjects;
        }

        internal static void SetBuzzWords(WordSet newWordset, string origin)
        {
            wordSet = newWordset;
            wordsOrigin = origin;

            int outsiderIndex = getrandom.Next(1,playerObjects.Count);
            int i = 1;
            foreach (GameObject playerInfoObject in playerObjects)
            {
                PlayerInfo playerInfo = playerInfoObject.GetComponent<PlayerInfo>();
                if (i == outsiderIndex)
                {
                    outsiderName = playerInfo.GetName();
                    Debug.Log("Outsider: " + outsiderIndex + " " + outsiderName);
                    playerInfo.SetBuzzWord(wordSet.OutsiderWord);
                }
                else
                {
                    playerInfo.SetBuzzWord(wordSet.InsiderWord);
                }
                i++;
            }
        }

        // called from the singlePlayerPanel to loop through the players
        internal static GameObject GetNextPlayer()
        {
            currentIndex++;
            if (currentIndex < playerObjects.Count)
            {
                return playerObjects[currentIndex];
            }
            currentIndex = -1;
            return null;
        }

        internal static string GetOutsiderName()
        {
            return outsiderName;
        }
    }
}