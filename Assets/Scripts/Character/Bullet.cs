using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float duration;
    public GameObject owner;

    private void OnEnable()
    {
        StartCoroutine(Despawn());
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(duration);
        owner = null;
        BulletManager.Instance.ReturnBullet(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Character") && !ReferenceEquals(other.gameObject, owner))
        {
            UpdateHealth(other.gameObject, GetComponent<MeshRenderer>().material);
        }
    }

    private void UpdateHealth(GameObject character, Material material)
    {
        CharacterData data = character.GetComponent<CharacterData>();
        List<Color> colourKeys = new List<Color>(data.colourHealth.Keys);
        float maxValue = 0.0f;
        TeamColour newTeam = data.currentTeam;

        foreach (Color colour in colourKeys)
        {
            if (colour == material.color)
                data.colourHealth[colour] += 0.2f;

            else
                data.colourHealth[colour] -= 0.1f;

            data.colourHealth[colour] = Mathf.Clamp(data.colourHealth[colour], 0.0f, 1.0f);

            if (maxValue < data.colourHealth[colour])
            {
                maxValue = data.colourHealth[colour];
                newTeam = Gameplay.Instance.GetColourToTeam(colour);
            }
        }

        character.GetComponent<ColourChange>().ChangeColour(newTeam);
    }
}
