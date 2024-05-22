using System;
using UnityEngine;

public class PlayerController : UnitController
{
    [Header("PLAYER REFERENCES")]
    [SerializeField]
    private PlayerStats _playerStats;

    [SerializeField]
    private PlayerCombat _playerCombat;

    //GETTERS & SETTERS
    public PlayerCombat PlayerCombat => _playerCombat;
    public PlayerStats CurrentStats
    {
        get => _playerStats;
        set => _playerStats = value;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _playerStats = GetComponent<PlayerStats>();
        _playerCombat = GetComponentInChildren<PlayerCombat>();
    }
}
