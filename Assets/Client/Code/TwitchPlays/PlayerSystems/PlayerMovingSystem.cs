using Leopotam.Ecs;

public class PlayerMovingSystem : IEcsRunSystem
{
	private readonly EcsFilter<PlayerComponent, MovingCommandComponent> filter = default;
	private readonly GameData gameData = default;

	public void Run()
	{
		foreach(var i in filter)
		{
			ref var command = ref filter.Get2(i);
			var playerEnt = filter.GetEntity(i);
			ref var player = ref filter.Get1(i);
			ref var map = ref player.MapEntity.Set<MapComponent>();
			if(map.IsOut(command.TargetPosition))
			{
				playerEnt.Unset<MovingCommandComponent>();
				continue;
			}

			ref var moving = ref playerEnt.Set<MovingComponent>();

			var worldPos = map.MapToWorld(command.TargetPosition);
			moving.Target = command.TargetPosition;
			moving.Speed = gameData.PlayersData.MovingSpeed;
			moving.StoppingDistance = gameData.PlayersData.StoppingDistance;
			player.View.Agent.SetDestination(worldPos);

			playerEnt.Set<StopMiningComponent>();
			playerEnt.Unset<MovingCommandComponent>();
		}
	}
}