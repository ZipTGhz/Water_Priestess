using UnityEngine;

public class PlayerCombat : CustomMonoBehaviour
{
    public static PlayerCombat Instance;

    [Header("AIR ATTACK")]
    public bool ShowAirAttackHitBox;
    public Vector2 AirAttackPoint;
    public Vector2 AirAttackSize;

    [Header("FIRST ATTACK")]
    public bool ShowFirstAttackHitBox;
    public Vector2 FirstAttackPoint;
    public Vector2 FirstAttackSize;

    [Header("SECOND ATTACK")]
    public bool ShowSecondAttackHitBox;
    public Vector2 SecondAttackPoint;
    public Vector2 SecondAttackSize;

    [Header("THIRD ATTACK")]
    public bool ShowThirdAttackHitBox;
    public Vector2 ThirdAttackPoint;
    public Vector2 ThirdAttackSize;

    [Header("SPECIAL ATTACK FIRST PHASE")]
    public bool ShowSpecialAttackFirst;
    public Vector2 SpecialAttackFirstPoint;
    public Vector2 SpecialAttackFirstSize;

    [Header("SPECIAL ATTACK SECOND PHASE")]
    public bool ShowSpecialAttackSecond;
    public Vector2 SpecialAttackSecondPoint;
    public Vector2 SpecialAttackSecondSize;

    protected override void Awake()
    {
        base.Awake();
        if (Instance == null)
        {
            Instance = this;
        }
    }

    protected override void LoadDefaultValues()
    {
        //AIR ATTACK
        AirAttackPoint = new Vector2(1.65f, 1.12f);
        AirAttackSize = new Vector2(1.9f, 0.55f);

        //FIRST ATTACK
        FirstAttackPoint = new Vector2(1.4f, 0.78f);
        FirstAttackSize = new Vector2(0.85f, 0.15f);

        //SECOND ATTACK
        SecondAttackPoint = new Vector2(1.72f, 0.79f);
        SecondAttackSize = new Vector2(1.57f, 0.2f);

        //THIRD ATTACK
        ThirdAttackPoint = new Vector2(2.16f, 0.65f);
        ThirdAttackSize = new Vector2(1f, 1.26f);

        //SPECIAL ATTACK
        SpecialAttackFirstPoint = new Vector2(1.92f, 0.33f);
        SpecialAttackFirstSize = new Vector2(1.69f, 0.64f);

        SpecialAttackSecondPoint = new Vector2(1.78f, 0.38f);
        SpecialAttackSecondSize = new Vector2(2.67f, 0.73f);
    }

    public void DoAirAttack() { }

    public void DoFirstAttack() { }

    public void DoSecondAttack() { }

    public void DoThirdAttack() { }

    public void DoSpecialAttack_First() { }

    public void DoSpecialAttack_Second() { }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (ShowAirAttackHitBox)
        {
            Gizmos.DrawWireCube(
                (Vector2)transform.position
                    + (Vector2)(transform.right + transform.up) * AirAttackPoint,
                AirAttackSize
            );
        }
        else if (ShowFirstAttackHitBox)
        {
            Gizmos.DrawWireCube(
                (Vector2)transform.position
                    + (Vector2)(transform.right + transform.up) * FirstAttackPoint,
                FirstAttackSize
            );
        }
        else if (ShowSecondAttackHitBox)
        {
            Gizmos.DrawWireCube(
                (Vector2)transform.position
                    + (Vector2)(transform.right + transform.up) * SecondAttackPoint,
                SecondAttackSize
            );
        }
        else if (ShowThirdAttackHitBox)
        {
            Gizmos.DrawWireCube(
                (Vector2)transform.position
                    + (Vector2)(transform.right + transform.up) * ThirdAttackPoint,
                ThirdAttackSize
            );
        }
        else if (ShowSpecialAttackFirst)
        {
            Gizmos.DrawWireCube(
                (Vector2)transform.position
                    + (Vector2)(transform.right + transform.up) * SpecialAttackFirstPoint,
                SpecialAttackFirstSize
            );
        }
        else if (ShowSpecialAttackSecond)
        {
            Gizmos.DrawWireCube(
                (Vector2)transform.position
                    + (Vector2)(transform.right + transform.up) * SpecialAttackSecondPoint,
                SpecialAttackSecondSize
            );
        }
    }
}
