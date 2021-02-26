using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase", menuName = "AITreeNodes/Chase")]
public class ChaseTarget : TreeNode
{
    public override bool PerformCheck()
    {
        Vector3 targetPosition = brain.activeTarget.transform.position;
        Vector3 selfPosition = brain.character.transform.position;

        if (Vector3.Distance(targetPosition, selfPosition) > brain.combatRange)
        {
            brain.moveDestination = Vector3.Scale(targetPosition, new Vector3(1.0f, 0.0f, 1.0f));
        }

        return true;
    }

    public override bool Run()
    {
        if (!PerformCheck()) return false;

        brain.moveDestination = brain.enemyTargets[Random.Range(0, brain.enemyTargets.Count - 1)].transform.position;

        return base.Run();
    }
}
