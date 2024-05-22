using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField]
    private SliderController _hp;

    [SerializeField]
    private SliderController _mp;

    [SerializeField]
    private SliderController _exp;

    [SerializeField]
    private TextMeshProUGUI _levelText;

    [SerializeField]
    private PlayerStats _playerStats;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        _playerStats.OnStatsChangedEvent += UpdateStats;
        _playerStats.OnMaxStatsChangedEvent += UpdateMaxStats;
        _playerStats.OnLevelUpEvent += UpdateLevelStats;
    }

    private void OnDisable()
    {
        _playerStats.OnStatsChangedEvent -= UpdateStats;
        _playerStats.OnMaxStatsChangedEvent -= UpdateMaxStats;
        _playerStats.OnLevelUpEvent += UpdateLevelStats;
    }

    private void UpdateStats(StatsEvent curEvent, float curValue)
    {
        switch (curEvent)
        {
            case StatsEvent.HP:
                _hp.SetValue(curValue);
                break;
            case StatsEvent.MP:
                _mp.SetValue(curValue);
                break;
            case StatsEvent.EXP:
                _exp.SetValue(curValue);
                break;
        }
    }

    private void UpdateMaxStats(StatsEvent curEvent, float maxValue)
    {
        switch (curEvent)
        {
            case StatsEvent.HP:
                _hp.SetMaxValue(maxValue);
                break;
            case StatsEvent.MP:
                _mp.SetMaxValue(maxValue);
                break;
            case StatsEvent.EXP:
                _exp.SetMaxValue(maxValue);
                break;
        }
    }

    private void UpdateLevelStats()
    {
        int curLv = _playerStats.CurLevel;
        _levelText.SetText((curLv + 1).ToString());
    }
}
