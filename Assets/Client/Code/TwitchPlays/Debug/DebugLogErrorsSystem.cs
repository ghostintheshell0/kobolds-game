using Leopotam.Ecs;

public class DebugLogErrorsSystem : IEcsRunSystem
{
	private readonly EcsFilter<ErrorComponent> filter = default;
	private readonly LevelData levelData = default;

	public void Run()
	{
		foreach(var i in filter)
		{
			ref var err = ref filter.Get1(i);
			levelData.DebugOutput.Log(err.Message);
			UnityEngine.Debug.Log(err.Message);
		}
	}
}