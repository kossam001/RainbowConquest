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

        data.currentColour = GameManager.Instance.GetColour();
        
        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            mesh.material = data.currentColour;
        }
    }

    public void ChangeColour(Material material)
    {
        data.currentColour = material;
    }
}
