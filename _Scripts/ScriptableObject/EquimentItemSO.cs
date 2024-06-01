using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Item", menuName = "SO/EquipmentItemSO")]
public class EquipmentItemSO : BaseItemSO
{
    [Header("EQUIPMENT ITEM CONFIGS")]
    [SerializeField]
    private float _hp;

    [SerializeField]
    private float _mp;

    [SerializeField]
    private float _atk;

    [SerializeField]
    private float _spd;

    public override void UseItem()
    {
        EquipGear();
    }

    private void EquipGear()
    {
        PlayerStats playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();

        playerStats.MaxHP += _hp;
        playerStats.CurHP += _hp;

        playerStats.MaxMP += _mp;
        playerStats.CurMP += _mp;

        playerStats.CurAtkDmg += _atk;
        playerStats.CurSPD += _spd;

        CharacterSheetManager.Instance.EquipGear(this);
    }

    public void UnEquipGear()
    {
        PlayerStats playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();

        playerStats.MaxHP -= _hp;
        playerStats.CurHP -= _hp;

        playerStats.MaxMP -= _mp;
        playerStats.CurMP -= _mp;

        playerStats.CurAtkDmg -= _atk;
        playerStats.CurSPD -= _spd;
    }
}

[CustomEditor(typeof(EquipmentItemSO))]
public class EquipmentItemSOEditor : Editor
{
    private const int SIZE = 128;

    public override void OnInspectorGUI()
    {
        // Lấy đối tượng ItemSO hiện tại
        EquipmentItemSO item = (EquipmentItemSO)target;

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
