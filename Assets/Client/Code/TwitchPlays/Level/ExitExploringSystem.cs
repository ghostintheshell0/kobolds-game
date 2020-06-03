using Leopotam.Ecs;
using UnityEngine;

public class ExitExploringSystem : IEcsRunSystem
{
	private readonly EcsFilter<ExploreComponent> filter = default;
	private readonly EcsFilter<ExitComponent> exits = default;
	private readonly EcsWorld world = default;

	public void Run()
	{
		foreach(var i in filter)
		{
			ref var explored = ref filter.Get1(i);

			foreach (var k in exits)
			{
				ref var exit = ref exits.Get1(k);

				if (explored.Value == true && IsNeighbord(exit.Position, explored.Position))
				{
					var destrEnt = world.NewEntity();
					ref var destr = ref destrEnt.Set<WallDestroyingComponent>();
					destr.Position = exit.Position;
					exit.View.gameObject.SetActive(true);
				}
			}
			
		}
	}

	private bool IsNeighbord(Vector2Int a, Vector2Int b)
	{
		return (a - b).sqrMagnitude <= 1;
	}
}