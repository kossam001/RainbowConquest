using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChange : MonoBehaviour
{
    [SerializeField] public List<SkinnedMeshRenderer> meshes;
    private CharacterData data;

    private void Start()
    {
        data = GetComponent<CharacterData>();

        Material colour = GameManager.Instance.InitColour(gameObject, data);

        data.currentColour = colour;

        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            mesh.material = data.currentColour;
        }
    }

    public void ChangeColour(Material material)
    {
        if (data.currentColour.name == material.name) return;

        GameManager.Instance.ChangeTeams(data.currentColour, material, data, gameObject);

        data.currentColour = material;

        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            mesh.material = data.currentColour;
        }
    }
}
