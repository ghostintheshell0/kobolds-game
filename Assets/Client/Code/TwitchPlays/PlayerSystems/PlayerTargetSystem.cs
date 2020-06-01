using Leopotam.Ecs;

public class PlayerTargetSystem : IEcsRunSystem
{
	private readonly EcsFilter<PlayerComponent, TargetComponent> filter = default;

	public void Run()
	{
		foreach(var i in filter)
		{
			ref var player = ref filter.Get1(i);
			ref var map = ref player.MapEntity.Set<MapComponent>();
			ref var targetComponent = ref filter.Get2(i);
			var target = targetComponent.Direction + player.Position;
			if (!map.IsOut(target))
			{
				var playerEnt = filter.GetEntity(i);
				if (map.Walls[target.x, target.y].IsAlive())
				{
					ref var mining = ref playerEnt.Set<MiningCommandComponent>();
					mining.Target = map.Walls[target.x, target.y];
				}
				else
				{
					ref var moving = ref playerEnt.Set<MovingCommandComponent>();
					moving.TargetPosition = target;
				}
			}

			filter.GetEntity(i).Unset<TargetComponent>();
		}
	}
}
