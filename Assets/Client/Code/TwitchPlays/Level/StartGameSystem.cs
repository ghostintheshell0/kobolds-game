using Leopotam.Ecs;

public class StartGameSystem : IEcsRunSystem
{
	private readonly EcsWorld world = default;
	private readonly GameData gameData = default;
	private readonly LevelData levelData = default;
	private readonly RuntimeData runtimeData = default;

	private readonly EcsFilter<StartGameComponent> filter = default;

	public void Run()
	{
		foreach(var i in filter)
		{
			CreateLevel();
			CreateTimer();
			SpawnEscapedPlayers();
			runtimeData.IsDoom = false;
			runtimeData.ClearPlayersInLastGame();
			filter.GetEntity(i).Destroy();
		}
	}

	private void CreateLevel()
	{
		var e = world.NewEntity();
		ref var generation = ref e.Set<LevelGenerationComponent>();
		generation.Settings = gameData.GenerationSettings;
	}

	private void CreateTimer()
	{

		var e = world.NewEntity();
		ref var timer = ref e.Set<LevelTimerComponent>();
		timer.View = levelData.LevelTimer;
		timer.Format = gameData.TimerFormat;
		timer.Time = gameData.RoundDuration;
	}

	private void SpawnEscapedPlayers()
	{
		var e = world.NewEntity();
		ref var escapedPlayers = ref e.Set<SpawnSavedPlayersComponent>();
	}
}