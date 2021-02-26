using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float duration;

    private void OnEnable()
    {
        StartCoroutine()
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(duration);

    }
}
