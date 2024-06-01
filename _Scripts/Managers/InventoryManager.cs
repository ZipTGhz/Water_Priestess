using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [Header("DATA REFERENCES")]
    [SerializeField]
    private ItemSlot[] _itemSlots;

    [SerializeField]
    private Toggle[] _togglesAsItemSlot;

    [SerializeField]
    private int _selectedIndex;

    //GETTERS & SETTERS


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        // Gán sự kiện cho từng toggle trong túi đồ
        for (int i = 0; i < _togglesAsItemSlot.Length; i++)
        {
            int index = i;
            _togglesAsItemSlot[i]
                .onValueChanged.AddListener((isOn) => OnToggleClicked(index, isOn));
        }
    }

    private void OnToggleClicked(int index, bool isOn)
    {
        if (isOn)
            _selectedIndex = index;
    }

    public bool AddItemToInventory(bool isEquipmentGear, ItemName itemName, string itemDescription, Sprite itemSprite)
    {
        foreach (ItemSlot slot in _itemSlots)
        {
            if (slot.Quantity == 0 || (slot.IsFull == false && slot.ItemName == itemName))
            {
                slot.Fill(isEquipmentGear, itemName, itemDescription, itemSprite);
                return true;
            }
        }
        return false;
    }

    public void Use()
    {
        _itemSlots[_selectedIndex].UseItem();
    }

    public void Drop()
    {
        _itemSlots[_selectedIndex].Drop();
    }
}
