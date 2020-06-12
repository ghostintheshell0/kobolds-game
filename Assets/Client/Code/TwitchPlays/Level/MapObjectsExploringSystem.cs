using Leopotam.Ecs;
using UnityEngine;

public class MapObjectsExploringSystem : IEcsRunSystem
{
	private readonly EcsFilter<ExploreComponent> filter = default;
	private readonly EcsFilter<MapObjectComponent> objects = default;
	private readonly EcsWorld world = default;

	public void Run()
	{
		foreach(var i in filter)
		{
			ref var explore = ref filter.Get1(i);

			foreach (var k in objects)
			{
				ref var mapObj = ref objects.Get1(k);

				if (IsReadyForShow(ref mapObj, ref explore))
				{
					var destrEnt = world.NewEntity();
					ref var destr = ref destrEnt.Set<WallDestroyingComponent>();
					destr.Position = mapObj.Position;
					mapObj.View.gameObject.SetActive(true);
				}
			}
			
		}
	}
	private bool IsReadyForShow(ref MapObjectComponent obj, ref ExploreComponent explore)
	{
		if (explore.Value == false) return false;

		if(IsNeighbord(obj.Position, explore.Position) && !obj.CanBeUnderWall)
		{
			return true;
		}

		if(obj.Position == explore.Position)
		{
			return true;
		}

		return false;
	}

	private bool IsNeighbord(Vector2Int a, Vector2Int b)
	{
		return (a - b).sqrMagnitude <= 1;
	}

}