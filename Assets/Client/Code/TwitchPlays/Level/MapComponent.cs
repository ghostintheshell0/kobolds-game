using Leopotam.Ecs;
using UnityEngine;

public struct MapComponent
{
	public bool[,] Explored;
	public EcsEntity[,] Walls;
	public Vector2Int Size;
	public Vector3 CellSize;
	public Vector3 Position;
	public EcsEntity Exit;
	public EcsEntity Entity;
}