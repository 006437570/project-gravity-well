using UnityEngine;

public class fireProjectileGun : MonoBehaviour
{
    // Rework this script

    public Transform firepoint; //point from gun that bullet is shot from
    public GameObject bulletPrefab; //bullet prefab that will be shot

    public int magazineSize;
    public int ammo;

    void Start()
    {
        ammo = magazineSize;
    }

    public void Shoot(GameObject playerAttacker)
    {   
        if (ammo > 0)
        {
            bulletPrefab.GetComponent<bulletProjectile>().playerAttacker = playerAttacker;
            Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
            ammo--;
        }
        else
        {
            //audio
        }
    }
}