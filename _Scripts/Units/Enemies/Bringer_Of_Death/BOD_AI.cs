using UnityEngine;

public class BOD_AI : EnemyAI
{
	protected override void LoadDefaultValues()
	{
		base.LoadDefaultValues();
		moveSpeed = 2f;
		minDistanceToTarget = 1f;
		attackCoolDown = 3f;
		observeCoolDown = 3f;
	}
}
