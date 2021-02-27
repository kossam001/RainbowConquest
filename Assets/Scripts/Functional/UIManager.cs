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

    [Header("Stage Select")]
    public TMP_InputField redInput;
    public TMP_InputField greenInput;
    public TMP_InputField blueInput;
    public TMP_Dropdown playerTeamInput;

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
}
