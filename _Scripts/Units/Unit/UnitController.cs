using UnityEngine;

public abstract class UnitController : CustomMonoBehaviour
{
    [Header("UNIT REFERENCES")]
    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private Collider2D _hitBox;

    [SerializeField]
    private Transform _gFX;

    [SerializeField]
    private Animator _animator;

    //GETTERS & SETTERS
    public Rigidbody2D Rb => _rb;
    public Collider2D HitBox => _hitBox;
    public Transform GFX => _gFX;
    public Animator Animator => _animator;

    protected override void LoadComponents()
    {
        _rb = GetComponent<Rigidbody2D>();
        _hitBox = GetComponent<Collider2D>();
        GameObject gfxObject = GetComponentInChildren<Animator>().gameObject;
        _gFX = gfxObject.GetComponent<Transform>();
        _animator = gfxObject.GetComponent<Animator>();
    }

    public virtual void DeactivateUnit()
    {
        gameObject.SetActive(false);
    }
}
