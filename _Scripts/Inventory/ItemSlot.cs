using UnityEngine;

public class ItemSlot : BaseItemSlot
{
    //ITEM DATA
    private int _quantity;
    private bool _isFull;

    //SETTERS & GETTERS
    public int Quantity
    {
        get => _quantity;
        private set
        {
            _quantity = value;
            UpdateItemSlot();
        }
    }
    public bool IsFull
    {
        get => _isFull;
        private set => _isFull = value;
    }

    private void UpdateItemSlot()
    {
        if (Quantity > 0)
            ItemSlotText.SetText(_quantity.ToString());
        else
            ClearItemSlot();
        ItemSlotText.enabled = Quantity > 0;
    }

    protected override void ClearItemSlot()
    {
        base.ClearItemSlot();
        IsFull = false;
    }

    public void Fill(
        bool isEquipmentGear,
        ItemName itemName,
        string itemDescription,
        Sprite itemSprite
    )
    {
        //ITEM DATA
        if (isEquipmentGear)
            IsFull = true;
        ItemName = itemName;
        ItemDescription = itemDescription;
        ++Quantity;

        //PREVIEW REFERENCES
        ItemSlotImage.sprite = itemSprite;
    }

    public void UseItem()
    {
        BaseItemSO itemSO = ItemLibraryManager.Instance.GetItemSO_ByName(ItemName);
        itemSO.UseItem();
        --Quantity;
    }

    public override void Drop()
    {
        --Quantity;
        ItemDropManager.Instance.Drop(ItemName, ItemDescription);
    }

    public override void ShowDescriptionMenu(bool value)
    {
        if (_quantity == 0)
            return;
        base.ShowDescriptionMenu(value);
    }
}
