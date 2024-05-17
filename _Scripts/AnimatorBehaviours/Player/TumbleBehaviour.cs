using UnityEngine;

public class TumbleBehaviour : StateMachineBehaviour
{
    private PlayerController _playerController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        if (_playerController == null)
            _playerController = animator.GetComponentInParent<PlayerController>();

        PlayerInput.Instance.IsTumbleInProgress = true;

        _playerController.Rb.velocity = new Vector2(
            _playerController.GFX.right.x * PlayerInput.Instance.TumbleForce,
            0
        );
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerInput.Instance.IsTumbleInProgress = false;
    }
}
