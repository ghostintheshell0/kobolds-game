using Leopotam.Ecs;
using UnityEngine;

public class PlayerMiningCompleteSystem : IEcsRunSystem
{
	private readonly EcsFilter<PlayerComponent, MiningComponent> filter = default;
	private readonly EcsWorld world = default;
	private readonly GameData gameData = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var player = ref filter.Get1(i);
			ref var mining = ref filter.Get2(i);

			var playerEnt = filter.GetEntity(i);

			mining.TimeLeft += Time.deltaTime;

			if(!mining.Target.IsAlive())
			{
				playerEnt.Set<StopMiningComponent>();
				continue;
			}

			ref var wall = ref mining.Target.Set<WallComponent>();

			if (mining.Progress < 1f)
			{
				var damage = player.Stats.Level * gameData.PlayersData.DamageMultipler;
				mining.Progress += damage * Time.deltaTime / wall.Heals;
				continue;
			}

			player.Stats.Ore += wall.Ore;
			player.Stats.WallsDestroyed++;
			player.Stats.WallsDestroyedInCurrentGame++;

			var destroyWallEnt = world.NewEntity();
			ref var destroyWall = ref destroyWallEnt.Set<WallDestroyingComponent>();
			destroyWall.Position = wall.Position;

			var exploringEnt = world.NewEntity();
			ref var exploring = ref exploringEnt.Set<ExploreComponent>();
			exploring.Position = wall.Position;
			exploring.Value = true;

			playerEnt.Set<StopMiningComponent>();

			ref var moving = ref playerEnt.Set<MovingCommandComponent>();
			moving.TargetPosition = wall.Position;

		}
	}
}