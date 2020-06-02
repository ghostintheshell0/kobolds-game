using Leopotam.Ecs;
using UnityEngine;

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
			SpawnSavedPlayers();
			runtimeData.IsDoom = false;
			filter.GetEntity(i).Destroy();
		}
	}

	private void CreateLevel()
	{
		var e = world.NewEntity();
		ref var settings = ref e.Set<LevelGenerationComponent>();
		settings.Size = gameData.GenerationSettings.MapSize;
		settings.StartPoint = gameData.GenerationSettings.VisualSettings.StartPoint;
		settings.CellSize = gameData.GenerationSettings.VisualSettings.CellSize;
		settings.ExitTemlate = gameData.GenerationSettings.VisualSettings.ExitTemplate;
		settings.ExitBorederOffset = gameData.GenerationSettings.ExitBorderOffset;
		settings.Spawners = gameData.GenerationSettings.Spawners;
		settings.Walls = gameData.GenerationSettings.Walls;
	}

	private void CreateTimer()
	{

		var e = world.NewEntity();
		ref var timer = ref e.Set<LevelTimerComponent>();
		timer.View = levelData.LevelTimer;
		timer.Format = gameData.TimerFormat;
		timer.Time = gameData.RoundDuration;
	}

	private void SpawnSavedPlayers()
	{
		var e = world.NewEntity();
		ref var savedPlayers = ref e.Set<SpawnSavedPlayersComponent>();
	}
}
