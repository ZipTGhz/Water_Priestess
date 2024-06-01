using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseItemSlot : MonoBehaviour
{
    //ITEM DATA
    private ItemName _itemName;
    private string _itemDescription;

    [Header("EMPTY IMAGE REFERENCES")]
    [SerializeField]
    private Sprite _emptySprite;

    [Header("ITEM SLOT PREVIEW REFERENCES")]
    [SerializeField]
    private Image _itemSlotImage;

    [SerializeField]
    private TMP_Text _itemSlotText;

    [Header("ITEM DESCRIPTION PREVIEW REFERENCES")]
    [SerializeField]
    private GameObject _descriptionMenu;

    [SerializeField]
    private Image _itemDescriptionImage;

    [SerializeField]
    private TMP_Text _itemDescriptionType;

    [SerializeField]
    private TMP_Text _itemDescriptionName;

    [SerializeField]
    private TMP_Text _itemDescriptionText;

    //SETTERS & GETTERS
    public virtual ItemName ItemName
    {
        get => _itemName;
        protected set => _itemName = value;
    }
    public virtual string ItemDescription
    {
        get => _itemDescription;
        protected set => _itemDescription = value;
    }

    public virtual Sprite EmptySprite => _emptySprite;

    protected virtual Image ItemSlotImage
    {
        get => _itemSlotImage;
        set => _itemSlotImage = value;
    }
    protected virtual TMP_Text ItemSlotText
    {
        get => _itemSlotText;
        set => _itemSlotText = value;
    }
    protected virtual GameObject DescriptionMenu
    {
        get => _descriptionMenu;
    }
    protected virtual Image ItemDescriptionImage
    {
        get => _itemDescriptionImage;
        set => _itemDescriptionImage = value;
    }
    protected virtual TMP_Text ItemDescriptionType
    {
        get => _itemDescriptionType;
        set => _itemDescriptionType = value;
    }
    protected virtual TMP_Text ItemDescriptionName
    {
        get => _itemDescriptionName;
        set => _itemDescriptionName = value;
    }
    protected virtual TMP_Text ItemDescriptionText
    {
        get => _itemDescriptionText;
        set => _itemDescriptionText = value;
    }

    protected virtual void ClearItemSlot()
    {
        ItemSlotImage.sprite = EmptySprite;
        DescriptionMenu.SetActive(false);
    }

    public virtual void ShowDescriptionMenu(bool value)
    {
        DescriptionMenu.SetActive(value);
        if (value == true)
        {
            ItemDescriptionImage.sprite = ItemSlotImage.sprite;
            ItemDescriptionName.SetText("Name: " + ItemName.ToString());
            ItemDescriptionText.SetText(ItemDescription);
        }
    }

    public abstract void Drop();
}
