using Leopotam.Ecs;
using UnityEngine;

public struct MapComponent
{
	public bool[,] Explored;
	public EcsEntity[,] Walls;
	public Vector2Int MapSize;
	public Vector3 CellSize;
	public Vector3 Position;
	public EcsEntity Exit;
}