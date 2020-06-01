using UnityEngine;

[CreateAssetMenu(menuName =("Game/Visual generation settings"))]
public class VisualGenerationSettings : ScriptableObject
{
	public Vector3 StartPoint;
	public Vector3 CellSize;
	public MonoExit ExitTemplate;
}