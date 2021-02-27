using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }

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

    public Image teamLabel;

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void SetTeamColour(Color colour)
    {
        teamLabel.color = colour;
    }
}
