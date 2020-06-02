using Leopotam.Ecs;

public class DoomSystem : IEcsRunSystem
{

	private readonly EcsFilter<DoomComponent> filter = default;
	private readonly RuntimeData runtimeData = default;
	private readonly GameData gameData = default;
	private readonly EcsWorld world = default;

	public void Run()
	{
		if (filter.IsEmpty()) return;

		var timerEnt = world.NewEntity();
		ref var timerUpdater = ref timerEnt.Set<TimerUpdaterComponent>();

		if(runtimeData.PlayersCount == 0)
		{
			timerUpdater.NewTime = 0;
		}
		else
		{
			if (runtimeData.IsDoom) return;
			var messEnt = world.NewEntity();
			ref var mess = ref messEnt.Set<ErrorComponent>();
			timerUpdater.NewTime = gameData.AfterFirstLeaverRoundDuration;
			mess.Message = $"The world will be destroyed in {gameData.AfterFirstLeaverRoundDuration} seconds";
		}

		runtimeData.IsDoom = true;

		foreach (var i in filter)
		{
			filter.GetEntity(i).Destroy();
		}
	}
}