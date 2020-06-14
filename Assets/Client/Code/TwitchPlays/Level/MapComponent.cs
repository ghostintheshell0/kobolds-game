using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public struct MapComponent
{
	public bool[,] Explored;
	public EcsEntity[,] Walls;
	public Vector2Int Size;
	public Vector3 CellSize;
	public Vector3 Position;
	public EcsEntity Entity;
	public List<EcsEntity> Objects;
	public List<EcsEntity> Spawners;
	public List<EcsEntity> FreeSpawners;
}