using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int MaxPlayers;

    [SerializeField] public int maxScore; // meant to keep score

    public int gameMode; //which game mode is going to be played

    public List<PlayerController> activePlayers = new List<PlayerController>();

    public string[] elimination_levels; // setup for elimination levels

    public string[] ctf_levels; // setup for capture the flag levels

    public string[] deathmatch_levels; // setup for deathmatch levels

    private List<string> levelOrder = new List<string>();

    [HideInInspector] public int lastPlayerNumber;

    public int pointsToWin;
    private List<int> roundWins = new List<int>();

    private bool gameWon;

    public string winLevel;

    private int numEliminated;

    private int score = 0;

    private void Awake() // on awake prevent the game manager from getting destroyed
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPlayer(PlayerController newPlayer)
    {
        if(activePlayers.Count < MaxPlayers)
        {
            activePlayers.Add(newPlayer);
        }
        else
        {
            Destroy(newPlayer.gameObject);
        }
    }

    public void ActivatePlayers()
    {
        foreach(PlayerController player in activePlayers)
        {
            player.gameObject.SetActive(true);
            player.GetComponent<PlayerHealth>().FillHealth();
        }
    }

    public int CheckActivePlayers()
    {
        int playerAliveCount = 0;
        for(int i = 0; i < activePlayers.Count; i++)
        {
            if(activePlayers[i].gameObject.activeInHierarchy)
            {
                playerAliveCount++;
                lastPlayerNumber = i;
            }
        }
        return playerAliveCount;
    }

    public int CheckScore_Deathmatch()
    {
        for(int i = 0; i < activePlayers.Count; i++)
        {
            if(activePlayers[i].gameObject.GetComponent<PlayerHealth>().killCounter >= maxScore)
            {
                lastPlayerNumber = i;
                gameWon = true;
            }
            score = activePlayers[i].gameObject.GetComponent<PlayerHealth>().killCounter;
        }
        return score;
    }

    public void GoToNextArena()
    {
        if(!gameWon)
        {
            if(levelOrder.Count == 0)
            {
                List<string> allLevelList = new List<string>();
                if(gameMode == 0)
                {
                    allLevelList.AddRange(elimination_levels);
                    for(int i = 0; i < elimination_levels.Length; i++)
                    {
                        int selected = Random.Range(0, allLevelList.Count);
                        levelOrder.Add(allLevelList[selected]);
                        allLevelList.RemoveAt(selected);
                    }
                }
                if(gameMode == 1)
                {
                    allLevelList.AddRange(deathmatch_levels);
                    for(int i = 0; i < deathmatch_levels.Length; i++)
                    {
                        int selected = Random.Range(0, allLevelList.Count);
                        levelOrder.Add(allLevelList[selected]);
                        allLevelList.RemoveAt(selected);
                    }
                }
                if(gameMode == 2)
                {
                    allLevelList.AddRange(ctf_levels);
                    for(int i = 0; i < ctf_levels.Length; i++)
                    {
                        int selected = Random.Range(0, allLevelList.Count);
                        levelOrder.Add(allLevelList[selected]);
                        allLevelList.RemoveAt(selected);
                    }
                }
            }
            string levelToLoad = levelOrder[0];
            levelOrder.RemoveAt(0);

            foreach(PlayerController player in activePlayers)
            {
                player.gameObject.SetActive(true);
                player.GetComponent<PlayerHealth>().FillHealth();
            }

            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            foreach(PlayerController player in activePlayers)
            {
                player.gameObject.SetActive(false);
                player.GetComponent<PlayerHealth>().FillHealth();
            }
            SceneManager.LoadScene(winLevel);
        }
    }

    public void StartFirstRound()
    {
        roundWins.Clear();
        foreach(PlayerController player in activePlayers)
        {
            roundWins.Add(0);
        }
        gameWon = false;
        GoToNextArena();
    }

    public void AddRoundWin()
    {
        if(gameMode == 0) // gamemode is elimination
        {
            if(CheckActivePlayers() == 1)
            {
                roundWins[lastPlayerNumber]++;
                if(roundWins[lastPlayerNumber] >= pointsToWin)
                {
                    gameWon = true;
                }
            }
        }
        if(gameMode == 1) // gamemode is deathmatch
        {
            CheckScore_Deathmatch();
        }
        if(gameMode == 2) // gamemode is ctf
        {
            if(CheckActivePlayers() == 1)
            {
                roundWins[lastPlayerNumber]++;
                if(roundWins[lastPlayerNumber] >= pointsToWin)
                {
                    gameWon = true;
                }
            }
        }
    }
}