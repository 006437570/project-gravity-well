using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // variables for setting up the game over screen
    private bool isGameOver;

    public float timeToEnd = 5;
    private int i = 0;

    [SerializeField]
    private int maxScore; // meant to keep score
    public GameObject InputManager;
    public playerSpawnManager PlayerSpawnManager; // for setting up the player spawns

    private GameObject winner;
    public GameObject gameOverText;

    // On awake set the game over to false as to avoid ending the game immediately
    // setup the player spawns using the spawn manager
    private void Awake()
    {
        isGameOver = false;

        PlayerSpawnManager = InputManager.GetComponent<playerSpawnManager>();
    }

    // per very update check to see if the game is over
    private void Update()
    {
        if (Input.GetKeyDown("escape")) // allows users to quit the game prematurely
        {
            Debug.Log("Test");
            SceneManager.LoadScene("Menu");
        }
        for (int i = 0; i < PlayerSpawnManager.numPlayers; i++) // Check the players in the array to see who won
        {
            if (PlayerSpawnManager.players[i].GetComponent<PlayerHealth>().killCounter >= maxScore) // checks the kill counter to see which player wins
            {
                isGameOver = true; // set the game over to true
                winner = PlayerSpawnManager.players[i]; // winner is called out based on their position in the array
            }
        }
        GameOver();
    }

    // Function to end the game
    private void GameOver()
    {
        if (isGameOver) // check if the game is over
        {
            if (i < 1) // if i is less than 1 then launch the winner screen
            {
                var go = Instantiate(gameOverText, transform.position, Quaternion.identity);
                go.GetComponent<TextMesh>().text = "Player " + winner.GetComponent<PlayerHealth>().playerID + " wins!";
                i++;
            }
            timeToEnd -= Time.deltaTime;
            if (timeToEnd <= 0) // load the Menu after the game time is up
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}