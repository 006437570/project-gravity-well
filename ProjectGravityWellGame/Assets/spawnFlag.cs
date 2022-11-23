using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnFlag : MonoBehaviour
{
    [SerializeField]
    private GameObject flag;

    [SerializeField]
    private Transform flagSpawn;

    void Start()
    {
        flag.SetActive(true);
        flag.GetComponent<flagRespawn>().sp = flagSpawn;
    }
}
