using System;
using System.Collections.Generic;
using UnityEngine;

namespace Globals
{

    public enum State
    {
        Rules,
        EnteringPlayers,
        Pauze,
        ShowChosenWord,
        EnteringFirstWord,
        ShowingFirstWords,
        EnteringSecondWord,
        ShowingSecondWords,
        EnteringOutsider,
        ShowingOutsider
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

        public static State currentState = State.Rules;
        public static GeneratorType generatorType = GeneratorType.File;

        private static List<GameObject> playerObjects;
        private static readonly System.Random getrandom = new System.Random();
        private static int currentIndex = -1;

        public static void Initialize()
        {
            // TODO set generatorType from preferences/settings
        }

        public static State GetState()
        {
            return currentState;
        }

        public static void SetPlayers(List<GameObject> playerObjectsIn)
        {
            playerObjects = playerObjectsIn;
        }

        public static List<GameObject> GetPlayerObjects()
        {
            return playerObjects;
        }

        public static void Back()
        {

        }

        public static void Forward(string sceneName)
        {

        }

        internal static void SetState(State state)
        {
            currentState = state;
        }

        internal static void SetBuzzWords(string insiderWord, string outsiderWord)
        {
            int outsiderIndex = getrandom.Next(1,playerObjects.Count);
            Debug.Log("Outsider: " + outsiderIndex);
            int i = 1;
            foreach (GameObject playerInfoObject in playerObjects)
            {
                PlayerInfo playerInfo = playerInfoObject.GetComponent<PlayerInfo>();
                if (i == outsiderIndex)
                    playerInfo.SetBuzzWord(outsiderWord);
                else
                    playerInfo.SetBuzzWord(insiderWord);
                i++;
            }
        }

        // called from the singlePlayerPanel to loop through the players
        internal static GameObject GetNextPlayer()
        {
            currentIndex++;
            Debug.Log("GettingPlayer " + currentIndex);
            if (currentIndex < playerObjects.Count)
            {
                return playerObjects[currentIndex];
            }
            currentIndex = -1;
            return null;
        }

        internal static void NextState()
        {
            currentState++;
            currentIndex = -1; // restartin player index for next singleplayerloop
            NextStateEvent?.Invoke();

        }
    }
}