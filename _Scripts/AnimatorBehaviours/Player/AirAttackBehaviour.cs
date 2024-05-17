using UnityEngine;

public class AirAttackBehaviour : StateMachineBehaviour
{
	private Rigidbody2D _rb;
	private PlayerCombat _playerCombat;
	
	private float _lastGravityScale;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(
		Animator animator,
		AnimatorStateInfo stateInfo,
		int layerIndex
	)
	{
		if (_rb == null)
			_rb = animator.GetComponentInParent<Rigidbody2D>();
		if (_playerCombat == null)
			_playerCombat = animator.GetComponent<PlayerCombat>();

		_lastGravityScale = _rb.gravityScale;
		_rb.gravityScale = 0;
		_rb.velocity = new Vector2(0, -PlayerInput.Instance.AirAttackFallSpeed);
		_playerCombat.ShowAirAttackHitBox = true;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	// override public void OnStateUpdate(
	//     Animator animator,
	//     AnimatorStateInfo stateInfo,
	//     int layerIndex
	// ) { }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_rb.gravityScale = _lastGravityScale;
		_playerCombat.ShowAirAttackHitBox = false;
	}
}
