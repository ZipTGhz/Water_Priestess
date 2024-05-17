using UnityEngine;

public class DamageReceiver : CustomMonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private UnitController _unitController;

    protected override void LoadDefaultValues()
    {
        _animator = GetComponentInParent<Animator>();
        _unitController = GetComponentInParent<UnitController>();
    }

    public void Receive(float value)
    {
        //MINUS HP
        _unitController.CurrentStats.Heath -= value;
        if (_unitController.CurrentStats.Heath <= 0)
        {
            Die();
        }
        //DO SOMETHING
        _animator.SetTrigger(NameHash.TakeHitTrigger);
    }

    private void Die()
    {
        _animator.SetBool(NameHash.DeathBool, true);
    }
}
