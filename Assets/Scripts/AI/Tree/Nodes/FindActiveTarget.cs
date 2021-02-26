using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FindActiveTarget", menuName = "AITreeNodes/FindActiveTarget")]
public class FindActiveTarget : TreeNode
{
    [Tooltip("How long to target this target")]
    public float duration;

    [Header("Debug")]
    public GameObject activeTarget;
    [SerializeField] private float timer;

    public override bool Run()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            brain.enemyTargets = GameManager.Instance.GetEnemies(brain.character.GetComponent<CharacterData>().currentColour);

            brain.activeTarget = brain.enemyTargets[Random.Range(0, brain.enemyTargets.Count)];
            activeTarget = brain.activeTarget;

            timer = duration;
        }

        return base.Run();
    }
}
