using Leopotam.Ecs;

public class EndGameSystem : IEcsRunSystem
{
	private readonly EcsFilter<EndGameComponent> filter = default;
	private readonly RuntimeData runtimeData = default;
	private readonly EcsWorld world = default;


	public void Run()
	{
		if (filter.IsEmpty()) return;

		var clearEnt = world.NewEntity();
		clearEnt.Set<ClearLevelComponent>();

		var messEnt = world.NewEntity();
		ref var message = ref messEnt.Set<ErrorComponent>();

		if (runtimeData.SavedPlayers.Count == 0)
		{
			message.Message = $"Level completed.";
		}
		else
		{
			message.Message = $"Level completed. Saved players: ";

			for (int i = 0; i < runtimeData.SavedPlayers.Count; ++i)
			{
				if (runtimeData.SavedPlayers.Count - 1 == i)
				{
					message.Message += $"@{runtimeData.SavedPlayers[i].Name} .";
				}
				else
				{
					message.Message += $"@{runtimeData.SavedPlayers[i].Name} , ";
				}
			}
		}

		foreach(var i in filter)
		{
			filter.GetEntity(i).Destroy();
		}

	}
}