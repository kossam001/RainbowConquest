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
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero; // Reusing ball reuses velocity, needs to be zeroed out
        bullet.GetComponent<Bullet>().owner = gameObject;
        bullet.SetActive(true);

        bullet.GetComponent<MeshRenderer>().material.color = Gameplay.Instance.GetTeamToColour(data.currentTeam);
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
    }
}
