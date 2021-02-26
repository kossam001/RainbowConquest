using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [SerializeField] public List<Material> colours;
    public Dictionary<Material, List<GameObject>> teams;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        teams = new Dictionary<Material, List<GameObject>>();
        
        for (int i = 0; i < 3; i++)
        {
            teams.Add(colours[i], new List<GameObject>());
        }
    }

    public Material InitColour(GameObject character)
    {
        Material randColour = colours[Random.Range(0, colours.Count)];
        teams[randColour].Add(character);

        return randColour;
    }

    public List<GameObject> GetEnemies(Material colour)
    {
        List<GameObject> enemies = new List<GameObject>();

        foreach (Material mat in colours)
        {
            if (mat.name != colour.name)
            {
                enemies.AddRange(teams[mat]);
            }
        }

        return enemies;
    }
}
