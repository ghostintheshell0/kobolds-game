using Leopotam.Ecs;
using UnityEngine;

public class PlayerExitSystem : IEcsRunSystem
{
	private readonly EcsFilter<PlayerComponent, PlayerExitCommandComponent> filter = default;
	private readonly RuntimeData runtimeData = default;
	private readonly EcsWorld world = default;

	public void Run()
	{
		foreach(var i in filter)
		{
			ref var player = ref filter.Get1(i);
			var playerEnt = filter.GetEntity(i);
			ref var map = ref player.MapEntity.Set<MapComponent>();
			var objEnt = GetObjectInPos(ref map, player.Position);

			if (objEnt.IsAlive() && objEnt.Has<ExitComponent>())
			{
				ref var exit = ref objEnt.Set<ExitComponent>();
				if (exit.Position == player.Position)
				{
					player.Stats.Escapes++;
					var doomEnt = world.NewEntity();
					ref var doom = ref doomEnt.Set<DoomComponent>();

					runtimeData.SavePlayer(playerEnt);

					playerEnt.Set<RemovingComponent>();

				}
			}

			playerEnt.Unset<PlayerExitCommandComponent>();
		}
	}

	private EcsEntity GetObjectInPos(ref MapComponent map, Vector2Int pos)
	{
		for(var i = 0; i < map.Objects.Count; ++i)
		{
			ref var mapObjComp = ref map.Objects[i].Set<MapObjectComponent>();
			if (mapObjComp.Position == pos) return map.Objects[i];
		}

		return EcsEntity.Null;

	}
}