using Leopotam.Ecs;

public class SpawnSavedPlayersSystem : IEcsRunSystem
{
	private readonly EcsFilter<SpawnSavedPlayersComponent> filter = default;
	private readonly RuntimeData runtimeData = default;
	private readonly EcsWorld world = default;

	public void Run()
	{
		foreach(var i in filter)
		{
			for (int k = 0; k < runtimeData.SavedPlayers.Count; ++k)
			{
				var e = world.NewEntity();
				ref var spawn = ref e.Set<PlayerSpawnComponent>();

				spawn.Stats = runtimeData.SavedPlayers[k];
			}

			runtimeData.ClearSavedPlayers();
		
			filter.GetEntity(i).Destroy();
		}

	}
}