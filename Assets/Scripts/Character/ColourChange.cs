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

        ChangeColour(GameManager.Instance.InitColour(gameObject));
    }

    public void ChangeColour(Material material)
    {
        data.currentColour = material;

        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            mesh.material = data.currentColour;
        }
    }
}
