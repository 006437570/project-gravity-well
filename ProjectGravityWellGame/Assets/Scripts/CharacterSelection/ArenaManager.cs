using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArenaManager : MonoBehaviour
{
    private bool roundOver;
    public List<Transform> spawnPoints = new List<Transform>();

    public TMP_Text playerWinText;
    public GameObject winBar, roundCompleteText;

    public GameObject[] Weapons;
    public float timeBetweenSpawns;
    private float gunCounter;

    // Start is called before the first frame update
    void Start()
    {
        foreach(PlayerController player in GameManager.instance.activePlayers)
        {
            int randomPoint = Random.Range(0, spawnPoints.Count);
            player.transform.position = spawnPoints[randomPoint].position;
            if(GameManager.instance.activePlayers.Count <= spawnPoints.Count)
            {
                spawnPoints.RemoveAt(randomPoint);
            }
        }
        GameManager.instance.ActivatePlayers();

        gunCounter = timeBetweenSpawns * Random.Range(.75f, 1.25f);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.gameMode == 0)
        {
            if(GameManager.instance.CheckActivePlayers() == 1 && !roundOver)
            {
                roundOver = true;
                StartCoroutine(EndRoundCo());
            }
        }
        if(GameManager.instance.gameMode == 1)
        {
            if(GameManager.instance.CheckScore_Deathmatch() == GameManager.instance.maxScore)
            {
                StartCoroutine(EndRoundCo());
            }
        }
        if(GameManager.instance.gameMode == 2)
        {
            if(GameManager.instance.CheckActivePlayers() == 1 && !roundOver)
            {
                roundOver = true;
                StartCoroutine(EndRoundCo());
            }
        }

        if(gunCounter > 0)
        {
            gunCounter -= Time.deltaTime;
            if(gunCounter <= 0)
            {
                int randomPoint = Random.Range(0, spawnPoints.Count);
                Instantiate(Weapons[Random.Range(0, Weapons.Length)], spawnPoints[randomPoint].position, spawnPoints[randomPoint].rotation);
                gunCounter = timeBetweenSpawns * Random.Range(.75f, 1.25f);
            }
        }
    }

    IEnumerator EndRoundCo()
    {
        winBar.SetActive(true); // set these two UI elements to true;
        roundCompleteText.SetActive(true);
        playerWinText.gameObject.SetActive(true);
        playerWinText.text = "Player " + (GameManager.instance.lastPlayerNumber + 1) + " wins!";

        GameManager.instance.AddRoundWin();

        yield return new WaitForSeconds(2f); // Stop doing stuff for 2 seconds

        GameManager.instance.GoToNextArena();
    }
}
