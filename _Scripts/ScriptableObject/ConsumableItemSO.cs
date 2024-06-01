using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "SO/ConsumableItemSO")]
public class ConsumableItemSO : BaseItemSO
{
    [Header("CONSUMABLE ITEM CONFIGS")]
    [SerializeField]
    private StatToChange _statToChange;

    [SerializeField]
    private ChangeType _changeType;

    [SerializeField]
    private float _changeAmount;

    public override void UseItem()
    {
        PlayerStats playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();

        float amount = 0;

        switch (_changeType)
        {
            case ChangeType.Fixed:
                amount = _changeAmount;
                break;
            case ChangeType.Percentage:
                switch (_statToChange)
                {
                    case StatToChange.Health:
                        amount = playerStats.MaxHP * _changeAmount / 100;
                        break;
                    case StatToChange.Mana:
                        amount = playerStats.MaxMP * _changeAmount / 100;
                        break;
                }
                break;
        }

        switch (_statToChange)
        {
            case StatToChange.Health:
                playerStats.CurHP += amount;
                break;
            case StatToChange.Mana:
                playerStats.CurMP += amount;
                break;
        }
    }

    private enum StatToChange
    {
        Health = 0,
        Mana = 1,
    }

    private enum ChangeType
    {
        Fixed = 0,
        Percentage = 1,
    }
}

[CustomEditor(typeof(ConsumableItemSO))]
public class ConsumableItemSOEditor : Editor
{
    private const int SIZE = 128;

    public override void OnInspectorGUI()
    {
        // Lấy đối tượng ItemSO hiện tại
        ConsumableItemSO item = (ConsumableItemSO)target;

        // Vẽ các thuộc tính mặc định
        DrawDefaultInspector();

        // Hiển thị Sprite
        if (item.ItemSprite == null)
            return;

        Texture2D sprite = AssetPreview.GetAssetPreview(item.ItemSprite);
        GUILayout.Label("", GUILayout.Height(SIZE), GUILayout.Width(SIZE));
        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), sprite);
    }
}
