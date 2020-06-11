using UnityEngine;
using Leopotam.Ecs;
using System.Collections.Generic;

public class LevelGenerationSystem : IEcsRunSystem, IEcsInitSystem
{
	private readonly EcsFilter<LevelGenerationComponent> filter = default;
	private readonly EcsWorld world = default;
	private Dictionary<MapObjectType, System.Action<EcsEntity>> mapObjectSpecialComponents;

	public void Init()
	{
		mapObjectSpecialComponents = new Dictionary<MapObjectType, System.Action<EcsEntity>>()
		{
			{MapObjectType.Exit, AddExitComponent },
			{MapObjectType.Chest, AddChestComponent },
		};
	}

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
		GenerateObjects(ref map, settings);
		GenerateSpawners(ref map, settings);
	}

	private void SetMapValues(ref MapComponent map, GenerationSettings settings)
	{
		var size = settings.MapSize;
		map.Explored = new bool[size.x, size.y];
		map.Walls = new EcsEntity[size.x, size.y];
		map.Size = size;
		map.CellSize = settings.CellSize;
		map.Position = settings.StartPoint;
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
				wall.DisableAllPlanes();
				map.Explored[ix, iy] = false;
				map.Walls[ix, iy] = e;
			}
		}

		EnableBorderWallPlanes(ref map);
	}

	private void EnableBorderWallPlanes(ref MapComponent map)
	{
		var lastX = map.Size.x - 1;
		var lastY = map.Size.y - 1;

		for (var x = 0; x < map.Size.x; ++x)
		{
			ref var wall = ref map.Walls[x, 0];
			wall.Set<WallComponent>().EnablePlane(Vector2Int.down);
			wall = ref map.Walls[x, lastY];
			wall.Set<WallComponent>().EnablePlane(Vector2Int.up);
		}

		for (var y = 0; y < map.Size.y; ++y)
		{
			ref var wall = ref map.Walls[0, y];
			wall.Set<WallComponent>().EnablePlane(Vector2Int.left);
			wall = ref map.Walls[lastX, y];
			wall.Set<WallComponent>().EnablePlane(Vector2Int.right);
		}
	}

	private void GenerateObjects(ref MapComponent map, GenerationSettings settings)
	{
		for(var i = 0; i < settings.MapObjects.List.Count; ++i)
		{
			var objData = settings.MapObjects.List[i];
			for(int k = 0; k < objData.Count; ++k)
			{
				var chance = Random.Range(0, 1f);
				if (objData.Chance < chance) continue;
				GenerateObject(ref map, objData);
			}
		}
	}

	private void GenerateObject(ref MapComponent map, MapObject objData)
	{

		var x = Random.Range(objData.BordersOffset, map.Size.x - objData.BordersOffset);
		var y = Random.Range(objData.BordersOffset, map.Size.y - objData.BordersOffset);
		var pos = new Vector2Int(x, y);
		
		var objEnt = world.NewEntity();
		ref var mapObj = ref objEnt.Set<MapObjectComponent>();
		mapObj.Position = pos;
		mapObj.View = ObjectPool.Spawn(objData.Template);
		mapObj.View.transform.position = map.MapToWorld(pos);
		mapObj.View.gameObject.SetActive(objData.Visible);
		mapObj.Visible = objData.Visible;
		mapObj.CanBeUnderWall = objData.CanBeUnderWall;
		mapObj.MapObjectType = objData.MapObjectType;

		if (mapObj.Visible)
		{
			var destrEnt = world.NewEntity();
			ref var wallDestr = ref destrEnt.Set<WallDestroyingComponent>();
			wallDestr.Position = pos;
			ref var explore = ref destrEnt.Set<ExploreComponent>();
			explore.Position = pos;
			explore.Value = true;
		}

		AddSpecialComponents(objEnt, ref mapObj);
		Debug.Log($"{mapObj.View.name} in {mapObj.Position}");
	}

	private void AddSpecialComponents(EcsEntity objEnt, ref MapObjectComponent mapObj)
	{
		var type = mapObj.MapObjectType;

		mapObjectSpecialComponents[type](objEnt);

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

	private void AddExitComponent(EcsEntity ent)
	{
		ent.Set<ExitComponent>();
	}

	private void AddChestComponent(EcsEntity ent)
	{

	}
}
