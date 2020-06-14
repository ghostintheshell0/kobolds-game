using Leopotam.Ecs;

public class HideTwitchConnectionUI  : IEcsRunSystem, IEcsInitSystem
{
	private readonly EcsFilter<ChangeTwitchConnectionUIComponent> filter = default;
	private readonly LevelData levelData = default;
	private readonly GameData gameData = default;

	public void Init()
	{
		levelData.TwitchConnectionUI.gameObject.SetActive(!gameData.IsDebug);
	}

	public void Run()
	{
		foreach(var i in filter)
		{
			ref var changer = ref filter.Get1(i);
			levelData.TwitchConnectionUI.gameObject.SetActive(changer.Visible);
			filter.GetEntity(i).Unset<ChangeTwitchConnectionUIComponent>();
		}
	}
}
