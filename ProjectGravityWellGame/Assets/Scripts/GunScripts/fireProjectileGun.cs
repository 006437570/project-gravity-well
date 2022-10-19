using UnityEngine;

public class fireProjectileGun : MonoBehaviour
{
    // Rework this script

    public Transform firepoint; //point from gun that bullet is shot from
    public GameObject bulletPrefab; //bullet prefab that will be shot

    public void Shoot(GameObject playerAttacker)
    {   
        bulletPrefab.GetComponent<bulletProjectile>().playerAttacker = playerAttacker;
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
    }
}