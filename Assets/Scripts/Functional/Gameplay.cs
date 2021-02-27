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

    public Dictionary<TeamColour, Dictionary<int, GameObject>> teams;

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

        teams = new Dictionary<TeamColour, Dictionary<int, GameObject>>();
        
        for (int i = 0; i < 3; i++)
        {
            teams.Add((TeamColour)i, new Dictionary<int, GameObject>());
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

    public Color InitColour(GameObject character, CharacterData data)
    {
        Material randColour = colours[Random.Range(0, colours.Count)];
        teams[GetColourToTeam(randColour.color)].Add(characterCount, character);

        data.id = characterCount;
        data.currentTeam = Gameplay.Instance.GetColourToTeam(randColour.color);
        characterCount++;

        // Setup health
        data.colourHealth = new Dictionary<Color, float>();
        data.colourHealth.Add(Color.red, 0.0f);
        data.colourHealth.Add(Color.green, 0.0f);
        data.colourHealth.Add(Color.blue, 0.0f);
        data.colourHealth[randColour.color] = 1.0f;

        return randColour.color;
    }

    public List<GameObject> GetEnemies(TeamColour colour)
    {
        List<GameObject> enemies = new List<GameObject>();
        List<TeamColour> teamKeys = new List<TeamColour>(teams.Keys);

        foreach (TeamColour col in teamKeys)
        {
            if (col != colour)
            {
                enemies.AddRange(teams[col].Values);
            }
        }

        return enemies;
    }

    public void ChangeTeams(TeamColour oldColour, TeamColour newColour, CharacterData data, GameObject character)
    {
        teams[oldColour].Remove(data.id);
        teams[newColour].Add(data.id, character);
        data.currentTeam = newColour;
    }

    private void Update()
    {
        UpdateScore();
        UpdateHealth();
    }

    private void UpdateScore()
    {
        int totalCharacters = numCharacters + 1; // +player

        float redTotal = (float)teams[TeamColour.RED].Count / (float)totalCharacters;
        float greenTotal = (float)teams[TeamColour.GREEN].Count / (float)(totalCharacters) + redTotal;
        float blueTotal = (float)teams[TeamColour.BLUE].Count / (float)(totalCharacters) + greenTotal;

        redTeamScore.value = redTotal;
        greenTeamScore.value = greenTotal;
        blueTeamScore.value = blueTotal;
    }

    private void UpdateHealth()
    {
        float redTotal = (float)playerData.colourHealth[colours[0].color] / 3.0f;
        float greenTotal = (float)playerData.colourHealth[colours[1].color] / 3.0f + redTotal;
        float blueTotal = (float)playerData.colourHealth[colours[2].color] / 3.0f + greenTotal;

        redHealth.value = redTotal;
        greenHealth.value = greenTotal;
        blueHealth.value = blueTotal;
    }

    public TeamColour GetColourToTeam(Color colour)
    {
        if (Mathf.Max(colour.r, colour.g, colour.b) % colour.r == 0)
            return TeamColour.RED;

        else if (Mathf.Max(colour.r, colour.g, colour.b) % colour.g == 0)
            return TeamColour.GREEN;

        else
            return TeamColour.BLUE;
    }

    public Color GetTeamToColour(TeamColour teamColour)
    {
        switch(teamColour)
        {
            case TeamColour.RED:
                return Color.red;
            case TeamColour.GREEN:
                return Color.green;
            default:
                return Color.blue;
        }
    }
}
