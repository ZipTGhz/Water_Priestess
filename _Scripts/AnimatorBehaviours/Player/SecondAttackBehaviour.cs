using UnityEngine;

public class SecondAttackBehaviour : StateMachineBehaviour
{
    private PlayerCombat _playerCombat;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        if (_playerCombat == null)
            _playerCombat = animator.GetComponent<PlayerCombat>();
        _playerCombat.ShowSecondAttackHitBox = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerCombat.ShowSecondAttackHitBox = false;
    }
}
