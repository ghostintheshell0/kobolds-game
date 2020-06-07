using UnityEngine;

[CreateAssetMenu(menuName = "Game/Generation Settings")]
public class GenerationSettings : ScriptableObject
{
	
	[SerializeField] private VisualGenerationSettings visualSettings = default;
	[SerializeField] private RandomWallList walls = default;
	[SerializeField] private Vector2Int mapSize = default;
	[SerializeField] private int totalSpawners = default;
	[SerializeField] private int exitBorderOffset = 1;

	public VisualGenerationSettings VisualSettings => visualSettings;
	public Vector2Int MapSize => mapSize;
	public int Spawners => totalSpawners;
	public RandomWallList Walls => walls;
	public int ExitBorderOffset => exitBorderOffset;
}