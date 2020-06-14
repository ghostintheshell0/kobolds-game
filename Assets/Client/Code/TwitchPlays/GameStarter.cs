using UnityEngine;
using Leopotam.Ecs;

public class GameStarter : MonoBehaviour
{
	[SerializeField] private LevelData levelData = default;
	[SerializeField] private GameData gameData = default;
	private RuntimeData runtimeData = default;

	private EcsWorld world;
	private EcsSystems systems;

    private void Start()
    {
		world = new EcsWorld();
		systems = new EcsSystems(world);

		runtimeData = new RuntimeData();
		levelData.DebugInput.World = world;
		levelData.TwitchConnectionUI.World = world;
		CreateSystems();

		systems.Init();
    }

	private void CreateSystems()
	{
#if UNITY_EDITOR
		Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(world);
		Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(systems);
#endif
		systems

			.Add(new TwitchIntegrationsSystems())
			.Add(new CommandsSystems())
			.Add(new LevelSystems())
			.Add(new PlayerSystems())
			.Add(runtimeData)

			.Inject(gameData)
			.Inject(levelData)
			.Inject(runtimeData);
		
	}

    private void Update()
    {
		systems.Run();
    }

	private void OnDestroy()
	{
		systems.Destroy();
		systems = null;
	}
}

public class TwitchIntegrationsSystems : IEcsRunSystem, IEcsInitSystem
{
	private readonly LevelData levelData = default;
	private readonly GameData gameData = default;
	private readonly EcsWorld world = default;
	private EcsSystems systems = default;


	public void Init()
	{
		systems = new EcsSystems(world);
		
		if(!gameData.IsDebug)
		{
			systems.Add(new TwitchConnectSystem());
		}

		systems
			.Add(new TwitchJoinSystem())
			.Add(new SendErrorsToTwitchSystem())
			.Add(new TwitchMessagesReceivingSystem())
			.Add(new HideTwitchConnectionUI())
			.Add(new LoadTwitchInfoSystem())

			.OneFrame<ErrorComponent>()
			.Inject(levelData)
			.Inject(gameData);

		systems.Init();
	}

	public void Run()
	{
		systems.Run();
	}
}

public class CommandsSystems : IEcsRunSystem, IEcsInitSystem
{
	private EcsSystems systems = default;
	private readonly EcsWorld world = default;
	private readonly GameData gameData = default;
	private readonly RuntimeData runtimeData = default;

	public void Init()
	{
		systems = new EcsSystems(world);

		systems
			.Add(new PlayerEnterSystem())
			.Add(new DirectionCommandsSystem())
			.Add(new StatsMessagesSystem())
			.Add(new HelpMessagesSystem())
			.Add(new ColorCommandSystem())
			.Add(new HeadSizeCommandSystem())
			.Add(new ExitCommandSystem())
			.Add(new UpgradeSystem())
			.Add(new TasksBreakSystem())
			.Add(new TaskCompleteSystems())
			.Add(new UseCommandSystem())
			.Add(new HatCommandSystem())

			.Inject(gameData.TwitchCommands)
			.Inject(runtimeData)
			.Inject(gameData)

			.OneFrame<MessageComponent>()
						
		.Init();
	}

	public void Run()
	{
		systems.Run();
	}
}

public class PlayerSystems : IEcsRunSystem, IEcsInitSystem
{
	private EcsSystems systems = default;
	private readonly EcsWorld world = default;
	private readonly GameData gameData = default;
	private readonly LevelData levelData = default;
	private readonly RuntimeData runtimeData = default;

	public void Init()
	{
		systems = new EcsSystems(world);

		systems
			.Add(new PlayerSpawnSystem())
			.Add(new PlayerTargetSystem())
			.Add(new PlayerMiningSystem())
			.Add(new PlayerMovingSystem())
			.Add(new PlayerStopMiningSystem())
			.Add(new PlayerStopMovingSystem())
			.Add(new SpawnEscapedPlayersSystem())
			.Add(new PlayerExitSystem())
			.Add(new HeadSizeChangingSystem())
			.Add(new PlayerMiningCompleteSystem())
			.Add(new PlayerMovingCompleteSystem())
			.Add(new MinigProgressSystem())
			.Add(new PlayerHudLookToCameraSystem())
			.Add(new SkinColorChangingSystem())
			.Add(new PlayerRemovingSystem())
			.Add(new EnableNavmeshAgentDelaySystem())
			.Add(new PlayerHudRemovingSystem())
			.Add(new LeaderBoardSystem())
			.Add(new PlayerUsingSystem())
			.Add(new ChangeHatSystem())
			.Add(new SavePlayersSystem())
			.Add(new LoadPlayersSystem())

			.Inject(gameData)
			.Inject(levelData)
			.Inject(runtimeData);

		systems.Init();
	}

	public void Run()
	{
		systems.Run();
	}
}

public class LevelSystems : IEcsRunSystem, IEcsInitSystem
{
	private EcsSystems systems = default;

	private readonly EcsWorld world = default;
	private readonly GameData gameData = default;
	private readonly LevelData levelData = default;
	private readonly RuntimeData runtimeData = default;

	public void Init()
	{
		systems = new EcsSystems(world);

		systems
			.Add(new StartGameSystem())
			.Add(new ClearLevelSystem())
			.Add(new LevelGenerationSystem())
			.Add(new EndGameSystem())
			.Add(new DoomSystem())
			.Add(new LevelTimerSystem())
			.Add(new EndGameTimerUpdaterSystem())
			.Add(new CameraControllerSystem())
			.Add(new MapsExploringSystem())
			.Add(new DebugLogErrorsSystem())
			.Add(new ChangeDebugUIState())
			.Add(new MapObjectsExploringSystem())
			.Add(new WallsExploringSystem())
			.Add(new WallDestroyingSystem())

			.Inject(gameData)
			.Inject(levelData)
			.Inject(runtimeData)

			.OneFrame<MessageComponent>()
			.OneFrame<ExploreComponent>()
		.Init();
	}

	public void Run()
	{
		systems.Run();
	}
}