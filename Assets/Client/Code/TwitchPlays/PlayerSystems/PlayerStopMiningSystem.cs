using Leopotam.Ecs;
using UnityEngine;

public class PlayerStopMiningSystem : IEcsRunSystem
{
	private readonly EcsFilter<PlayerComponent, StopMiningComponent> filter = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var player = ref filter.Get1(i);
			ref var playerEnt = ref filter.GetEntity(i);
			if(playerEnt.Has<MiningComponent>())
			{
				ref var mining = ref playerEnt.Set<MiningComponent>();
				if(mining.Target.IsAlive())
				{
					ref var targetWall = ref mining.Target.Set<WallComponent>();
					targetWall.Locked = false;
				}
			}
			player.View.Animator.SetBool(AnimationNames.Mining, false);
			playerEnt.Unset<StopMiningComponent>();
			playerEnt.Unset<MiningComponent>();

		}

	}
}
