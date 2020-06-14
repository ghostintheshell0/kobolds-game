using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class ClearLevelSystem : IEcsRunSystem
{
	private readonly EcsFilter<ClearLevelComponent> filter = default;

	private readonly EcsFilter<PlayerComponent> players = default;
	private readonly EcsFilter<PlayerHudComponent> huds = default;
	private readonly EcsFilter<WallComponent> walls = default;
	private readonly EcsFilter<SpawnerComponent> spawners = default;
	private readonly EcsFilter<MapComponent> maps = default;
	private readonly RuntimeData runtimeData = default;

	private readonly EcsWorld world = default;

	public void Run()
	{
		foreach(var i in filter)
		{
			RemovePlayers();
			RemoveHuds();
			RemoveWalls();
			RemoveSpawners();

			ClearMaps();

			filter.GetEntity(i).Destroy();

			var e = world.NewEntity();
			e.Set<StartGameComponent>();
		}
	}


	private void RemoveWalls()
	{
		foreach (var i in walls)
		{
			ref var wall = ref walls.Get1(i);
			ObjectPool.Recycle(wall.View);
			var e = walls.GetEntity(i);
			e.Destroy();
		}
	}

	private void RemoveSpawners()
	{
		foreach(var i in spawners)
		{
			ref var e = ref spawners.GetEntity(i);
			e.Destroy();
		}
	}

	private void RemovePlayers()
	{
		foreach (var i in players)
		{
			ref var player = ref players.Get1(i);
			ObjectPool.Recycle(player.View);

			player.Stats.CurrentOre = 0;
			player.Stats.WallsDestroyedInCurrentGame = 0;
			player.Stats.Level = 1;

			ref var e = ref players.GetEntity(i);
			runtimeData.ClearLivePlayers();
			e.Destroy();
		}
	}

	private void RemoveHuds()
	{
		foreach (var i in huds)
		{
			ref var hud = ref huds.Get1(i);
			ObjectPool.Recycle(hud.View);

			ref var e = ref huds.GetEntity(i);
			e.Destroy();
		}
	}

	private void ClearMaps()
	{

		foreach (var i in maps)
		{
			ref var ent = ref maps.GetEntity(i);
			ref var map = ref ent.Set<MapComponent>();
			RemoveObjects(map.Objects);
			ent.Destroy();
		}
	}

	private void RemoveObjects(List<EcsEntity> objects)
	{
		for(int i = 0; i < objects.Count; ++i)
		{
			ref var mapObj = ref objects[i].Set<MapObjectComponent>();
			ObjectPool.Recycle(mapObj.View);
			objects[i].Destroy();
		}
	}
}
