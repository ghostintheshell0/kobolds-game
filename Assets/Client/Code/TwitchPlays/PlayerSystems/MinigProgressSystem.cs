using Leopotam.Ecs;
using UnityEngine;

public class MinigProgressSystem : IEcsRunSystem
{
	private readonly EcsFilter<PlayerHudComponent> filter = default;

	public void Run()
	{
		foreach(var i in filter)
		{
			ref var hud = ref filter.Get1(i);

			if(!hud.Target.IsAlive())
			{
				filter.GetEntity(i).Destroy();
				continue;
			}

			var isMining = hud.Target.Has<MiningComponent>();
			hud.View.Progress.gameObject.SetActive(isMining);
			if(!isMining)
			{
				continue;
			}
			ref var mining = ref hud.Target.Set<MiningComponent>();

			hud.View.Progress.value = mining.Progress;
		}
	}
}