using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : CustomMonoBehaviour
{
    public static PlayerInput Instance;

    private GameInputs _gameInputs;

    [Header("REFERENCES")]
    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private Transform _GFX;

    [SerializeField]
    private BoxCollider2D _collier2D;

    [Header("CONFIGS")]
    [SerializeField]
    private float _moveSpeed;

    [SerializeField]
    private float _jumpForce;

    [SerializeField]
    private float _airAttackFallSpeed;

    [SerializeField]
    private float _tumbleForce;

    [Header("COOLDOWN")]
    [SerializeField]
    private float _skillCoolDown;

    [SerializeField]
    private float _specialAttackCoolDown;

    [SerializeField]
    private float _tumbleCoolDown;

    [Header("RAYCAST")]
    [SerializeField]
    private Vector2 _boxOffset;

    [SerializeField]
    private Vector2 _boxSize;

    [SerializeField]
    private LayerMask _groundLayer;

    //VARIABLES
    private readonly string _xHash = "DirX";
    private readonly string _yHash = "DirY";
    private readonly string _commonAttackHash = "CommonAttack";
    private readonly string _skillHash = "Skill";
    private readonly string _specialAttackHash = "SpecialAttack";
    private readonly string _tumbleHash = "Tumble";

    private Vector2 _dirInput;
    private Vector2 _dir;

    private bool _isFacingRight = true;
    private bool _canCommonAttack;
    private bool _isCommonAttackPressed;
    private bool _isTumbleInProgress;

    private float _skillTime;
    private float _specialAttackTime;
    private float _tumbleTime;

    private bool _invulnerable;

    //GETTERS
    public string CommonAttackHash => _commonAttackHash;

    public bool IsCommonAttackPressed => _isCommonAttackPressed;

    public float AirAttackFallSpeed => _airAttackFallSpeed;
    public float TumbleForce => _tumbleForce;

    public Vector2 DirInput => _dirInput;

    public Vector2 GetVelocity => _rb.velocity;
    public float GetGravityScale => _rb.gravityScale;

    //SETTERS
    public void SetGravityScale(float gravityScale)
    {
        _rb.gravityScale = gravityScale;
    }

    public void SetVelocity(Vector2 _velocity)
    {
        _rb.velocity = _velocity;
    }

    //GETTERS AND SETTERS
    public Vector2 Dir
    {
        get => _dir;
        set => _dir = value;
    }

    public bool CanCommonAttack
    {
        get => _canCommonAttack;
        set => _canCommonAttack = value;
    }

    public bool Invulnerable
    {
        get => _invulnerable;
        set => _invulnerable = value;
    }

    public bool IsTumbleInProgress
    {
        get => _isTumbleInProgress;
        set => _isTumbleInProgress = value;
    }

    protected override void Awake()
    {
        base.Awake();
        if (Instance == null)
        {
            Instance = this;
            _gameInputs = new GameInputs();
        }
    }

    protected override void LoadComponents()
    {
        if (_rb == null)
        {
            GameObject gfxObject = GetComponentInChildren<Animator>().gameObject;
            _rb = GetComponent<Rigidbody2D>();
            _animator = gfxObject.GetComponent<Animator>();
            _GFX = gfxObject.GetComponent<Transform>();
            _collier2D = GetComponent<BoxCollider2D>();

            Debug.Log("Load components successfully from" + gameObject.name);
        }
    }

    protected override void LoadDefaultValues()
    {
        //CONFIGS
        _moveSpeed = 4f;
        _jumpForce = 15f;
        _airAttackFallSpeed = 1f;
        _tumbleForce = 8f;

        //COOLDOWN
        _skillCoolDown = 3f;
        _specialAttackCoolDown = 5f;
        _tumbleCoolDown = 1.5f;

        //RAYCAST
        _boxOffset = new Vector2(0.015f, 0.03f);
        _boxSize = new Vector2(0.59f, 0.05f);
        _groundLayer = 64;
    }

    private void Update()
    {
        // HandleJumpCollider();
    }

    private void FixedUpdate()
    {
        MovementX();
        MovementY();
        Flip();
    }

    private void MovementX()
    {
        if (_isTumbleInProgress == false)
            _rb.velocity = new Vector2(_dir.x * _moveSpeed, _rb.velocity.y);
        _animator.SetFloat(_xHash, Mathf.Abs(_dir.x));
    }

    private void MovementY()
    {
        if (_dir.y > 0.01f && IsGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }
        else if (_dir.y < -0.01f && IsGrounded())
        {
            if (Time.time > _tumbleTime)
            {
                _animator.SetTrigger(_tumbleHash);
                _tumbleTime = Time.time + _tumbleCoolDown;
            }
        }
        _animator.SetFloat(_yHash, _rb.velocity.y);
    }

    private void Flip()
    {
        if (_dir.x < 0 && _isFacingRight || _dir.x > 0 && !_isFacingRight)
        {
            _GFX.Rotate(0, 180, 0);
            _isFacingRight = !_isFacingRight;
        }
    }

    private void HandleJumpCollider()
    {
        if (_rb.velocity.y > 0.01f)
        {
            _collier2D.enabled = false;
        }
        else
        {
            _collier2D.enabled = true;
        }
    }

    private bool IsGrounded()
    {
        if (
            Physics2D.BoxCast(
                (Vector2)transform.position + _boxOffset,
                _boxSize,
                0,
                -transform.up,
                0f,
                _groundLayer
            )
        )
            return true;

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube((Vector2)transform.position + _boxOffset, _boxSize);
    }

    void OnEnable()
    {
        _gameInputs.Enable();

        _gameInputs.Gameplay.Movement.performed += OnMovement;
        _gameInputs.Gameplay.Movement.canceled += OnMovement;

        _gameInputs.Gameplay.CommonAttack.performed += OnCommonAttack;
        _gameInputs.Gameplay.CommonAttack.canceled += OnCommonAttack;

        _gameInputs.Gameplay.Skill.performed += OnSkill;
        _gameInputs.Gameplay.SpecialAttack.performed += OnSpecialAttack;
    }

    private void OnSpecialAttack(InputAction.CallbackContext context)
    {
        if (Time.time > _specialAttackTime)
        {
            _animator.SetTrigger(_specialAttackHash);
            _specialAttackTime = Time.time + _specialAttackCoolDown;
        }
    }

    private void OnSkill(InputAction.CallbackContext context)
    {
        if (Time.time > _skillTime)
        {
            _animator.SetTrigger(_skillHash);
            _skillTime = Time.time + _skillCoolDown;
        }
    }

    private void OnCommonAttack(InputAction.CallbackContext context)
    {
        _isCommonAttackPressed = context.ReadValueAsButton();
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        _dirInput = context.ReadValue<Vector2>();
        _dir = _dirInput;
    }

    private void OnDisable()
    {
        _gameInputs.Gameplay.Movement.performed -= OnMovement;
        _gameInputs.Gameplay.Movement.canceled -= OnMovement;

        _gameInputs.Gameplay.CommonAttack.performed -= OnCommonAttack;
        _gameInputs.Gameplay.CommonAttack.canceled -= OnCommonAttack;

        _gameInputs.Gameplay.Skill.performed -= OnSkill;
        _gameInputs.Gameplay.SpecialAttack.performed -= OnSpecialAttack;

        _gameInputs.Disable();
    }
}
