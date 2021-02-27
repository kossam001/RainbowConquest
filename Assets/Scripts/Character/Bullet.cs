using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float duration;

    private void OnEnable()
    {
        StartCoroutine(Despawn());
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(duration);
        BulletManager.Instance.ReturnBullet(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            UpdateHealth(other.gameObject, GetComponent<MeshRenderer>().material);
        }
    }

    private void UpdateHealth(GameObject character, Material material)
    {
        CharacterData data = character.GetComponent<CharacterData>();
        List<Color> colourKeys = new List<Color>(data.colourHealth.Keys);
        float maxValue = 0.0f;
        Color newColour = Color.black;

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
                newColour = colour;
            }
        }

        if (newColour != data.currentColour.color)
        {
            character.GetComponent<ColourChange>().ChangeColour(material);
        }
    }
}
