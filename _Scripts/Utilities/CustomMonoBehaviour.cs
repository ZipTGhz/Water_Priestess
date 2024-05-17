using UnityEngine;

public class CustomMonoBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        LoadDynamicData();
        LoadComponents();
        // LoadDefaultValues();
    }

    protected virtual void Reset()
    {
        LoadComponents();
        LoadDynamicData();
        LoadDefaultValues();
    }

    protected virtual void LoadDynamicData() { }

    protected virtual void LoadDefaultValues() { }

    protected virtual void LoadComponents() { }
}
