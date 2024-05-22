using UnityEngine;

public static class UtilTool
{
    public static float[] Vector3ToArray(Vector3 value)
    {
        return new float[] { value.x, value.y, value.z };
    }

    public static Vector3 ArrayToVector3(float[] value)
    {
        if (value.Length != 3)
        {
            Debug.LogError("Array length is not 3. Current length: " + value.Length);
            return Vector3.zero;
        }
        return new Vector3(value[0], value[1], value[2]);
    }

    public static class Combat
    {
        public static void DamageAllTargetNonAlloc(Collider2D[] colliders, int size, float damage)
        {
            for (int i = 0; i < size; ++i)
            {
                colliders[i].GetComponent<IDamageable>().TakeHP(damage);
            }
        }
    }

    public static class BaseStats
    {
        //HP
        public static readonly int[] BaseHP = { 10, 12, 15, 18, 22, 26, 31, 36, 42, 50 };

        //MP
        public static readonly int[] BaseMP = { 5, 7, 10, 13, 17, 21, 26, 31, 37, 43 };

        //LEVEL
        public static readonly int[] MaxEXP = { 3, 7, 20, 53, 103, 256, 745, 1569, 3251, 99999 };

        //INFO
        public static readonly float[] BaseATK =
        {
            1,
            1.2f,
            1.4f,
            1.6f,
            2f,
            2.5f,
            3f,
            3.5f,
            4.5f,
            6f
        };
        public static readonly float BaseSPD = 4f;

        public static readonly float BaseJumpForce = 15f;
        public static readonly float BaseDashForce = 8f;

        public static readonly float BaseSkillCD = 3f;
        public static readonly float BaseSpecialAtkCD = 3f;
        public static readonly float BaseDashCD = 2f;

        public static readonly int SkillManaCost = 2;
        public static readonly int SpecialAtkManaCost = 3;
    }
}
