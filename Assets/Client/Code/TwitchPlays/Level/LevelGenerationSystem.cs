using UnityEngine;
using Leopotam.Ecs;

public class LevelGenerationSystem : IEcsRunSystem
{
	private readonly EcsFilter<LevelGenerationComponent> filter = default;
	private readonly EcsWorld world = default;

	public void Run()
	{
		foreach(var i in filter)
		{
			ref var settings = ref filter.Get1(i);
			Generate(settings);
			filter.GetEntity(i).Destroy();
		}
	}

	private void Generate(in LevelGenerationComponent settings)
	{
		var mapEnt = world.NewEntity();
		ref var map = ref mapEnt.Set<MapComponent>();

		SetMapValues(ref map, settings);
		GenerateWalls(ref map, settings);
		GenerateExit(ref map, settings);
		GenerateSpawners(mapEnt, settings);
	}

	private void SetMapValues(ref MapComponent map, in LevelGenerationComponent settings)
	{
		map.Explored = new bool[settings.Size.x, settings.Size.y];
		map.Walls = new EcsEntity[settings.Size.x, settings.Size.y];
		map.CellSize = settings.CellSize;
		map.MapSize = settings.Size;
		map.Position = settings.StartPoint;
	}

	private void GenerateWalls(ref MapComponent map, in LevelGenerationComponent settings)
	{
		for (int ix = 0; ix < settings.Size.x; ++ix)
		{
			for (int iy = 0; iy < settings.Size.y; ++iy)
			{
				var e = world.NewEntity();
				ref var wall = ref e.Set<WallComponent>();
				var pos = new Vector2Int(ix, iy);
				var randomWallTemplate = settings.Walls.GetRandomItem();

				wall.Position = pos;
				wall.Heals = Random.Range(randomWallTemplate.MinHp, randomWallTemplate.MaxHp);
				wall.Ore = Random.Range(randomWallTemplate.MinOre, randomWallTemplate.MaxOre);
				wall.View = ObjectPool.Spawn(randomWallTemplate.Prefab);
				wall.View.transform.position = map.MapToWorld(pos);
				map.Explored[ix, iy] = false;
				map.Walls[ix, iy] = e;
			}
		}
	}

	private void GenerateExit(ref MapComponent map, in LevelGenerationComponent settings)
	{
		var x = Random.Range(settings.ExitBorederOffset, settings.Size.x - settings.ExitBorederOffset);
		var y = Random.Range(settings.ExitBorederOffset, settings.Size.y - settings.ExitBorederOffset);
		var pos = new Vector2Int(x, y);
		

		var e = world.NewEntity();
		ref var exit = ref e.Set<ExitComponent>();
		exit.Position = pos;
		exit.View = ObjectPool.Spawn(settings.ExitTemlate);
		exit.View.transform.position = map.MapToWorld(pos);
		exit.View.gameObject.SetActive(false);
		map.Exit = e;
		Debug.Log($"exit in {exit.Position}");
	}

	private void GenerateSpawners(EcsEntity mapEnt, LevelGenerationComponent settings)
	{
		var p1 = new Vector2Int(0, 0);
		var p2 = new Vector2Int(0, settings.Size.y);

		var p3 = new Vector2Int(0, settings.Size.y - 1);
		var p4 = new Vector2Int(settings.Size.x, settings.Size.y - 1);

		var p5 = new Vector2Int(settings.Size.x - 1, settings.Size.y - 1);
		var p6 = new Vector2Int(settings.Size.x - 1, 0);

		CreateSpawnLine(p1, p2, settings.Spawners / 4, mapEnt);
		CreateSpawnLine(p3, p4, settings.Spawners / 4, mapEnt);
		CreateSpawnLine(p5, p6, settings.Spawners / 4, mapEnt);
		CreateSpawnLine(p6, p1, settings.Spawners / 4, mapEnt);
	}

	private void CreateSpawnLine(Vector2Int from, Vector2Int to, int count, EcsEntity mapEnt)
	{
		if (count <= 0)
		{
			Debug.LogWarning("Add more spawners. total: " + count);
			return;
		}

		var xStep = (to.x - from.x) / count;
		var yStep = (to.y - from.y) / count;
		var step = new Vector2Int(xStep, yStep);

		for (int i = 0; i < count; ++i)
		{
			var e = world.NewEntity();
			ref var spawner = ref e.Set<SpawnerComponent>();
			spawner.MapPosition = from + step * i;
			spawner.Map = mapEnt;
		}
	}
}