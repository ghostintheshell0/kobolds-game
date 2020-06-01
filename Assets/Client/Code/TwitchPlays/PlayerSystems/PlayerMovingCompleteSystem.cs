using Leopotam.Ecs;

public class PlayerMovingCompleteSystem : IEcsRunSystem
{
	private readonly EcsFilter<PlayerComponent, MovingComponent> filter = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var player = ref filter.Get1(i);
			ref var moving = ref filter.Get2(i);
			ref var map = ref player.MapEntity.Set<MapComponent>();

			var worldPos = map.MapToWorld(moving.Target);
			if (player.View.transform.IsNear(worldPos, moving.StoppingDistance))
			{
				var e = filter.GetEntity(i);
				e.Unset<MovingComponent>();

				player.View.Animator.SetFloat(AnimationNames.Speed, 0f);
				continue;
			}
			var speed = player.View.Agent.velocity.magnitude;
			player.View.Animator.SetFloat(AnimationNames.Speed, speed);
			player.Position = moving.Target;
		}
	}
}