using UnityEngine;

public class BODCombat : CustomMonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField]
    private BODController _bodController;

    [SerializeField]
    private LayerMask _playerMask;

    [Header("COMMON ATTACK")]
    [SerializeField]
    private bool _showCommonAttack;

    [SerializeField]
    private Vector2 _commonAttackPoint;

    [SerializeField]
    private Vector2 _commonAttackSize;

    protected override void LoadComponents()
    {
        _bodController = GetComponentInParent<BODController>();
    }

    protected override void LoadDefaultValues()
    {
        _playerMask = 256;

        //COMMON ATTACK
        _commonAttackPoint = new Vector2(1.75f, 1f);
        _commonAttackSize = new Vector2(2.75f, 2f);
    }

    public void DoCommonAttack()
    {
        // USING RAYCAST
        Collider2D hitInfo = Physics2D.OverlapBox(
            (Vector2)transform.position
                + (Vector2)(transform.right + transform.up) * _commonAttackPoint,
            _commonAttackSize,
            0f,
            _playerMask
        );

        if (hitInfo == null)
            return;

        _bodController.DamageSender.Send(hitInfo, 1f);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            (Vector2)transform.position
                + (Vector2)(transform.right + transform.up) * _commonAttackPoint,
            _commonAttackSize
        );
    }
}
