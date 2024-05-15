using UnityEngine;

public class CustomMonoBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        LoadComponents();
        // LoadDefaultValues();
    }

    protected virtual void Reset()
    {
        LoadComponents();
        LoadDefaultValues();
    }

    protected virtual void LoadDefaultValues() { }

    protected virtual void LoadComponents() { }
}
