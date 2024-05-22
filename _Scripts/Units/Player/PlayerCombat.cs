using UnityEngine;

public class PlayerCombat : CustomMonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField]
    private PlayerController _playerController;

    [SerializeField]
    private LayerMask _enemyLayer;

    [Header("AIR ATTACK")]
    [SerializeField]
    private bool _showAirAttackHitBox;

    [SerializeField]
    private Vector2 _airAttackPoint;

    [SerializeField]
    private Vector2 _airAttackSize;

    [SerializeField]
    private float _airAttackMultiplier;

    [Header("FIRST ATTACK")]
    [SerializeField]
    private bool _showFirstAttackHitBox;

    [SerializeField]
    private Vector2 _firstAttackPoint;

    [SerializeField]
    private Vector2 _firstAttackSize;

    [SerializeField]
    private float _firstAttackMultiplier;

    [Header("SECOND ATTACK")]
    [SerializeField]
    private bool _showSecondAttackHitBox;

    [SerializeField]
    private Vector2 _secondAttackPoint;

    [SerializeField]
    private Vector2 _secondAttackSize;

    [SerializeField]
    private float _secondAttackMultiplier;

    [Header("THIRD ATTACK")]
    [SerializeField]
    private bool _showThirdAttackHitBox;

    [SerializeField]
    private Vector2 _thirdAttackPoint;

    [SerializeField]
    private Vector2 _thirdAttackSize;

    [SerializeField]
    private float _thirdAttackMultiplier;

    [Header("SPECIAL ATTACK FIRST PHASE")]
    [SerializeField]
    private bool _showSpecialAttackFirst;

    [SerializeField]
    private Vector2 _specialAttackFirstPoint;

    [SerializeField]
    private Vector2 _specialAttackFirstSize;

    [SerializeField]
    private float _specialAttackFirstMultiplier;

    [Header("SPECIAL ATTACK SECOND PHASE")]
    [SerializeField]
    private bool _showSpecialAttackSecond;

    [SerializeField]
    private Vector2 _specialAttackSecondPoint;

    [SerializeField]
    private Vector2 _specialAttackSecondSize;

    [SerializeField]
    private float _specialAttackSecondMultiplier;

    private readonly Collider2D[] _enemyHits = new Collider2D[32];

    //GETTERS & SETTERS
    public bool ShowAirAttackHitBox
    {
        set => _showAirAttackHitBox = value;
    }
    public bool ShowFirstAttackHitBox
    {
        set => _showFirstAttackHitBox = value;
    }
    public bool ShowSecondAttackHitBox
    {
        set => _showSecondAttackHitBox = value;
    }
    public bool ShowThirdAttackHitBox
    {
        set => _showThirdAttackHitBox = value;
    }
    public bool ShowSpecialAttackFirst
    {
        set => _showSpecialAttackFirst = value;
    }
    public bool ShowSpecialAttackSecond
    {
        set => _showSpecialAttackSecond = value;
    }

    protected override void LoadComponents()
    {
        _playerController = GetComponentInParent<PlayerController>();
    }

    protected override void LoadDefaultValues()
    {
        _enemyLayer = 128;
        //AIR ATTACK
        _airAttackPoint = new Vector2(1.65f, 1.12f);
        _airAttackSize = new Vector2(1.9f, 0.55f);

        //FIRST ATTACK
        _firstAttackPoint = new Vector2(1.4f, 0.78f);
        _firstAttackSize = new Vector2(0.85f, 0.15f);

        //SECOND ATTACK
        _secondAttackPoint = new Vector2(1.72f, 0.79f);
        _secondAttackSize = new Vector2(1.57f, 0.2f);

        //THIRD ATTACK
        _thirdAttackPoint = new Vector2(2.16f, 0.65f);
        _thirdAttackSize = new Vector2(1f, 1.26f);

        //SPECIAL ATTACK
        _specialAttackFirstPoint = new Vector2(1.92f, 0.33f);
        _specialAttackFirstSize = new Vector2(1.69f, 0.64f);

        _specialAttackSecondPoint = new Vector2(1.78f, 0.38f);
        _specialAttackSecondSize = new Vector2(2.67f, 0.73f);
    }

    public void DoAirAttack()
    {
        int size = Physics2D.OverlapBoxNonAlloc(
            (Vector2)transform.position
                + (Vector2)(transform.right + transform.up) * _airAttackPoint,
            _airAttackSize,
            0f,
            _enemyHits,
            _enemyLayer
        );
        if (size == 0)
            return;

        UtilTool.Combat.DamageAllTargetNonAlloc(
            _enemyHits,
            size,
            _playerController.CurrentStats.CurAtkDmg * _airAttackMultiplier / 100f
        );
    }

    public void DoFirstAttack()
    {
        int size = Physics2D.OverlapBoxNonAlloc(
            (Vector2)transform.position
                + (Vector2)(transform.right + transform.up) * _firstAttackPoint,
            _firstAttackSize,
            0f,
            _enemyHits,
            _enemyLayer
        );
        if (size == 0)
            return;

        UtilTool.Combat.DamageAllTargetNonAlloc(
            _enemyHits,
            size,
            _playerController.CurrentStats.CurAtkDmg * _firstAttackMultiplier / 100f
        );
    }

    public void DoSecondAttack()
    {
        int size = Physics2D.OverlapBoxNonAlloc(
            (Vector2)transform.position
                + (Vector2)(transform.right + transform.up) * _secondAttackPoint,
            _secondAttackSize,
            0f,
            _enemyHits,
            _enemyLayer
        );

        if (size == 0)
            return;

        UtilTool.Combat.DamageAllTargetNonAlloc(
            _enemyHits,
            size,
            _playerController.CurrentStats.CurAtkDmg * _secondAttackMultiplier / 100f
        );
    }

    public void DoThirdAttack()
    {
        int size = Physics2D.OverlapBoxNonAlloc(
            (Vector2)transform.position
                + (Vector2)(transform.right + transform.up) * _thirdAttackPoint,
            _thirdAttackSize,
            0f,
            _enemyHits,
            _enemyLayer
        );

        if (size == 0)
            return;

        UtilTool.Combat.DamageAllTargetNonAlloc(
            _enemyHits,
            size,
            _playerController.CurrentStats.CurAtkDmg * _thirdAttackMultiplier / 100f
        );
    }

    public void DoSpecialAttack_First()
    {
        int size = Physics2D.OverlapBoxNonAlloc(
            (Vector2)transform.position
                + (Vector2)(transform.right + transform.up) * _specialAttackFirstPoint,
            _specialAttackFirstSize,
            0f,
            _enemyHits,
            _enemyLayer
        );

        if (size == 0)
            return;

        UtilTool.Combat.DamageAllTargetNonAlloc(
            _enemyHits,
            size,
            _playerController.CurrentStats.CurAtkDmg
                * _specialAttackFirstMultiplier
                / 100f
        );
    }

    public void DoSpecialAttack_Second()
    {
        int size = Physics2D.OverlapBoxNonAlloc(
            (Vector2)transform.position
                + (Vector2)(transform.right + transform.up) * _specialAttackSecondPoint,
            _specialAttackSecondSize,
            0f,
            _enemyHits,
            _enemyLayer
        );

        if (size == 0)
            return;

        UtilTool.Combat.DamageAllTargetNonAlloc(
            _enemyHits,
            size,
            _playerController.CurrentStats.CurAtkDmg
                * _specialAttackSecondMultiplier
                / 100f
        );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (_showAirAttackHitBox)
        {
            Gizmos.DrawWireCube(
                (Vector2)transform.position
                    + (Vector2)(transform.right + transform.up) * _airAttackPoint,
                _airAttackSize
            );
        }
        else if (_showFirstAttackHitBox)
        {
            Gizmos.DrawWireCube(
                (Vector2)transform.position
                    + (Vector2)(transform.right + transform.up) * _firstAttackPoint,
                _firstAttackSize
            );
        }
        else if (_showSecondAttackHitBox)
        {
            Gizmos.DrawWireCube(
                (Vector2)transform.position
                    + (Vector2)(transform.right + transform.up) * _secondAttackPoint,
                _secondAttackSize
            );
        }
        else if (_showThirdAttackHitBox)
        {
            Gizmos.DrawWireCube(
                (Vector2)transform.position
                    + (Vector2)(transform.right + transform.up) * _thirdAttackPoint,
                _thirdAttackSize
            );
        }
        else if (_showSpecialAttackFirst)
        {
            Gizmos.DrawWireCube(
                (Vector2)transform.position
                    + (Vector2)(transform.right + transform.up) * _specialAttackFirstPoint,
                _specialAttackFirstSize
            );
        }
        else if (_showSpecialAttackSecond)
        {
            Gizmos.DrawWireCube(
                (Vector2)transform.position
                    + (Vector2)(transform.right + transform.up) * _specialAttackSecondPoint,
                _specialAttackSecondSize
            );
        }
    }
}
