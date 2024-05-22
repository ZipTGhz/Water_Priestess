using System;
using UnityEngine;

public class EnemyController : UnitController
{
    [Header("ENEMY REFERENCES")]
    [SerializeField]
    private EnemyStats _enemyStats;

    [SerializeField]
    private EnemyAI _ai;

    [SerializeField]
    private SliderController _hp;

    //GETTERS & SETTERS
    public EnemyStats CurrentStats
    {
        get => _enemyStats;
        set => _enemyStats = value;
    }
    public EnemyAI AI => _ai;

    private void OnEnable()
    {
        _enemyStats.OnHPChangedEvent += UpdateHP;
        _enemyStats.OnMaxHPChangedEvent += UpdateMaxHP;
    }

    private void OnDisable()
    {
        _enemyStats.OnHPChangedEvent -= UpdateHP;
        _enemyStats.OnMaxHPChangedEvent -= UpdateMaxHP;
    }

    private void UpdateMaxHP(float maxValue)
    {
        _hp.SetMaxValue(maxValue);
    }

    private void UpdateHP(float currentValue)
    {
        _hp.SetValue(currentValue);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _enemyStats = GetComponent<EnemyStats>();
        _ai = GetComponent<EnemyAI>();
        _hp = GetComponentInChildren<SliderController>();
    }
}
