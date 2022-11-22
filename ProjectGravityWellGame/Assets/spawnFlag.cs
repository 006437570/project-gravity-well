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
            //GameObject flag = flagPrefab;
            Instantiate(flag, flagSpawn.position, flagSpawn.rotation);
        }
    }
}
