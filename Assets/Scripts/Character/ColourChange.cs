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

        Color colour = Gameplay.Instance.InitColour(gameObject, data);

        data.currentColour = colour;

        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            mesh.material.color = data.currentColour;
        }
    }

    public void ChangeColour(TeamColour newTeam)
    {
        if (data.currentTeam != newTeam)
            Gameplay.Instance.ChangeTeams(data.currentTeam, newTeam, data, gameObject);

        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            mesh.material.color = new Color(data.colourHealth[Color.red], data.colourHealth[Color.green], data.colourHealth[Color.blue]);
        }
    }
}
