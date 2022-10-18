using UnityEngine;

public class respawnManager : MonoBehaviour
{
    public static respawnManager instance; // Will make this easy to reference by other scripts

    [SerializeField]
    public Transform pSP1, pSP2, pSP3, pSP4; // The spawns points that are available on the map

    private void Awake()
    {
        instance = this;
    }

    //Moves player to a spawn point
    public void respawnAt(Transform spawnPoint, GameObject player)
    {
        player.transform.position = spawnPoint.position; 
    }

    //Gets a random respawn point for player to go to
    public Transform randomRespawn(Transform sp1, Transform sp2, Transform sp3, Transform sp4)
    {
        Transform[] spawnPoints = {sp1, sp2, sp3, sp4}; //Array storing each of the available spawn points

        int rndNum = Random.Range(0, spawnPoints.Length); //Generates a random number 

        Transform rsp = spawnPoints[rndNum]; // Sets the random spawn point to what was randomly selected
        return rsp;
    }
}
