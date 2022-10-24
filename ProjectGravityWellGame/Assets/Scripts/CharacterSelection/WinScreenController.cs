using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreenController : MonoBehaviour
{
    public TMP_Text WinnerText;
    
    public Image playerImage;

    public string mainMenuScene, charSelectScene;
    // Start is called before the first frame update
    void Start()
    {
        WinnerText.text = "Player " + (GameManager.instance.lastPlayerNumber + 1) + " Wins the Game!";
        playerImage.sprite = GameManager.instance.activePlayers[GameManager.instance.lastPlayerNumber].GetComponent<SpriteRenderer>().sprite;
    }

    public void PlayAgain()
    {
        ClearGame();
        SceneManager.LoadScene(charSelectScene);
    }

    public void MainMenu()
    {
        ClearGame();
        SceneManager.LoadScene(mainMenuScene);
    }

    public void ClearGame()
    {
        foreach(PlayerController player in GameManager.instance.activePlayers)
        {
            Destroy(player.gameObject);
        }
        Destroy(GameManager.instance.gameObject);
        GameManager.instance = null;
    }

}
