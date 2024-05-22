using UnityEngine;

public class SkillBehaviour : StateMachineBehaviour
{
    private PlayerStats _playerStats;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        if (_playerStats == null)
            _playerStats = animator.GetComponentInParent<PlayerStats>();
        _playerStats.UseMP(UtilTool.BaseStats.SkillManaCost);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerStats.CurHP += _playerStats.CurAtkDmg * 2;
    }
}
