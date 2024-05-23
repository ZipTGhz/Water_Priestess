using System;
using UnityEngine;

public class EnemyStats : CustomMonoBehaviour, IDamageable
{
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
            _enemyController.OnMaxHPChangedEvent?.Invoke(_maxHP);
        }
    }
    public float CurHP
    {
        get => _curHP;
        set
        {
            _curHP = value;
            _enemyController.OnHPChangedEvent?.Invoke(_curHP);
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

    private void Start()
    {
        LoadDynamicStats();
        LoadConstantStats();
    }

    private void LoadConstantStats()
    {
        CurSPD = _baseStats.MoveSpeed;

        CurAtkCD = _baseStats.AtkCD;
        CurObserveCD = _baseStats.ObserveCD;
    }

    public void LoadDynamicStats()
    {
        float curMul = Multiplier + UIManager.Instance.CurLevel * 1f / 2;

        MaxHP = _baseStats.HP * curMul;
        CurHP = MaxHP;
        CurAtkDmg = _baseStats.AtkDmg * curMul;
        ExpGained = (int)(_baseStats.ExpGained * curMul);
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
