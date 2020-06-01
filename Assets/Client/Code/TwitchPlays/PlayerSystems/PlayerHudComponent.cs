using Leopotam.Ecs;
using UnityEngine;

public struct PlayerHudComponent
{
	public MonoPlayerHUD View;
	public EcsEntity Target;
	public Vector3 Offset;
}
