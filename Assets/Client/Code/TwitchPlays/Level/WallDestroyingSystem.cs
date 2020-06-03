using Leopotam.Ecs;

public class WallDestroyingSystem : IEcsRunSystem
{
	private readonly EcsFilter<WallDestroyingComponent> filter = default;
	private readonly EcsFilter<MapComponent> maps = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var wallDestroyng = ref filter.Get1(i);
			ref var wallDestroyingEnt = ref filter.GetEntity(i);
			foreach (var m in maps)
			{
				ref var map = ref maps.Get1(m);
				if (map.IsOut(wallDestroyng.Position))
				{
					continue;
				}
				ref var wallEnt = ref map.Walls[wallDestroyng.Position.x, wallDestroyng.Position.y];
				if (!wallEnt.IsAlive() || !wallEnt.Has<WallComponent>())
				{
					continue;
				}

				ref var wall = ref wallEnt.Set<WallComponent>();
				ObjectPool.Recycle(wall.View);
				wallEnt.Destroy();
			}

			wallDestroyingEnt.Unset<WallDestroyingComponent>();
		}
	}
}