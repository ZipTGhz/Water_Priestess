using UnityEngine;

public class SpecialAttackBehaviour : StateMachineBehaviour
{
    private PlayerCombat _playerCombat;
    private IMPUser _mpUser;
    private bool _hasActionExecuted;
    private readonly float _endFirst = 60f / 155f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        if (_playerCombat == null)
        {
            _playerCombat = animator.GetComponent<PlayerCombat>();
            _mpUser = animator.GetComponentInParent<IMPUser>();
        }
        _mpUser.UseMP(UtilTool.BaseStats.SpecialAtkManaCost);

        _playerCombat.ShowSpecialAttackFirst = true;
        _playerCombat.ShowSpecialAttackSecond = true;
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
            _playerCombat.ShowSpecialAttackFirst = false;
            _hasActionExecuted = true; // Ensure the action is executed only once
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerCombat.ShowSpecialAttackSecond = false;
        _hasActionExecuted = false;
    }
}
