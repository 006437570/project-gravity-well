using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireProjectileGun : MonoBehaviour
{
    // Rework this script

    [SerializeField]
    public Transform firepoint;
    [SerializeField]
    public GameObject bullet;

    public void Shoot()
    {
        Instantiate(bullet, firepoint.position, firepoint.rotation);
        Debug.Log("Firing");
    }

}