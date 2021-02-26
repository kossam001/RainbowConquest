using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public float force = 1000.0f;

    public void Shoot()
    {
        GameObject bullet = BulletManager.Instance.GetBullet();
        bullet.SetActive(true);

        bullet.transform.position = bulletSpawnPoint.position;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
    }
}
