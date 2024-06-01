using UnityEngine;

public class PlayerStats : CustomMonoBehaviour, IDamageable, IEXPGainer, IMPUser
{
    [SerializeField]
    private PlayerController _playerController;

    [Header("LEVEL")]
    [SerializeField]
    private int _curLevel;

    [SerializeField]
    private int _maxEXP;

    [SerializeField]
    private int _curEXP;

    [Header("HP")]
    [SerializeField]
    private float _maxHP;

    [SerializeField]
    private float _curHP;

    [Header("MP")]
    [SerializeField]
    private float _maxMP;

    [SerializeField]
    private float _curMP;

    [Header("OTHER DYNAMIC INFO")]
    [SerializeField]
    private float _curAtkDmg;

    [SerializeField]
    private float _curSPD;

    [SerializeField]
    private bool _invulnerable;

    [Header("OTHER CONSTANT INFO")]
    [SerializeField]
    private float _jumpForce;

    [SerializeField]
    private float _dashForce;

    [SerializeField]
    private float _skillCD;

    [SerializeField]
    private float _specialAtkCD;

    [SerializeField]
    private float _dashCD;

    //GETTERS & SETTERS
    public PlayerController PlayerController => _playerController;

    //LEVEL
    public int CurLevel
    {
        get => _curLevel;
        set
        {
            _curLevel = value;
            MaxEXP = UtilTool.BaseStats.MaxEXP[_curLevel];
            UIManager.OnLevelUpEvent?.Invoke();
        }
    }
    public int MaxEXP
    {
        get => _maxEXP;
        set
        {
            _maxEXP = value;
            UIManager.OnMaxStatsChangedEvent?.Invoke(StatsEvent.EXP, _maxEXP);
        }
    }
    public int CurEXP
    {
        get => _curEXP;
        set
        {
            _curEXP = value;
            CheckLevelUp();
            UIManager.OnStatsChangedEvent?.Invoke(StatsEvent.EXP, _curEXP);
        }
    }

    //HEALTH POINT
    public float MaxHP
    {
        get => _maxHP;
        set
        {
            _maxHP = value;
            UIManager.OnMaxStatsChangedEvent?.Invoke(StatsEvent.HP, _maxHP);
        }
    }
    public float CurHP
    {
        get => _curHP;
        set
        {
            _curHP = Mathf.Clamp(value, 0, MaxHP);
            UIManager.OnStatsChangedEvent?.Invoke(StatsEvent.HP, _curHP);
        }
    }

    //MANA POINT
    public float MaxMP
    {
        get => _maxMP;
        set
        {
            _maxMP = value;
            UIManager.OnMaxStatsChangedEvent?.Invoke(StatsEvent.MP, _maxMP);
        }
    }
    public float CurMP
    {
        get => _curMP;
        set
        {
            _curMP = Mathf.Clamp(value, 0, MaxMP);
            UIManager.OnStatsChangedEvent?.Invoke(StatsEvent.MP, _curMP);
        }
    }

    //DYNAMIC INFO
    public float CurAtkDmg
    {
        get => _curAtkDmg;
        set { _curAtkDmg = value; }
    }
    public float CurSPD
    {
        get => _curSPD;
        set { _curSPD = value; }
    }
    public float JumpForce
    {
        get => _jumpForce;
        set => _jumpForce = value;
    }
    public float DashForce
    {
        get => _dashForce;
        set => _dashForce = value;
    }
    public bool Invulnerable
    {
        get => _invulnerable;
        set { _invulnerable = value; }
    }

    //CONSTANT INFO

    public float SkillCD
    {
        get => _skillCD;
        set => _skillCD = value;
    }
    public float SpecialAtkCD
    {
        get => _specialAtkCD;
        set => _specialAtkCD = value;
    }
    public float DashCD
    {
        get => _dashCD;
        set => _dashCD = value;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        LoadBaseStats(0, 0, true);
    }

    public void LoadData(PlayerData playerData)
    {
        LoadBaseStats(playerData.Level, playerData.EXP, false);
        LoadDynamicStats(playerData);
    }

    //NEW GAME => LEVEL = 0
    //CONTINUE => LEVEL = VALUE IN DATA
    private void LoadBaseStats(int level, int exp, bool isNewGame)
    {
        LoadConstantStats();
        //LEVEL
        CurLevel = level;
        CurEXP = exp;

        //OTHER DYNAMIC INFO
        if (isNewGame)
        {
            MaxHP = UtilTool.BaseStats.BaseHP[CurLevel];
            CurHP = MaxHP;

            MaxMP = UtilTool.BaseStats.BaseMP[CurLevel];
            CurMP = MaxMP;

            MaxEXP = UtilTool.BaseStats.MaxEXP[CurLevel];
            CurEXP = 0;

            CurAtkDmg = UtilTool.BaseStats.BaseATK[CurLevel];
            CurSPD = UtilTool.BaseStats.BaseSPD;
            JumpForce = UtilTool.BaseStats.BaseJumpForce;
            DashForce = UtilTool.BaseStats.BaseDashForce;
        }
    }

    private void LoadConstantStats()
    {
        //OTHER CONSTANT INFO
        SkillCD = UtilTool.BaseStats.BaseSkillCD;
        SpecialAtkCD = UtilTool.BaseStats.BaseSpecialAtkCD;
        DashCD = UtilTool.BaseStats.BaseDashCD;
    }

    //LOAD FROM SAVED DATA
    private void LoadDynamicStats(PlayerData playerData)
    {
        //HP
        MaxHP = playerData.MaxHP;
        CurHP = playerData.CurHP;
        //MP
        MaxMP = playerData.MaxMP;
        CurMP = playerData.CurMP;
        //OTHER DYNAMIC INFO
        CurAtkDmg = playerData.AtkDmg;
        CurSPD = playerData.SPD;
        JumpForce = playerData.JumpForce;
        DashForce = playerData.DashForce;
        GetComponent<Transform>().position = UtilTool.ArrayToVector3(playerData.Position);
    }

    public void TakeHP(float value)
    {
        if (_invulnerable == true)
            return;
        //MINUS HP
        CurHP = Mathf.Clamp(CurHP - value, 0, MaxHP);
        PlayerController.Animator.SetTrigger(NameHash.TakeHitTrigger);
        if (CurHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        PlayerController.Animator.SetBool(NameHash.IsDeathBool, true);
    }

    private void CheckLevelUp()
    {
        while (_curEXP >= MaxEXP)
            LevelUp();
    }

    private void LevelUp()
    {
        _curEXP -= MaxEXP;

        //CLEAR OLD BASE STATS
        MaxHP -= UtilTool.BaseStats.BaseHP[CurLevel];
        MaxMP -= UtilTool.BaseStats.BaseMP[CurLevel];
        CurAtkDmg -= UtilTool.BaseStats.BaseATK[CurLevel];

        //LEVEL UP
        ++CurLevel;

        //FILL NEW BASE STATS
        MaxHP += UtilTool.BaseStats.BaseHP[CurLevel];
        CurHP = MaxHP;

        MaxMP += UtilTool.BaseStats.BaseMP[CurLevel];
        CurMP = MaxMP;

        CurAtkDmg += UtilTool.BaseStats.BaseATK[CurLevel];
    }

    public void GainEXP(int value)
    {
        CurEXP += value;
    }

    public void UseMP(int value)
    {
        if (CurMP < value)
            return;
        CurMP -= value;
    }
}

public enum StatsEvent
{
    HP,
    MP,
    EXP,
    LV,
}
