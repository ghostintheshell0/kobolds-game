using Leopotam.Ecs;

public class ChangeDebugUIState : IEcsRunSystem, IEcsInitSystem
{
	private readonly LevelData levelData = default;
	private readonly GameData gameData = default;

	public void Init()
	{
		levelData.DebugInput.gameObject.SetActive(gameData.IsDebug);
	}

	public void Run()
	{
		if(UnityEngine.Input.GetKeyDown(levelData.DebugInputButton))
		{
			levelData.DebugInput.gameObject.SetActive(!levelData.DebugInput.gameObject.activeSelf);
		}
	}
}
