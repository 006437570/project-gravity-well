using UnityEngine;

public class fireProjectileGun : MonoBehaviour
{
    // Rework this script

    [SerializeField] private AudioSource BlickySoundEffect;
    [SerializeField] private AudioSource OutOfAmmoSoundEffect;

    public Transform firepoint; //point from gun that bullet is shot from
    public GameObject bulletPrefab; //bullet prefab that will be shot

    [SerializeField] public int magazine;
    public int ammo;

    void Start()
    {
        ammo = magazine; // initializes the ammo to the magazine amount
    }


    public void Shoot(GameObject playerAttacker)
    {
        if(ammo > 0) // checks if ammo is greater than 0 if so then shoot
        {
            BlickySoundEffect.Play();
            GameObject bullet = bulletPrefab;
            bullet.GetComponent<bulletProjectile>().playerAttacker = playerAttacker;
            Instantiate(bullet, firepoint.position, firepoint.rotation);
            ammo--; // decreases ammo by 1 after shooting
        }
        else // if there is no more ammo in the gun then play a sound to indicate that there are no more bullets
        {
            OutOfAmmoSoundEffect.Play();
        }
    }
}