using UnityEngine;

public abstract class UnitController : CustomMonoBehaviour
{
    [Header("UNIT REFERENCES")]
    [SerializeField]
    protected Rigidbody2D rb;

    [SerializeField]
    protected Collider2D hitBox;

    [SerializeField]
    protected Transform gFX;

    [SerializeField]
    protected Animator animator;

    [SerializeField]
    protected DamageSender damageSender;

    [SerializeField]
    protected DamageReceiver damageReceiver;

    [Header("STATS")]
    [SerializeField]
    protected StatsSO currentStats;

    //GETTERS & SETTERS
    public Rigidbody2D Rb => rb;
    public Collider2D HitBox => hitBox;
    public Transform GFX => gFX;
    public Animator Animator => animator;
    public DamageSender DamageSender => damageSender;
    public DamageReceiver DamageReceiver => damageReceiver;

    public StatsSO CurrentStats => currentStats;

    protected override void LoadComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        hitBox = GetComponent<Collider2D>();
        GameObject gfxObject = GetComponentInChildren<Animator>().gameObject;
        gFX = gfxObject.GetComponent<Transform>();
        animator = gfxObject.GetComponent<Animator>();
        damageSender = GetComponentInChildren<DamageSender>();
        damageReceiver = GetComponentInChildren<DamageReceiver>();
    }

    public void ActivateUnit()
    {
        currentStats.SetDefaultValue();
        gameObject.SetActive(true);
    }

    public void DeactivateUnit()
    {
        gameObject.SetActive(false);
    }
}
