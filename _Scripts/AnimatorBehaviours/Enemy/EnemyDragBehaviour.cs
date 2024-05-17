using UnityEngine;

public class EnemyDragBehaviour : StateMachineBehaviour
{
    private EnemyController _enemyController;
    private float _lastDrag;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        if (_enemyController == null)
            _enemyController = animator.GetComponentInParent<EnemyController>();
        _enemyController.AI.DetectPlayerWhenHurt();
        _lastDrag = _enemyController.Rb.drag;
        _enemyController.Rb.drag = 1000000;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    ) { }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemyController.Rb.drag = _lastDrag;
    }
}
