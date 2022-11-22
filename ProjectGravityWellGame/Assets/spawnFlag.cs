using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnFlag : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private GameObject flag;

    [SerializeField]
    private Transform flagSpawn;

    void Start()
    {
        if(gameController.gameMode == 2)
        {
            flag.SetActive(true);
            //GameObject flag = Instantiate(flagPrefab, flagSpawn.position, flagSpawn.rotation);
            flag.GetComponent<flagRespawn>().sp = flagSpawn;
            //flag.GetComponent<flagRespawn>().psm
        }
    }
}
