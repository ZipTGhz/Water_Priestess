using UnityEngine;

public class AirAttackBehaviour : StateMachineBehaviour
{
    private float _lastGravityScale;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        _lastGravityScale = PlayerInput.Instance.GetGravityScale;

        PlayerInput.Instance.SetGravityScale(0);
        PlayerInput.Instance.SetVelocity(new Vector2(0, -PlayerInput.Instance.AirAttackFallSpeed));

        PlayerCombat.Instance.ShowAirAttackHitBox = true;
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
        PlayerInput.Instance.SetGravityScale(_lastGravityScale);

        PlayerCombat.Instance.ShowAirAttackHitBox = false;
    }
}
