using UnityEngine;

public abstract class EnemyAI : CustomMonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField]
    private EnemyController _enemyController;

    [SerializeField]
    private LayerMask _playerMask;

    [Header("CONFIGS")]
    [SerializeField]
    protected float moveSpeed;

    [SerializeField]
    protected float leftX;

    [SerializeField]
    protected float rightX;

    [SerializeField]
    protected float minDistanceToTarget;

    [SerializeField]
    protected Vector2 _detectPoint;

    [SerializeField]
    protected Vector2 _detectSize;

    [SerializeField]
    protected float attackCoolDown;

    [SerializeField]
    protected float observeCoolDown;

    private readonly float _radius = 0.5f;

    private float _dirX;

    private float _attackNextTime;
    private float _observeTime;

    private bool _isFacingRight;
    private bool _isDetectedPlayer;

    private readonly Collider2D[] _hitInfo = new Collider2D[4];

    //GETTERS & SETTERS
    public LayerMask PlayerMask;
    public float DirX
    {
        get => _dirX;
        set => _dirX = value;
    }

    protected override void LoadDynamicData()
    {
        _isFacingRight = _enemyController.GFX.right.x > 0;
        _dirX = _isFacingRight ? 1 : -1;
    }

    protected override void LoadComponents()
    {
        _enemyController = GetComponent<EnemyController>();
    }

    protected override void LoadDefaultValues()
    {
        _playerMask = 256;
        leftX = rightX = transform.position.x;
    }

    protected virtual void Update()
    {
        //kiểm tra xem người chơi có ở tầm mắt của đối tượng hay không
        DetectPlayerInRange();
        _enemyController.Animator.SetFloat(NameHash.XFloat, _enemyController.Rb.velocity.magnitude);

        //Nếu như không => 	di chuyển qua lại giữa 2 cực trái và phải (FixedUpdate)
        if (_isDetectedPlayer == false) { }
        //Nếu như có =>	nhìn vào hướng người chơi
        //				di chuyển đến người chơi (FixedUpdate)
        //				nếu như người chơi ở trong tầm đánh, thực hiện tấn công (FixedUpdate)
        else
        {
            _observeTime -= Time.deltaTime;
            LookAtPlayer();
            if (_observeTime <= 0)
                _isDetectedPlayer = false;
        }
    }

    protected virtual void FixedUpdate()
    {
        if (_isDetectedPlayer == false)
        {
            Move();
            Flip();
        }
        else
        {
            float distanceToTarget = MoveToTarget();
            if (distanceToTarget < 3f && Time.time >= _attackNextTime)
            {
                _enemyController.Animator.SetTrigger(NameHash.CommonAttackTrigger);
                _attackNextTime = Time.time + attackCoolDown;
            }
        }
    }

    protected virtual void Move()
    {
        _enemyController.Rb.velocity = new Vector2(moveSpeed * _dirX, 0f);
    }

    protected virtual float MoveToTarget()
    {
        Transform player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        float xPlayer = player.position.x;
        float xEnemy = transform.position.x;

        float distanceToTarget = Mathf.Abs(xEnemy - xPlayer);
        //Nếu như người chơi nằm trong vùng nhìn thấy của đối tượng, tiến hành di chuyển
        if (leftX <= xPlayer && xPlayer <= rightX)
            _enemyController.Rb.velocity = new Vector2(moveSpeed * _dirX, 0f);

        return distanceToTarget;
    }

    protected virtual void DetectPlayerInRange()
    {
        //Sử dụng raycast
        int size = Physics2D.OverlapBoxNonAlloc(
            _detectPoint,
            _detectSize,
            0f,
            _hitInfo,
            _playerMask
        );
        if (size == 0)
            return;
        //Tọa độ của người chơi
        float xPlayer = _hitInfo[0].transform.position.x;
        float xEnemy = transform.position.x;
        if (_isFacingRight && xPlayer < xEnemy || !_isFacingRight && xPlayer > xEnemy)
            return;

        _observeTime = observeCoolDown;

        Debug.Log("DETECTED PLAYER IN RANGE");
        _isDetectedPlayer = true;
    }

    public virtual void DetectPlayerWhenHurt()
    {
        //Khi bị tấn công thì kẻ địch sẽ nhìn vào hướng người chơi
        LookAtPlayer();
        Debug.Log("DETECTED PLAYER WHEN HURT");
        _isDetectedPlayer = true;
    }

    protected virtual void LookAtPlayer()
    {
        Transform player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        float xPlayer = player.position.x;
        float xEnemy = transform.position.x;

        if (_isFacingRight && xPlayer < xEnemy || !_isFacingRight && xPlayer > xEnemy)
            Flip(true);
    }

    protected virtual void Flip(bool forceMode = false)
    {
        //Tọa độ pivot của kẻ địch
        float x = transform.position.x;
        if (forceMode || x > rightX && _isFacingRight || x < leftX && !_isFacingRight)
        {
            _enemyController.GFX.Rotate(0, 180, 0);
            _dirX = -_dirX;
            _isFacingRight = !_isFacingRight;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector3(leftX, transform.position.y, 0), _radius);
        Gizmos.DrawWireSphere(new Vector3(rightX, transform.position.y, 0), _radius);
        Gizmos.DrawWireCube(_detectPoint, _detectSize);

        Gizmos.color = Color.black;
        Gizmos.DrawCube(transform.position, new Vector3(minDistanceToTarget, 0.5f, 0));
    }
}
