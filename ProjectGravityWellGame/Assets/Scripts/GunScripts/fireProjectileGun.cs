using UnityEngine;

public class fireProjectileGun : MonoBehaviour
{
    // Rework this script

    [SerializeField]
    public Transform firepoint;
    [SerializeField]
    public GameObject bullet;

    public void Shoot(GameObject player)
    {
        Instantiate(bullet, firepoint.position, firepoint.rotation);
        Debug.Log(player.GetComponent<PlayerHealth>().playerID);
    }
}