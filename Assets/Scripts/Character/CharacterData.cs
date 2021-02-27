using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public int id;
    public int health = 100;
    public bool isAttacking = false;
    public Color currentColour;
    public TeamColour currentTeam;

    public Dictionary<Color, float> colourHealth;
}
