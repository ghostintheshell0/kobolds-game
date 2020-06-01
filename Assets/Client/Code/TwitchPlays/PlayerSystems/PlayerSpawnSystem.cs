using Leopotam.Ecs;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerSpawnSystem : IEcsRunSystem
{
	private readonly EcsFilter<PlayerSpawnComponent> filter = default;
	private readonly EcsFilter<SpawnerComponent> spawners = default;
	private readonly RuntimeData runtimeData = default;
	private readonly LevelData levelData = default;
	private readonly GameData gameData = default;
	private readonly EcsWorld world = default;

	public void Run()
	{
		foreach(var i in filter)
		{
			var count = spawners.GetEntitiesCount();
			if (count <= 0)
			{
				Debug.Log("No have spawn points");
				continue;
			}

			ref var spawnComponent = ref filter.Get1(i);
			ref var spawner = ref GetSpawner();
			SpawnPlayer(spawnComponent, spawner);
			ClearSpawner(spawner);
			filter.GetEntity(i).Destroy();
		}
	}

	private ref SpawnerComponent GetSpawner()
	{
		var count = spawners.GetEntitiesCount();
		ref var spawner = ref spawners.Get1(Random.Range(0, count));
		return ref spawner;
	}

	private void SpawnPlayer(PlayerSpawnComponent spawnData, in SpawnerComponent spawner)
	{
		var playerEnt = world.NewEntity();
		ref var player = ref playerEnt.Set<PlayerComponent>();
		ref var map = ref spawner.Map.Set<MapComponent>(); 
		player.View = ObjectPool.Spawn(levelData.PlayerTemplate);
		player.Position = spawner.MapPosition;
		player.View.transform.position = map.MapToWorld(player.Position);
		player.MapEntity = spawner.Map;
		player.Stats = spawnData.Stats;

		ref var skinComponent = ref playerEnt.Set<SkinComponent>();
		skinComponent.View = player.View.Skin;

		ref var skinColor = ref playerEnt.Set<SkinColorChangerComponent>();
		skinColor.Color = spawnData.Stats.SkinColor;

		ref var headSize = ref playerEnt.Set<HeadSizeChangerComponent>();
		headSize.Size = spawnData.Stats.HeadSize;

		ref var enableNavmeshAgentDelay = ref playerEnt.Set<EnableNavmeshAgentDelayComponent>();
		enableNavmeshAgentDelay.Frames = gameData.PlayersData.EnableAgentFramesDelay;

		var playerHudEnt = world.NewEntity();
		ref var playerHud = ref playerHudEnt.Set<PlayerHudComponent>();
		playerHud.View = ObjectPool.Spawn(levelData.PlayerHUDTemplate);
		playerHud.View.Canvas.worldCamera = levelData.Camera;
		playerHud.Target = playerEnt;
		playerHud.Offset = levelData.PlayerHUDOffset;
		playerHud.View.PlayerNameField.text = player.Stats.Name;
		playerHud.View.Progress.gameObject.SetActive(false);
		playerHud.View.Canvas.transform.SetParent(player.View.transform);
		playerHud.View.transform.localPosition = playerHud.Offset;

		runtimeData.AddPlayer(playerEnt);
	}


	private void ClearSpawner(in SpawnerComponent spawner)
	{
		var e = world.NewEntity();
		ref var explored = ref e.Set<ExploreComponent>();
		explored.Position = spawner.MapPosition;
		explored.Value = true;

		ref var wallDestroying = ref e.Set<WallDestroyingComponent>();
		wallDestroying.Position = spawner.MapPosition;
	}

}