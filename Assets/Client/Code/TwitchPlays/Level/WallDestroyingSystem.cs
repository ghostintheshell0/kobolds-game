using Leopotam.Ecs;
using UnityEngine;

public class WallDestroyingSystem : IEcsRunSystem
{
	private readonly EcsFilter<WallDestroyingComponent> filter = default;
	private readonly EcsFilter<MapComponent> maps = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var wallDestroyng = ref filter.Get1(i);
			var wallDestroyingEnt = filter.GetEntity(i);
			foreach (var m in maps)
			{
				ref var map = ref maps.Get1(m);
				if (map.IsOut(wallDestroyng.Position)) continue;

				var wallEnt = map.Walls[wallDestroyng.Position.x, wallDestroyng.Position.y];
				if (wallEnt.IsNull() || !wallEnt.Has<WallComponent>()) continue;

				ref var wall = ref wallEnt.Set<WallComponent>();
				ObjectPool.Recycle(wall.View);
				map.Walls[wallDestroyng.Position.x, wallDestroyng.Position.y] = EcsEntity.Null;
				wallEnt.Destroy();
			}

			wallDestroyingEnt.Unset<WallDestroyingComponent>();
		}
	}
}