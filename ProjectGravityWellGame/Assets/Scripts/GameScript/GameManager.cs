using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int MaxPlayers;
    public List<PlayerController> activePlayers = new List<PlayerController>();

    public string[] all_levels;
    private List<string> levelOrder = new List<string>();

    [HideInInspector] public int lastPlayerNumber;

    public int pointsToWin;
    private List<int> roundWins = new List<int>();

    private bool gameWon;

    public string winLevel;

    private void Awake()
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

    public void GoToNextArena()
    {
        if(!gameWon)
        {
            if(levelOrder.Count == 0)
            {
                List<string> allLevelList = new List<string>();
                allLevelList.AddRange(all_levels);
                for(int i = 0; i < all_levels.Length; i++)
                {
                    int selected = Random.Range(0, allLevelList.Count);
                    levelOrder.Add(allLevelList[selected]);
                    allLevelList.RemoveAt(selected);
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
