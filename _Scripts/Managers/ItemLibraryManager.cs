using UnityEngine;

public class ItemLibraryManager : MonoBehaviour
{
    public static ItemLibraryManager Instance { get; private set; }

    [SerializeField]
    private BaseItemSO[] _itemLibrary;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public BaseItemSO GetItemSO_ByName(ItemName itemName)
    {
        foreach (BaseItemSO item in _itemLibrary)
        {
            if (itemName == item.ItemName)
                return item;
        }
        return null;
    }
}
