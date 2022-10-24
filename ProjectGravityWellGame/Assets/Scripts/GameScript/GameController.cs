using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private bool isGameOver;

    [SerializeField]
    private int maxScore;
    //public GameObject gameOverUI; 
    public GameObject InputManager;
    public playerSpawnManager PlayerSpawnManager;

    private void Awake()
    {
        isGameOver = false;

        PlayerSpawnManager = InputManager.GetComponent<playerSpawnManager>();
    }

    private void Update()
    {
        for (int i = 0; i < PlayerSpawnManager.numPlayers; i++)
        {
            if (PlayerSpawnManager.players[i].GetComponent<PlayerHealth>().killCounter >= maxScore)
            {
                isGameOver = true;
            }
        }

        if (isGameOver)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene("map1");
    }
}
