using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "SO/Stats")]
public class StatsSO : ScriptableObject
{
	[Header("BASE INFO")]
	[SerializeField]
	private float _baseHeath;

	[Header("CURRENT INFO")]
	public float Heath;
	public float AttackDamage;

	public void SetDefaultValue()
	{
		Heath = _baseHeath;
	}
}
