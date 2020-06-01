using Leopotam.Ecs;
using UnityEngine;

[SerializeField]
public struct PlayerComponent
{
	public PlayerStats Stats;
	public Vector2Int Position;
	public MonoPlayer View;
	public EcsEntity MapEntity;
}