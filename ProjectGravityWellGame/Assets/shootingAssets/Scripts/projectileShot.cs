using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingScript : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("Shooting!");
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

}
