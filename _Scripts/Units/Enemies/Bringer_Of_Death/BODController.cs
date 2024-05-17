using UnityEngine;

public class BODController : EnemyController
{
    protected override void LoadDynamicData()
    {
        currentStats = Resources.Load<StatsSO>("Stats/BOD/CurrentBODStats");
        currentStats.SetDefaultValue();
    }
}
