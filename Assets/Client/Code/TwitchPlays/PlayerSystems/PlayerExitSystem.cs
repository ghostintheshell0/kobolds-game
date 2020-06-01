using Leopotam.Ecs;

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
			ref var exit = ref player.MapEntity.Set<MapComponent>().Exit.Set<ExitComponent>();
			if (exit.Position == player.Position)
			{
				player.Stats.Escapes++;
				var doomEnt = world.NewEntity();
				ref var doom = ref doomEnt.Set<DoomComponent>();

				var e = filter.GetEntity(i);
				runtimeData.SavePlayer(e);
				
				e.Set<RemovingComponent>();
				e.Unset<PlayerExitCommandComponent>();

			}
		}
	}
}