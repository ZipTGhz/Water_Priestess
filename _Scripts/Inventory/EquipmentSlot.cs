using UnityEngine;

public class EquipmentSlot : BaseItemSlot
{
    //ITEM DATA
    private bool _isEquip;

    //SETTERS & GETTERS
    public bool IsEquip
    {
        get => _isEquip;
        private set
        {
            _isEquip = value;
            UpdateEquipmentSlot();
        }
    }

    private void UpdateEquipmentSlot()
    {
        ItemSlotText.enabled = !IsEquip;
        if (IsEquip == false)
            ClearItemSlot();
    }

    public void EquipGear(BaseItemSO item)
    {
        //ITEM DATA
        IsEquip = true;
        ItemName = item.ItemName;
        ItemDescription = item.ItemDescription;

        //PREVIEW REFERENCES
        ItemSlotImage.sprite = item.ItemSprite;
    }

    public void UnEquipGear()
    {
        EquipmentItemSO itemSO =
            ItemLibraryManager.Instance.GetItemSO_ByName(ItemName) as EquipmentItemSO;
        if (itemSO == null)
        {
            Debug.LogError("Item doesn't exist!");
            return;
        }
        itemSO.UnEquipGear();

        if (
            InventoryManager.Instance.AddItemToInventory(
                true,
                ItemName,
                ItemDescription,
                ItemSlotImage.sprite
            ) == false
        )
            return;

        IsEquip = false;
    }

    public override void Drop()
    {
        EquipmentItemSO itemSO =
            ItemLibraryManager.Instance.GetItemSO_ByName(ItemName) as EquipmentItemSO;
        if (itemSO == null)
        {
            Debug.LogError("Item doesn't exist!");
            return;
        }
        itemSO.UnEquipGear();

        ItemDropManager.Instance.Drop(ItemName, ItemDescription);

        IsEquip = false;
    }

    public override void ShowDescriptionMenu(bool value)
    {
        if (_isEquip == false)
            return;
        base.ShowDescriptionMenu(value);
    }
}
