using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : CustomMonoBehaviour
{
    public static PlayerInput Instance;

    private GameInputs _gameInputs;

    [Header("REFERENCES")]
    [SerializeField]
    private PlayerController _playerController;

    [Header("CONFIGS")]
    [SerializeField]
    private float _airAttackFallSpeed;

    [Header("RAYCAST")]
    [SerializeField]
    private Vector2 _boxOffset;

    [SerializeField]
    private Vector2 _boxSize;

    [SerializeField]
    private LayerMask _groundCoderLayer;

    [SerializeField]
    private LayerMask _waterUserLayer;

    //VARIABLES

    private Vector2 _dirInput;
    private Vector2 _dir;

    private bool _isFacingRight = true;
    private bool _canCommonAttack;
    private bool _isCommonAttackPressed;
    private bool _isTumbleInProgress;
    private bool _isBusyInput;

    private float _skillTime;
    private float _specialAttackTime;
    private float _tumbleTime;

    //GETTERS AND SETTERS
    public bool IsCommonAttackPressed => _isCommonAttackPressed;
    public float AirAttackFallSpeed => _airAttackFallSpeed;
    public Vector2 DirInput => _dirInput;
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

    public bool IsTumbleInProgress
    {
        get => _isTumbleInProgress;
        set => _isTumbleInProgress = value;
    }
    public bool IsBusyInput
    {
        get => _isBusyInput;
        set => _isBusyInput = value;
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
        _playerController = GetComponent<PlayerController>();
    }

    protected override void LoadDefaultValues()
    {
        //CONFIGS
        _airAttackFallSpeed = 1f;

        //RAYCAST
        _boxOffset = new Vector2(0.015f, 0.03f);
        _boxSize = new Vector2(0.59f, 0.05f);
        _groundCoderLayer = LayerMask.GetMask("Ground", "Water");
        _waterUserLayer = LayerMask.NameToLayer("Water");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckCanSurf(collision.gameObject.layer);
    }

    private void CheckCanSurf(int layer)
    {
        _playerController.Animator.SetBool(NameHash.CanSurfBool, layer == _waterUserLayer);
    }

    private void Update()
    {
        CheckPlayerMovementOnWater();
    }

    private Vector2 _lastDir = Vector2.zero;
    private Coroutine _waterCollisionCoroutine = null;

    private void CheckPlayerMovementOnWater()
    {
        if (_dir.magnitude != 0 && _lastDir.magnitude == 0)
        {
            if (_waterCollisionCoroutine != null)
                StopCoroutine(_waterCollisionCoroutine);

            Physics2D.IgnoreLayerCollision(
                LayerMask.NameToLayer("Player"),
                LayerMask.NameToLayer("Water"),
                false
            );
        }
        else if (_dir.magnitude == 0 && _lastDir.magnitude != 0)
        {
            if (_waterCollisionCoroutine != null)
                StopCoroutine(_waterCollisionCoroutine);
            _waterCollisionCoroutine = StartCoroutine(DisableWaterCollisionWithDelay());
        }
        _lastDir = _dir;
    }

    IEnumerator DisableWaterCollisionWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreLayerCollision(
            LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("Water"),
            true
        );
        _waterCollisionCoroutine = null;
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
            _playerController.Rb.velocity = new Vector2(
                _dir.x * _playerController.CurrentStats.CurSPD,
                _playerController.Rb.velocity.y
            );
        _playerController.Animator.SetFloat(NameHash.XFloat, Mathf.Abs(_dir.x));
    }

    private void MovementY()
    {
        if (_dir.y > 0.01f && IsGrounded())
        {
            _playerController.Rb.velocity = new Vector2(
                _playerController.Rb.velocity.x,
                _playerController.CurrentStats.JumpForce
            );
        }
        else if (_dir.y < -0.01f && IsGrounded())
        {
            if (Time.time > _tumbleTime)
            {
                _playerController.Animator.SetTrigger(NameHash.TumbleTrigger);
                _tumbleTime = Time.time + _playerController.CurrentStats.DashCD;
            }
        }
        _playerController.Animator.SetFloat(NameHash.YFloat, _playerController.Rb.velocity.y);
    }

    private void Flip()
    {
        if (_dir.x < 0 && _isFacingRight || _dir.x > 0 && !_isFacingRight)
        {
            _playerController.GFX.Rotate(0, 180, 0);
            _isFacingRight = !_isFacingRight;
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
                _groundCoderLayer
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
        if (
            _isBusyInput
            || _playerController.CurrentStats.CurMP < UtilTool.BaseStats.SpecialAtkManaCost
        )
            return;
        if (Time.time > _specialAttackTime)
        {
            _playerController.Animator.SetTrigger(NameHash.SpecialAttackTrigger);
            _specialAttackTime = Time.time + _playerController.CurrentStats.SpecialAtkCD;
        }
    }

    private void OnSkill(InputAction.CallbackContext context)
    {
        if (_isBusyInput || _playerController.CurrentStats.CurMP < UtilTool.BaseStats.SkillManaCost)
            return;

        if (Time.time > _skillTime)
        {
            _playerController.Animator.SetTrigger(NameHash.SkillTrigger);
            _skillTime = Time.time + _playerController.CurrentStats.SkillCD;
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
