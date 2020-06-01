using Leopotam.Ecs;
using UnityEngine;

public class MapsExploringSystem : IEcsRunSystem
{
	private readonly EcsFilter<ExploreComponent> filter = default;
	private readonly EcsFilter<MapComponent> maps = default;
	private readonly EcsWorld world = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var explored = ref filter.Get1(i);
			var exploredEnt = filter.GetEntity(i);
			foreach (var k in maps)
			{
				ref var map = ref maps.Get1(i);
				if (map.Explored[explored.Position.x, explored.Position.y] == explored.Value) continue;

				map.Explored[explored.Position.x, explored.Position.y] = explored.Value;

				ref var exit = ref map.Exit.Set<ExitComponent>();
				if (IsNeighbord(exit.Position, explored.Position))
				{
					var wallEnt = map.Walls[exit.Position.x, exit.Position.y];
					if (wallEnt.IsAlive())
					{
						ref var wall = ref wallEnt.Set<WallComponent>();

						var destrEnt = world.NewEntity();
						ref var destr = ref destrEnt.Set<WallDestroyingComponent>();
						destr.Position = wall.Position;

						ObjectPool.Recycle(wall.View);
					}

					exit.View.gameObject.SetActive(true);
				}
			}
			exploredEnt.Unset<ExploreComponent>();
		}
	}

	private bool IsNeighbord(Vector2Int a, Vector2Int b)
	{
		return (a - b).sqrMagnitude <= 1;
	}
}