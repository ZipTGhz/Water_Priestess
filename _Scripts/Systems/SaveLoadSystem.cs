using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    private readonly IDataService _dataService = new JsonDataService();
    private PlayerStats _playerStats;

    private void Start()
    {
        _playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }

    public void SerializeJSON()
    {
        PlayerData playerData = new(_playerStats);
        _dataService.SaveData("/player-stats.json", playerData, false);
        Debug.Log("SAVED!");
    }

    public void DeserializeJSON()
    {
        PlayerData playerData = _dataService.LoadData<PlayerData>("/player-stats.json", false);
        _playerStats.LoadData(playerData);
        Debug.Log("LOADED!");
    }
}
