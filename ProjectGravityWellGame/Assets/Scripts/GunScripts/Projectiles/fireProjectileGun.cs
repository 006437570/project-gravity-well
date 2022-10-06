using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireProjectileGun : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;

    void Update()
    {
        //melee when not holding gun
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    // Spawns bullter at firePoint
    public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

}