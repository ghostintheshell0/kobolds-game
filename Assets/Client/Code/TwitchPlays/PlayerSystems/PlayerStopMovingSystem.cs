using Leopotam.Ecs;

public class PlayerStopMovingSystem : IEcsRunSystem
{
	private readonly EcsFilter<PlayerComponent, StopMovingComponent> filter = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var playerEnt = ref filter.GetEntity(i);

			if (playerEnt.Has<MovingComponent>())
			{
				ref var player = ref filter.Get1(i);
				player.View.Animator.SetFloat(AnimationNames.Speed, 0f);
				player.View.Agent.SetDestination(player.View.transform.position);
				playerEnt.Unset<MovingComponent>();
			}

			playerEnt.Unset<StopMovingComponent>();
		}
	}
}