using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase", menuName = "AITreeNodes/Chase")]
public class ChaseTarget : TreeNode
{
    public override bool PerformCheck()
    {
        if (brain.activeTarget == null) return false;

        Vector3 targetPosition = brain.activeTarget.transform.position;
        Vector3 selfPosition = brain.character.transform.position;

        float distance = Vector3.Distance(targetPosition, selfPosition);
        Debug.Log(distance);

        if (distance > brain.combatRange)
        {
            brain.agent.isStopped = false;
            brain.moveDestination = Vector3.Scale(targetPosition, new Vector3(1.0f, 0.0f, 1.0f));
        }
        else
        {
            brain.agent.isStopped = true;
        }

        return true;
    }

    public override bool Run()
    {
        if (!PerformCheck()) return false;

        return base.Run();
    }
}
