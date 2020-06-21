using UnityEngine;

[CreateAssetMenu(menuName = "Game/Generation Settings")]
public class GenerationSettings : ScriptableObject
{
	
//	[SerializeField] private VisualGenerationSettings visualSettings = default;
	[SerializeField] private RandomWallList walls = default;
	[SerializeField] private MapObjects mapObjects = default;
	[SerializeField] private MonoFloor floorTemplate = default;
	[SerializeField] private Vector2Int mapSize = default;
	[SerializeField] private int totalSpawners = default;
	[SerializeField] private int exitBorderOffset = 1;
	[SerializeField] public Vector3 startPoint;
	[SerializeField] public Vector3 cellSize;

//	public VisualGenerationSettings VisualSettings => visualSettings;
	public RandomWallList Walls => walls;
	public MapObjects MapObjects => mapObjects;
	public MonoFloor FloorTemplate => floorTemplate;
	public Vector2Int MapSize => mapSize;
	public Vector3 CellSize => cellSize;
	public Vector3 StartPoint => startPoint;
	public int Spawners => totalSpawners;
	//	public int ExitBorderOffset => exitBorderOffset;
}