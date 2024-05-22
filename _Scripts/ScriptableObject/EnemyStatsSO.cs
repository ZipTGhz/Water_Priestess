using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "SO/EnemyStatsSO")]
public class EnemyStatsSO : ScriptableObject
{
    [Header("ENEMY STATS")]
    [SerializeField]
    private float _hP;

    [SerializeField]
    private float _atkDmg;

    [SerializeField]
    private float _moveSpeed;

    [SerializeField]
    private float _atkCD;

    [SerializeField]
    private float _observeCD;

    [SerializeField]
    private int _expGained;

    //GETTERS & SETTERS
    public float HP => _hP;

    public float AtkDmg => _atkDmg;
    public float MoveSpeed => _moveSpeed;

    public float AtkCD => _atkCD;
    public float ObserveCD => _observeCD;

    public int ExpGained => _expGained;
}
