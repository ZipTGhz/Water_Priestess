using UnityEngine;

public class SpecialAttackBehaviour : StateMachineBehaviour
{
    private bool _hasActionExecuted;
    private readonly float _endFirst = 60f / 155f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        PlayerCombat.Instance.ShowSpecialAttackFirst = true;
        PlayerCombat.Instance.ShowSpecialAttackSecond = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        if (!_hasActionExecuted && stateInfo.normalizedTime >= _endFirst)
        {
            PlayerCombat.Instance.ShowSpecialAttackFirst = false;
            _hasActionExecuted = true; // Ensure the action is executed only once
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerCombat.Instance.ShowSpecialAttackSecond = false;
        _hasActionExecuted = false;
    }
}
