using UnityEngine;

public class PlayerController : UnitController
{
    [Header("PLAYER REFERENCES")]
    [SerializeField]
    private PlayerCombat _playerCombat;

    //GETTERS & SETTERS
    public PlayerCombat PlayerCombat => _playerCombat;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _playerCombat = GetComponentInChildren<PlayerCombat>();
    }

    protected override void LoadDynamicData()
    {
        currentStats = Resources.Load<StatsSO>("Stats/Water_Priestess/CurrentWaterPStats");
    }
}
