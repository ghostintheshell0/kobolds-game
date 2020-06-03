using Leopotam.Ecs;

public class MapsExploringSystem : IEcsRunSystem
{
	private readonly EcsFilter<ExploreComponent> filter = default;
	private readonly EcsFilter<MapComponent> maps = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var explored = ref filter.Get1(i);
			var exploredEnt = filter.GetEntity(i);

			foreach (var k in maps)
			{
				ref var map = ref maps.Get1(k);
				map.Explored[explored.Position.x, explored.Position.y] = explored.Value;
			}
		}
	}
}