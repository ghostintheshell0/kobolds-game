using Leopotam.Ecs;
using UnityEngine;

public class PlayerMiningSystem : IEcsRunSystem
{
	private readonly EcsFilter<PlayerComponent, MiningCommandComponent> filter = default;
	private readonly GameData gameData = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var player = ref filter.Get1(i);
			ref var command = ref filter.Get2(i);

			if (!IsReadyForMining(command.Target)) continue;

			ref var wall = ref command.Target.Set<WallComponent>();
			if (wall.Locked) continue;

			var playerEnt = filter.GetEntity(i);

			if(playerEnt.Has<MiningComponent>())
			{
				ref var mining = ref playerEnt.Set<MiningComponent>();
				var isNewTarget = command.Target != mining.Target;

				Debug.Log("already mining");
				if(isNewTarget)
				{
					mining.TimeLeft = 0f;
					mining.Progress = 0f;
					ref var oldTargetWall = ref mining.Target.Set<WallComponent>();
					oldTargetWall.Locked = false;
				}

				wall.Locked = true;
				mining.Target = command.Target;
			}
			else
			{
				playerEnt.Set<StopMovingComponent>();
				ref var mining = ref playerEnt.Set<MiningComponent>();
				wall.Locked = true;
				mining.Target = command.Target;

			}
			var variant = Random.Range(0, gameData.PlayersData.PickaxeAnimationVariants);
			player.View.Animator.SetBool(AnimationNames.Mining, true);
			player.View.Animator.SetFloat(AnimationNames.AttackVariant, variant);
			player.View.transform.LookAt(wall.View.transform.position);
			playerEnt.Unset<MiningCommandComponent>();
		}
	}

	private bool IsReadyForMining(EcsEntity wallEnt)
	{
		if(!wallEnt.IsAlive())
		{
			return false;
		}

		if (!wallEnt.Has<WallComponent>())
		{
			return false;
		}

		ref var wall = ref wallEnt.Set<WallComponent>();

		if (wall.Locked)
		{
			return false;
		}

		return true;
	}
}
