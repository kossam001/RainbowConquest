using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TeamColour
{
    RED,
    GREEN,
    BLUE
}

public class Gameplay : MonoBehaviour
{
    private static Gameplay instance;
    public static Gameplay Instance { get { return instance; } }

    [Header("Initialization")]
    [SerializeField] public List<Material> colours;

    public TeamColour playerTeamColour;
    public GameObject player;

    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private int numCharacters;

    [SerializeField] private Transform spawnCenter;
    [SerializeField] private float spawnRadius;

    [Header("Score UI")]
    [SerializeField] private Slider redTeamScore;
    [SerializeField] private Slider greenTeamScore;
    [SerializeField] private Slider blueTeamScore;

    [Header("Health UI")]
    [SerializeField] private CharacterData playerData;
    [SerializeField] private Slider redHealth;
    [SerializeField] private Slider greenHealth;
    [SerializeField] private Slider blueHealth;

    public Dictionary<Color, Dictionary<int, GameObject>> teams;

    private int characterCount = 0;

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

        teams = new Dictionary<Color, Dictionary<int, GameObject>>();
        
        for (int i = 0; i < 3; i++)
        {
            teams.Add(colours[i].color, new Dictionary<int, GameObject>());
        }

        for (int i = 0; i < numCharacters; i++)
        {
            Vector3 spawnLocation = spawnCenter.position + new Vector3(Random.Range(-spawnRadius, spawnRadius), 0.0f, Random.Range(-spawnRadius, spawnRadius));

            GameObject spawnedCharacter = Instantiate(characterPrefab, spawnLocation, Quaternion.Euler(0.0f,0.0f, 0.0f));
            spawnedCharacter.transform.SetParent(spawnCenter);
        }

        //UIManager.Instance.SetTeamColour(player.GetComponent<CharacterData>().currentColour.color);
        //player.GetComponent<ColourChange>().ChangeColour(colours[(int)playerTeamColour]);
    }

    public Material InitColour(GameObject character, CharacterData data)
    {
        Material randColour = colours[Random.Range(0, colours.Count)];
        teams[randColour.color].Add(characterCount, character);

        data.id = characterCount;
        characterCount++;

        // Setup health
        data.colourHealth = new Dictionary<Color, float>();
        data.colourHealth.Add(Color.red, 0.0f);
        data.colourHealth.Add(Color.green, 0.0f);
        data.colourHealth.Add(Color.blue, 0.0f);
        data.colourHealth[randColour.color] = 1.0f;

        return randColour;
    }

    public List<GameObject> GetEnemies(Material colour)
    {
        List<GameObject> enemies = new List<GameObject>();

        foreach (Material mat in colours)
        {
            if (mat.color != colour.color)
            {
                enemies.AddRange(teams[mat.color].Values);
            }
        }

        return enemies;
    }

    public void ChangeTeams(Material oldColour, Material newColour, CharacterData data, GameObject character)
    {
        teams[oldColour.color].Remove(data.id);
        teams[newColour.color].Add(data.id, character);
    }

    private void Update()
    {
        UpdateScore();
        UpdateHealth();
    }

    private void UpdateScore()
    {
        int totalCharacters = numCharacters + 1; // +player

        float redTotal = (float)teams[colours[0].color].Count / (float)totalCharacters;
        float greenTotal = (float)teams[colours[1].color].Count / (float)(totalCharacters) + redTotal;
        float blueTotal = (float)teams[colours[2].color].Count / (float)(totalCharacters) + greenTotal;

        redTeamScore.value = redTotal;
        greenTeamScore.value = greenTotal;
        blueTeamScore.value = blueTotal;
    }

    private void UpdateHealth()
    {
        float redTotal = (float)playerData.colourHealth[colours[0].color] / 1.0f;
        float greenTotal = (float)playerData.colourHealth[colours[1].color] / 1.0f + redTotal;
        float blueTotal = (float)playerData.colourHealth[colours[2].color] / 1.0f + greenTotal;

        redHealth.value = redTotal;
        greenHealth.value = greenTotal;
        blueHealth.value = blueTotal;
    }
}
