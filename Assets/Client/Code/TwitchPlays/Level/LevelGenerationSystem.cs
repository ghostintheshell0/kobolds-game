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
			ref var generation = ref filter.Get1(i);
			Generate(generation.Settings);
			filter.GetEntity(i).Destroy();
		}
	}

	private void Generate(GenerationSettings settings)
	{
		var mapEnt = world.NewEntity();
		ref var map = ref mapEnt.Set<MapComponent>();
		map.Entity = mapEnt;

		SetMapValues(ref map, settings);
		GenerateWalls(ref map, settings.Walls);
		GenerateExit(ref map, settings);
		GenerateSpawners(ref map, settings);
	}

	private void SetMapValues(ref MapComponent map, GenerationSettings settings)
	{
		var size = settings.MapSize;
		map.Explored = new bool[size.x, size.y];
		map.Walls = new EcsEntity[size.x, size.y];
		map.Size = size;
		map.CellSize = settings.VisualSettings.CellSize;
		map.Position = settings.VisualSettings.StartPoint;
	}

	private void GenerateWalls(ref MapComponent map, RandomWallList walls)
	{
		for (int ix = 0; ix < map.Size.x; ++ix)
		{
			for (int iy = 0; iy < map.Size.y; ++iy)
			{
				var e = world.NewEntity();
				ref var wall = ref e.Set<WallComponent>();
				var pos = new Vector2Int(ix, iy);
				var randomWallTemplate = walls.GetRandomItem();

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

	private void GenerateExit(ref MapComponent map, GenerationSettings settings)
	{
		var x = Random.Range(settings.ExitBorderOffset, map.Size.x - settings.ExitBorderOffset);
		var y = Random.Range(settings.ExitBorderOffset, map.Size.y - settings.ExitBorderOffset);
		var pos = new Vector2Int(x, y);
		

		var e = world.NewEntity();
		ref var exit = ref e.Set<ExitComponent>();
		exit.Position = pos;
		exit.View = ObjectPool.Spawn(settings.VisualSettings.ExitTemplate);
		exit.View.transform.position = map.MapToWorld(pos);
		exit.View.gameObject.SetActive(false);
		map.Exit = e;
		Debug.Log($"exit in {exit.Position}");
	}

	private void GenerateSpawners(ref MapComponent map, GenerationSettings settings)
	{
		var p1 = new Vector2Int(0, 0);
		var p2 = new Vector2Int(0, map.Size.y);

		var p3 = new Vector2Int(0, map.Size.y - 1);
		var p4 = new Vector2Int(map.Size.x, map.Size.y - 1);

		var p5 = new Vector2Int(map.Size.x - 1, map.Size.y - 1);
		var p6 = new Vector2Int(map.Size.x - 1, 0);

		var spawnersPerLine = settings.Spawners / 4;

		CreateSpawnLine(p1, p2, spawnersPerLine, ref map);
		CreateSpawnLine(p3, p4, spawnersPerLine, ref map);
		CreateSpawnLine(p5, p6, spawnersPerLine, ref map);
		CreateSpawnLine(p6, p1, spawnersPerLine, ref map);
	}

	private void CreateSpawnLine(Vector2Int from, Vector2Int to, int count, ref MapComponent map)
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
			spawner.MapEnt = map.Entity;
		}
	}
}