using Leopotam.Ecs;

public class HideTwitchConnectionUI  : IEcsRunSystem
{
	private readonly EcsFilter<HideTwitchConnectionUIComponent> filter = default;
	private readonly LevelData levelData = default;

	public void Run()
	{
		foreach(var i in filter)
		{
			levelData.TwitchConnectionUI.gameObject.SetActive(false);
			filter.GetEntity(i).Unset<HideTwitchConnectionUIComponent>();
		}
	}
}

public struct HideTwitchConnectionUIComponent
{
}
