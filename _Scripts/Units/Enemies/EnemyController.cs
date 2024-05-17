using UnityEngine;

public class EnemyController : UnitController
{
    [Header("CUSTOM")]
    [SerializeField]
    private EnemyAI _ai;

    //GETTERS & SETTERS
    public EnemyAI AI => _ai;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _ai = GetComponent<EnemyAI>();
    }
}
