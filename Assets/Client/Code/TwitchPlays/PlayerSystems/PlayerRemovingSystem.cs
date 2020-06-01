using Leopotam.Ecs;

public class PlayerRemovingSystem : IEcsRunSystem
{
	private readonly EcsFilter<PlayerComponent, RemovingComponent> filter = default;
	private readonly RuntimeData runtimeData = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			var playerEnt = filter.GetEntity(i);
			ref var player = ref filter.Get1(i);
			ObjectPool.Recycle(player.View);
			if(playerEnt.Has<MiningComponent>())
			{
				ref var mining = ref playerEnt.Set<MiningComponent>();
				ref var targetWall = ref mining.Target.Set<WallComponent>();
				targetWall.Locked = false;
			}

			runtimeData.RemovePlayer(player.Stats.Name);
			playerEnt.Destroy();
		}
	}
}