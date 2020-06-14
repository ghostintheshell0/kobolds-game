using Leopotam.Ecs;
using System;
using UnityEngine;

public class LevelTimerSystem : IEcsRunSystem
{
	private readonly EcsFilter<LevelTimerComponent> filter = default;
	private readonly EcsWorld world = default;

	public void Run()
	{
		foreach(var i in filter)
		{
			ref var timer = ref filter.Get1(i);
			timer.Time -= Time.deltaTime;
			if(timer.Time <= 0f)
			{
				var e = world.NewEntity();
				ref var end = ref e.Set<EndGameComponent>();
				var timerEnt = filter.GetEntity(i);
				timerEnt.Destroy();
				timer.Time = 0f;
				continue;
			}

			timer.View.ShowTime(timer.Time);
		}
	}
}