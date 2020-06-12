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
			var objEnt = map.GetObjectInPosition(player.Position);

			if (objEnt.IsAlive() && objEnt.Has<ExitComponent>())
			{
				ref var exit = ref objEnt.Set<ExitComponent>();
				player.Stats.Escapes++;
				var doomEnt = world.NewEntity();
				ref var doom = ref doomEnt.Set<DoomComponent>();

				runtimeData.EscapePlayer(player.Stats);

				playerEnt.Set<RemovingComponent>();
			}

			playerEnt.Unset<PlayerExitCommandComponent>();
		}
	}
}