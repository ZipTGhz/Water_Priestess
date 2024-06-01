using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSheetManager : MonoBehaviour
{
    public static CharacterSheetManager Instance { get; private set; }

    [Header("DATA REFERENCES")]
    [SerializeField]
    private EquipmentSlot[] _equipmentSlots;

    [SerializeField]
    private Toggle[] _togglesAsEquipmentSlot;
    private int _selectedIndex;

    [SerializeField]
    private TMP_Text _hpText;

    [SerializeField]
    private TMP_Text _mpText;

    [SerializeField]
    private TMP_Text _atkText;

    [SerializeField]
    private TMP_Text _spdText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // Gán sự kiện cho từng toggle trong túi đồ
        for (int i = 0; i < _togglesAsEquipmentSlot.Length; i++)
        {
            int index = i;
            _togglesAsEquipmentSlot[i]
                .onValueChanged.AddListener((isOn) => OnToggleClicked(index, isOn));
        }
    }

    private void OnToggleClicked(int index, bool isOn)
    {
        if (isOn)
            _selectedIndex = index;
    }

    public void EquipGear(BaseItemSO item)
    {
        int indexOfSlot = (int)item.ItemName % 100;
        if (_equipmentSlots[indexOfSlot].IsEquip == true)
            UnEquipGear();
        _equipmentSlots[indexOfSlot].EquipGear(item);
        OnStatsChanged();
    }

    public void OnInventoryOpen()
    {
        OnStatsChanged();
    }

    private void OnStatsChanged()
    {
        PlayerStats playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();

        _hpText.SetText(playerStats.MaxHP.ToString());
        _mpText.SetText(playerStats.MaxMP.ToString());
        _atkText.SetText(playerStats.CurAtkDmg.ToString());
        _spdText.SetText(playerStats.CurSPD.ToString());
    }

    public void Drop()
    {
        _equipmentSlots[_selectedIndex].Drop();
        OnStatsChanged();
    }

    public void UnEquipGear()
    {
        _equipmentSlots[_selectedIndex].UnEquipGear();
        OnStatsChanged();
    }
}
