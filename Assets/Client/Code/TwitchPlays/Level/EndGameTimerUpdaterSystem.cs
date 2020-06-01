using Leopotam.Ecs;

public class EndGameTimerUpdaterSystem : IEcsRunSystem
{
	private readonly EcsFilter<TimerUpdaterComponent> filter = default;
	private readonly EcsFilter<LevelTimerComponent> timers = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			float newTime = filter.Get1(i).NewTime;
			foreach (var k in timers)
			{
				ref var timer = ref timers.Get1(k);
				timer.Time = newTime;
			}

			filter.GetEntity(i).Destroy();
		}
	}
}