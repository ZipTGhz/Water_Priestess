using System;
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
    private float _horizontalThreshold;

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
    private float _horizontalInput;
    private float _horizontalMove;

    private bool _isFacingRight = true;
    private bool _canCommonAttack;
    private bool _isCommonAttackPressed;
    private bool _isTumbleInProgress;
    private bool _canMovement = true;
    private bool _isBusyInput;

    private float _skillTime;
    private float _specialAttackTime;
    private float _tumbleTime;

    //GETTERS AND SETTERS
    public float HorizontalInput => _horizontalInput;
    public float HorizontalMove
    {
        get => _horizontalMove;
        set => _horizontalMove = value;
    }
    public bool IsCommonAttackPressed => _isCommonAttackPressed;
    public float AirAttackFallSpeed => _airAttackFallSpeed;

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
    public bool CanMovement
    {
        get => _canMovement;
        set => _canMovement = value;
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
        _horizontalThreshold = 0.2f;
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
        HorizontalMovement();
        VerticalMovement();
        Flip();
        CheckPlayerMovementOnWater();
    }

    private void HorizontalMovement()
    {
        if (_isTumbleInProgress == false)
        {
            if (_canMovement == false)
                _horizontalMove = 0;
            else if (_horizontalInput > _horizontalThreshold)
                _horizontalMove = _playerController.CurrentStats.CurSPD;
            else if (_horizontalInput < -_horizontalThreshold)
                _horizontalMove = -_playerController.CurrentStats.CurSPD;
            else
                _horizontalMove = 0;
            _playerController.Rb.velocity = new Vector2(
                _horizontalMove,
                _playerController.Rb.velocity.y
            );
        }
        _playerController.Animator.SetFloat(NameHash.DirXFloat, Mathf.Abs(_horizontalMove));
    }

    private void VerticalMovement()
    {
        _playerController.Animator.SetFloat(NameHash.YFloat, _playerController.Rb.velocity.y);
    }

    private void Flip()
    {
        if (_horizontalMove < 0 && _isFacingRight || _horizontalMove > 0 && !_isFacingRight)
        {
            _playerController.GFX.Rotate(0, 180, 0);
            _isFacingRight = !_isFacingRight;
        }
    }

    private float _lastHorizontalMove;
    private Coroutine _waterCollisionCoroutine = null;

    private void CheckPlayerMovementOnWater()
    {
        if (_horizontalMove != 0 && _lastHorizontalMove == 0)
        {
            if (_waterCollisionCoroutine != null)
                StopCoroutine(_waterCollisionCoroutine);

            Physics2D.IgnoreLayerCollision(
                LayerMask.NameToLayer("Player"),
                LayerMask.NameToLayer("Water"),
                false
            );
        }
        else if (_horizontalMove == 0 && _lastHorizontalMove != 0)
        {
            if (_waterCollisionCoroutine != null)
                StopCoroutine(_waterCollisionCoroutine);
            _waterCollisionCoroutine = StartCoroutine(DisableWaterCollisionWithDelay());
        }
        _lastHorizontalMove = _horizontalMove;
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

        _gameInputs.Gameplay.HorizontalInput.performed += OnHorizontalInput;
        _gameInputs.Gameplay.HorizontalInput.canceled += OnHorizontalInput;

        _gameInputs.Gameplay.Jump.started += OnJump;

        _gameInputs.Gameplay.Dash.started += OnDash;

        _gameInputs.Gameplay.CommonAttack.performed += OnCommonAttack;
        _gameInputs.Gameplay.CommonAttack.canceled += OnCommonAttack;

        _gameInputs.Gameplay.Skill.started += OnSkill;

        _gameInputs.Gameplay.SpecialAttack.started += OnSpecialAttack;
    }

    private void OnDisable()
    {
        _gameInputs.Gameplay.HorizontalInput.performed -= OnHorizontalInput;
        _gameInputs.Gameplay.HorizontalInput.canceled -= OnHorizontalInput;

        _gameInputs.Gameplay.Jump.started -= OnJump;

        _gameInputs.Gameplay.Dash.started -= OnDash;

        _gameInputs.Gameplay.CommonAttack.performed -= OnCommonAttack;
        _gameInputs.Gameplay.CommonAttack.canceled -= OnCommonAttack;

        _gameInputs.Gameplay.Skill.started -= OnSkill;

        _gameInputs.Gameplay.SpecialAttack.started -= OnSpecialAttack;

        _gameInputs.Disable();
    }

    private void OnHorizontalInput(InputAction.CallbackContext context)
    {
        _horizontalInput = context.ReadValue<float>();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (CanMovement && IsGrounded())
        {
            _playerController.Rb.velocity = new Vector2(
                _playerController.Rb.velocity.x,
                _playerController.CurrentStats.JumpForce
            );
        }
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        if (CanMovement && IsGrounded())
        {
            if (Time.time > _tumbleTime)
            {
                _playerController.Animator.SetTrigger(NameHash.TumbleTrigger);
                _tumbleTime = Time.time + _playerController.CurrentStats.DashCD;
            }
        }
    }

    private void OnCommonAttack(InputAction.CallbackContext context)
    {
        _isCommonAttackPressed = context.ReadValueAsButton();
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
}
