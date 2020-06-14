using Leopotam.Ecs;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerSpawnSystem : IEcsRunSystem
{
	private readonly EcsFilter<PlayerSpawnComponent> filter = default;
	private readonly RuntimeData runtimeData = default;
	private readonly LevelData levelData = default;
	private readonly GameData gameData = default;
	private readonly EcsWorld world = default;

	public void Run()
	{
		foreach(var i in filter)
		{
			ref var spawnData = ref filter.Get1(i);
			ref var map = ref runtimeData.GetMap(spawnData.MapIndex);

			if (map.Spawners.Count > 0)
			{
				var spawner = GetSpawner(ref map);
				var playerEnt = SpawnPlayer(spawnData, spawner);
				ref var player = ref playerEnt.Set<PlayerComponent>();
				ClearSpawner(spawner);
				runtimeData.AddPlayer(playerEnt);

				if (!runtimeData.IsSavedPlayer(player.Stats.Name))
				{
					runtimeData.SavePlayer(player.Stats);
				}
			}
			else
			{
				Debug.Log("No have spawn points");
			}

			filter.GetEntity(i).Destroy();
		}
	}

	private SpawnerComponent GetSpawner(ref MapComponent map)
	{
		if(map.FreeSpawners.Count == 0)
		{
			map.FreeSpawners.AddRange(map.Spawners);
		}

		var count = map.FreeSpawners.Count;
		var index = Random.Range(0, count);

		var spawnerEnt = map.FreeSpawners[index];
		map.FreeSpawners.RemoveAt(index);
		ref var spawner = ref spawnerEnt.Set<SpawnerComponent>();

		return spawner;
	}

	private EcsEntity SpawnPlayer(PlayerSpawnComponent spawnData, in SpawnerComponent spawner)
	{
		var playerEnt = world.NewEntity();
		ref var player = ref playerEnt.Set<PlayerComponent>();
		ref var map = ref spawner.MapEnt.Set<MapComponent>(); 
		player.View = ObjectPool.Spawn(levelData.PlayerTemplate);
		player.Position = spawner.MapPosition;
		player.View.transform.position = map.MapToWorld(player.Position);
		player.MapEntity = spawner.MapEnt;
		player.Stats = spawnData.Stats;
		player.Stats.WallsDestroyedInCurrentGame = 0;

		ref var changeHat = ref playerEnt.Set<ChangeHatComponent>();
		changeHat.HatIndex = player.Stats.CurrentHatIndex;

		ref var skinComponent = ref playerEnt.Set<SkinComponent>();
		skinComponent.View = player.View.Skin;
/*
		ref var skinColor = ref playerEnt.Set<SkinColorChangerComponent>();
		skinColor.Color = spawnData.Stats.SkinColor;
		*/
		ref var headSize = ref playerEnt.Set<HeadSizeChangerComponent>();
		headSize.Size = spawnData.Stats.HeadSize;

		ref var enableNavmeshAgentDelay = ref playerEnt.Set<EnableNavmeshAgentDelayComponent>();
		enableNavmeshAgentDelay.Frames = gameData.PlayersData.EnableAgentFramesDelay;

		var playerHudEnt = world.NewEntity();
		ref var playerHud = ref playerHudEnt.Set<PlayerHudComponent>();
		playerHud.View = ObjectPool.Spawn(levelData.PlayerHUDTemplate);
		playerHud.View.Canvas.worldCamera = levelData.Camera;
		playerHud.Target = playerEnt;
		playerHud.View.PlayerNameField.text = player.Stats.Name;
		playerHud.View.Progress.gameObject.SetActive(false);
		playerHud.View.Canvas.transform.SetParent(player.View.transform);
		playerHud.View.transform.localPosition = playerHud.View.Offset;

		return playerEnt;
	}


	private void ClearSpawner(in SpawnerComponent spawner)
	{
		var exploeEnt = world.NewEntity();
		ref var explored = ref exploeEnt.Set<ExploreComponent>();
		explored.Position = spawner.MapPosition;
		explored.Value = true;

		var wallDestroyingEnt = world.NewEntity();
		ref var wallDestroying = ref wallDestroyingEnt.Set<WallDestroyingComponent>();
		wallDestroying.Position = spawner.MapPosition;
	}

}