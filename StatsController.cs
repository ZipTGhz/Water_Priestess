using UnityEngine;

public class StatsController : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField]
    private GameObject _statContext;

    [SerializeField]
    private GameObject _skillContext;

    public void OnStatsToggleOn()
    {
        _statContext.SetActive(true);
        _skillContext.SetActive(false);
    }

    public void OnSkillToggleOn()
    {
        _statContext.SetActive(false);
        _skillContext.SetActive(true);
    }
}
