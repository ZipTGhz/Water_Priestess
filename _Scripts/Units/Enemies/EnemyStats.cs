using System;
using UnityEngine;

public class EnemyStats : CustomMonoBehaviour, IDamageable
{
    public event Action<float> OnHPChangedEvent;
    public event Action<float> OnMaxHPChangedEvent;

    [SerializeField]
    private EnemyController _enemyController;

    [SerializeField]
    private EnemyStatsSO _baseStats;

    [SerializeField]
    private float _multiplier = 1f;

    [SerializeField]
    private float _maxHP;

    [SerializeField]
    private float _curHP;

    [SerializeField]
    private float _curSPD;

    [SerializeField]
    private bool _invulnerable;

    [SerializeField]
    private float _curAtkDmg;

    [SerializeField]
    private float _curAtkCD;

    [SerializeField]
    private float _curObserveCD;

    [SerializeField]
    private int _expGained;

    //GETTERS & SETTERS
    public float Multiplier
    {
        get => _multiplier;
        set => _multiplier = value;
    }
    public float MaxHP
    {
        get => _maxHP;
        set
        {
            _maxHP = value;
            OnMaxHPChangedEvent?.Invoke(_maxHP);
        }
    }
    public float CurHP
    {
        get => _curHP;
        set
        {
            _curHP = value;
            OnHPChangedEvent?.Invoke(_curHP);
        }
    }

    public float CurSPD
    {
        get => _curSPD;
        set => _curSPD = value;
    }

    public bool Invulnerable
    {
        get => _invulnerable;
        set => _invulnerable = value;
    }

    public float CurAtkDmg
    {
        get => _curAtkDmg;
        set => _curAtkDmg = value;
    }
    public float CurAtkCD
    {
        get => _curAtkCD;
        set => _curAtkCD = value;
    }
    public float CurObserveCD
    {
        get => _curObserveCD;
        set => _curObserveCD = value;
    }
    public int ExpGained
    {
        get => _expGained;
        set => _expGained = value;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _enemyController = GetComponent<EnemyController>();
    }

    protected override void LoadDynamicData()
    {
        base.LoadDynamicData();
        MaxHP = _baseStats.HP * Multiplier;
        CurHP = MaxHP;

        CurSPD = _baseStats.MoveSpeed;

        CurAtkDmg = _baseStats.AtkDmg * Multiplier;

        ExpGained = (int)(_baseStats.ExpGained * Multiplier);

        CurAtkCD = _baseStats.AtkCD;
        CurObserveCD = _baseStats.ObserveCD;
    }

    public void TakeHP(float value)
    {
        if (_invulnerable == true)
            return;
        CurHP = Mathf.Clamp(CurHP - value, 0, MaxHP);
        _enemyController.Animator.SetTrigger(NameHash.TakeHitTrigger);
        if (CurHP <= 0)
            Die();
    }

    public void Die()
    {
        if (_enemyController.Animator.GetBool(NameHash.IsDeathBool) == true)
            return;
        GameObject.FindWithTag("Player").GetComponent<IEXPGainer>().GainEXP(ExpGained);
        _enemyController.Animator.SetBool(NameHash.IsDeathBool, true);
    }
}
