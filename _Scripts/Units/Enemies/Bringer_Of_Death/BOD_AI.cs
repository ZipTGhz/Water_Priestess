using UnityEngine;

public class BOD_AI : EnemyAI
{
    protected override void LoadDefaultValues()
    {
        base.LoadDefaultValues();
        minDistanceToTarget = 1f;
    }
}
