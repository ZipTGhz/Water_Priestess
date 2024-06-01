using UnityEngine;

public abstract class BaseItemSO : ScriptableObject
{
    [Header("CONFIGS")]
    [SerializeField]
    private ItemName _itemName;

    [TextArea]
    [SerializeField]
    private string _itemDescription;

    [SerializeField]
    private Sprite _itemSprite;

    //GETTER & SETTERS
    public ItemName ItemName => _itemName;
    public string ItemDescription => _itemDescription;
    public Sprite ItemSprite => _itemSprite;

    public abstract void UseItem();
}
