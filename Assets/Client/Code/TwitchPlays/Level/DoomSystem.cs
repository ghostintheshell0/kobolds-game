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


		if (runtimeData.PlayersCount == 0)
		{
			var timerEnt = world.NewEntity();
			ref var timerUpdater = ref timerEnt.Set<TimerUpdaterComponent>();
			timerUpdater.NewTime = 0;
		}
		else if (!runtimeData.IsDoom)
		{
			runtimeData.IsDoom = true;
			var messEnt = world.NewEntity();
			ref var mess = ref messEnt.Set<ErrorComponent>();
			var timerEnt = world.NewEntity();
			ref var timerUpdater = ref timerEnt.Set<TimerUpdaterComponent>();
			timerUpdater.NewTime = gameData.AfterFirstLeaverRoundDuration;
			mess.Message = $"The world will be destroyed in {gameData.AfterFirstLeaverRoundDuration} seconds";
		}

		foreach (var i in filter)
		{
			filter.GetEntity(i).Destroy();
		}
	}
}