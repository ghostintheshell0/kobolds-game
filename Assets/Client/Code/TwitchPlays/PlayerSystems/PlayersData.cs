using UnityEngine;

[CreateAssetMenu(menuName =("Game/Player start values"))]
public class PlayersData : ScriptableObject
{
	public PlayerStats StartPlayerValues;

	public float MovingSpeed;
	public float StoppingDistance;
	public int PickaxeAnimationVariants;
	public int DamageMultipler;
	public int LevelCostMultipler;
	public int EnableAgentFramesDelay;
}