using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fire", menuName = "AITreeNodes/Fire")]
public class Fire : TreeNode
{
    public float firerate;
    public float timer;

    public override bool Run()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = firerate;
            brain.character.GetComponent<Attack>().Shoot();
        }

        return base.Run();
    }
}
