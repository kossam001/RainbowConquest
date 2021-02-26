using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    private CharacterData data;

    public float force = 1000.0f;

    private void Awake()
    {
        data = GetComponent<CharacterData>();
    }

    public void Shoot()
    {
        GameObject bullet = BulletManager.Instance.GetBullet();
        bullet.SetActive(true);

        bullet.GetComponent<MeshRenderer>().material = data.currentColour;
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
    }
}
