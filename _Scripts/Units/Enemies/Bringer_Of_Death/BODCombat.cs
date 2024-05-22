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
        _commonAttackPoint = new Vector2(1.5f, 1f);
        _commonAttackSize = new Vector2(3.25f, 2f);
    }

    public void DoCommonAttack()
    {
        // USING RAYCAST
        Collider2D colliderInfo = Physics2D.OverlapBox(
            (Vector2)transform.position
                + (Vector2)(transform.right + transform.up) * _commonAttackPoint,
            _commonAttackSize,
            0f,
            _playerMask
        );

        if (colliderInfo == null)
            return;
        colliderInfo
            .GetComponent<IDamageable>()
            .TakeHP(_bodController.CurrentStats.CurAtkDmg);
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
