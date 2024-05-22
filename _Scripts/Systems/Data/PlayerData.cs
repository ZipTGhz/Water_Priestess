using UnityEngine;

public struct PlayerData
{
    //LEVEL
    public int Level;
    public int EXP;

    //HP
    public float MaxHP;
    public float CurHP;

    //MP
    public float MaxMP;
    public float CurMP;

    //OTHER DYNAMIC INFO
    public float AtkDmg;
    public float SPD;
    public float JumpForce;
    public float DashForce;

    //public Item[] Inventory;
    public float[] Position;

    public PlayerData(PlayerStats playerStats)
    {
        //LEVEL
        Level = playerStats.CurLevel;
        EXP = playerStats.CurEXP;
        //HP
        MaxHP = playerStats.MaxHP;
        CurHP = playerStats.CurHP;
        //MP
        MaxMP = playerStats.MaxMP;
        CurMP = playerStats.CurMP;

        //OTHER DYNAMIC INFO
        AtkDmg = playerStats.CurAtkDmg;
        SPD = playerStats.CurSPD;
        JumpForce = playerStats.JumpForce;
        DashForce = playerStats.DashForce;

        Vector3 position = playerStats.GetComponent<Transform>().position;
        Position = UtilTool.Vector3ToArray(position);
    }
}
