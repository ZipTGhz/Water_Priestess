using UnityEngine;

public class DamageSender : MonoBehaviour
{
    public void Send(Collider2D hitInfo, float value)
    {
        //DO SOMETHING
        hitInfo.GetComponentInChildren<DamageReceiver>().Receive(value);
    }
}
