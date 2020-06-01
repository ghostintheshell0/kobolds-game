using UnityEngine;

public struct LevelGenerationComponent
{
	public RandomWallList Walls;
	public Vector2Int Size;
	public Vector3 StartPoint;
	public Vector3 CellSize;
	public MonoExit ExitTemlate;
	public int ExitBorederOffset;
	public int Spawners;
}
