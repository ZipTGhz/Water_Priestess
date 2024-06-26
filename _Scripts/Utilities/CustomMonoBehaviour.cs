using UnityEngine;

public class CustomMonoBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        LoadComponents();
        LoadDynamicData();
        // LoadDefaultValues();
    }

    protected virtual void Reset()
    {
        LoadComponents();
        LoadDynamicData();
        LoadDefaultValues();
    }

    protected virtual void LoadComponents() { }

    protected virtual void LoadDynamicData() { }

    protected virtual void LoadDefaultValues() { }
}
