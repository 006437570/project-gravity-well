using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private bool isGameOver;

    public float timeToEnd = 5;
    private int i = 0;

    [SerializeField]
    private int maxScore;
    public GameObject InputManager;
    public playerSpawnManager PlayerSpawnManager;

    private GameObject winner;
    public GameObject gameOverText;

    private void Awake()
    {
        isGameOver = false;

        PlayerSpawnManager = InputManager.GetComponent<playerSpawnManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("Test");
            SceneManager.LoadScene("Menu");
        }
        for (int i = 0; i < PlayerSpawnManager.numPlayers; i++)
        {
            if (PlayerSpawnManager.players[i].GetComponent<PlayerHealth>().killCounter >= maxScore)
            {
                isGameOver = true;
                winner = PlayerSpawnManager.players[i];
            }
        }
        GameOver();
    }

    private void GameOver()
    {
        if (isGameOver)
        {
            if (i < 1)
            {
                var go = Instantiate(gameOverText, transform.position, Quaternion.identity);
                go.GetComponent<TextMesh>().text = "Player " + winner.GetComponent<PlayerHealth>().playerID + " wins!";
                i++;
            }
            timeToEnd -= Time.deltaTime;
            if (timeToEnd <= 0)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}