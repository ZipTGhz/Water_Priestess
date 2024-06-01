using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemName _itemName;

    private bool _isEquipmentGear;
    private string _itemDescription;

    public ItemName ItemName
    {
        get => _itemName;
        set => _itemName = value;
    }
    public string ItemDescription
    {
        get => _itemDescription;
        set => _itemDescription = value;
    }

    private void Start()
    {
        GetItemInfo();
    }

    private void GetItemInfo()
    {
        BaseItemSO itemSO = ItemLibraryManager.Instance.GetItemSO_ByName(ItemName);
        if (itemSO == null)
        {
            Debug.LogError("Item doesn't exist!");
            return;
        }
        if (itemSO as EquipmentItemSO)
            _isEquipmentGear = true;

        GetComponent<SpriteRenderer>().sprite = itemSO.ItemSprite;
        _itemDescription = itemSO.ItemDescription;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (
                InventoryManager.Instance.AddItemToInventory(
                    _isEquipmentGear,
                    _itemName,
                    _itemDescription,
                    GetComponent<SpriteRenderer>().sprite
                ) == false
            )
                return;
            Destroy(gameObject);
        }
    }
}
