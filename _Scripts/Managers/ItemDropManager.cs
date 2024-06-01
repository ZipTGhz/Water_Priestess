using UnityEngine;

public class ItemDropManager : MonoBehaviour
{
    public static ItemDropManager Instance;

    [SerializeField]
    private GameObject _itemPrefab;

    [SerializeField]
    private GameObject _itemDropBucket;
    private Transform _playerTransform;
    private Transform _gfxTransform;

    private Vector3 PlayerPos => _playerTransform.position;
    private Vector3 FaceDirection => _gfxTransform.right;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _gfxTransform = _playerTransform
            .GetComponentInChildren<SpriteRenderer>()
            .GetComponent<Transform>();
    }

    public void Drop(ItemName itemName, string itemDescription)
    {
        GameObject itemObjectDropped = Instantiate(
            _itemPrefab,
            PlayerPos + FaceDirection,
            Quaternion.identity,
            _itemDropBucket.transform
        );
        Item itemDropped = itemObjectDropped.GetComponent<Item>();
        itemDropped.ItemName = itemName;
        itemDropped.ItemDescription = itemDescription;
    }
}
