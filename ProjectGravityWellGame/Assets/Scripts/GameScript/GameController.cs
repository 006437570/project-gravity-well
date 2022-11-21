using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // variables for setting up the game over screen
    private bool isGameOver;

    public float matchOverCD = 5;
    private int i = 0;

    [SerializeField]
    private int maxScore; // meant to keep score

    public int gameMode; //which game mode is going to be played

    public GameObject InputManager;
    public playerSpawnManager PlayerSpawnManager; // for setting up the player spawns

    private GameObject winner;
    public GameObject gameOverText;

    private int numEliminated;

    // On awake set the game over to false as to avoid ending the game immediately
    // setup the player spawns using the spawn manager
    private void Awake()
    {
        isGameOver = false;

        PlayerSpawnManager = InputManager.GetComponent<playerSpawnManager>();

        numEliminated = 0;
    }

    // per very update check to see if the game is over
    private void Update()
    {
        if (Input.GetKeyDown("escape")) // allows users to quit the game prematurely
        {
            Debug.Log("Test");
            SceneManager.LoadScene("Menu");
        }
        if (gameMode == 0)
        {
            deathMatch();
        } else if (gameMode == 1)
        {
            eliminationMatch();
        } else if (gameMode == 2)
        {
            ctfMatch();
        }
        GameOver();
    }

    private void deathMatch()
    {
        for (int i = 0; i < PlayerSpawnManager.numPlayers; i++) // Check the players in the array to see who won
        {
            if (PlayerSpawnManager.players[i].GetComponent<PlayerHealth>().killCounter >= maxScore) // checks the kill counter to see which player wins
            {
                isGameOver = true; // set the game over to true
                winner = PlayerSpawnManager.players[i]; // winner is called out based on their position in the array
            }
        }
    }

    private void eliminationMatch()
    {
        for (int i = 0; i < PlayerSpawnManager.numPlayers; i++) // Check the players in the array to see who won
        {
            if (!PlayerSpawnManager.players[i].GetComponent<PlayerHealth>().isEliminated)
            {
                if (PlayerSpawnManager.players[i].GetComponent<PlayerHealth>().deathCounter >= maxScore)
                {
                    PlayerSpawnManager.players[i].GetComponent<PlayerHealth>().isEliminated = true;
                    numEliminated++;
                    Debug.Log("Player:" + PlayerSpawnManager.players[i].GetComponent<PlayerHealth>().playerID + " is eliminated");
                }
            }

            if (PlayerSpawnManager.numPlayers - numEliminated <= 1 && PlayerSpawnManager.numPlayers > 1)
            {
                isGameOver = true;
                winner = PlayerSpawnManager.players[i];
            }
        }
    }

    private void ctfMatch()
    {
        // create flag object and spawn it in middle of scene
        // player can pick up and drop flag
        // if player takes flag to original spawn point give player a point and disable interaction with flag
        // give player point
        // move flag in center
        // re-enable flag interaction

        // flag script has "weapon tag"
        // has shoot script like a gun (if player shoots it throws the flag)
        // on collisionEnter2D one of the set spawn points
            // if that player isn't in the game, then do nothing
            // give point to player that spawn belongs to
        for (int i = 0; i < PlayerSpawnManager.numPlayers; i++) // Check the players in the array to see if someone won
        {
            if (PlayerSpawnManager.players[i].GetComponent<PlayerHealth>().scoreCounter >= maxScore) // checks the score counter to see which player wins
            {
                isGameOver = true; // set the game over to true
                winner = PlayerSpawnManager.players[i]; // winner is called out based on their position in the array
            }
        }
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
            matchOverCD -= Time.deltaTime;
            if (matchOverCD <= 0) // load the Menu after the game time is up
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}