using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    [Header("Main Level")]
    public Image teamLabel;
    public GameObject pauseMenu;

    [Header("Stage Select")]
    public TMP_InputField redInput;
    public TMP_InputField greenInput;
    public TMP_InputField blueInput;
    public TMP_Dropdown playerTeamInput;

    [Header("Game Over")]
    public Image background;
    public TMP_Text result;

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

        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            SetGameOver();
        }
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void SetTeamColour(Color colour)
    {
        teamLabel.color = colour;
    }

    public void StartGame(string name)
    {
        GameManager.Instance.redTeamSize = int.Parse(redInput.text);
        GameManager.Instance.greenTeamSize = int.Parse(greenInput.text);
        GameManager.Instance.blueTeamSize = int.Parse(blueInput.text);
        GameManager.Instance.playerTeam = (TeamColour)playerTeamInput.value;

        SceneManager.LoadScene(name);
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
    }

    private void SetGameOver()
    {
        Color backgroundColour;

        switch (GameManager.Instance.playerTeam)
        {
            case TeamColour.RED:
                backgroundColour = Color.red;
                break;
            case TeamColour.GREEN:
                backgroundColour = Color.green;
                break;
            default:
                backgroundColour = Color.blue;
                break;
        }

        background.color = backgroundColour;

        if (GameManager.Instance.win)
            result.text = "YOU WIN";

        else
            result.text = "YOU LOSE";
    }
}
