using UnityEngine;

public class NoMovementBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    // override public void OnStateEnter(
    //     Animator animator,
    //     AnimatorStateInfo stateInfo,
    //     int layerIndex
    // ) { }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        PlayerInput.Instance.Dir = Vector2.zero;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerInput.Instance.Dir = PlayerInput.Instance.DirInput;
    }
}
