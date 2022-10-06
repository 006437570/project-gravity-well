using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapManager : MonoBehaviour
{
    public static mapManager instance;

    // The spawns points that are available on the map
    public Transform playerSP1;
    public Transform playerSP2;
    public Transform playerSP3;
    public Transform playerSP4;
    
    public GameObject playerPrefab;

    // Will make this easy to reference by other scripts
    private void Awake()
    {
        instance = this;
    }

    // Creates new player instance at a spawn point
    public void playerRespawn(Transform spawnPoint)
    {
        Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
    }

    public Transform randomRespawn(Transform sp1, Transform sp2, Transform sp3, Transform sp4)
    {   
        // Generates a random number 
        int rndNum = Random.Range(0, 4);
        
        // Array storing each of the available spawn points
        Transform[] spawnPoints = {sp1, sp2, sp3, sp4};

        // Sets the random spawn point to what was randomly selected
        //Debug.Log("spawn at " + rndNum);
        Transform rsp = spawnPoints[rndNum];
        return rsp;
    }
}
