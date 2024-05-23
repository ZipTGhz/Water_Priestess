using System;
using UnityEngine;

public class EnemyController : UnitController
{
	public Action<float> OnHPChangedEvent;
	public Action<float> OnMaxHPChangedEvent;

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
		OnHPChangedEvent += UpdateHP;
		OnMaxHPChangedEvent += UpdateMaxHP;
	}

	private void OnDisable()
	{
		OnHPChangedEvent -= UpdateHP;
		OnMaxHPChangedEvent -= UpdateMaxHP;
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

	public void ReSpawn()
	{
		_ai.ObserveTime = 0f;
		_enemyStats.LoadDynamicStats();
	}
}
