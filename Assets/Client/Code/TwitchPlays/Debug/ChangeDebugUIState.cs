using Leopotam.Ecs;

public class ChangeDebugUIState : IEcsRunSystem
{
	private readonly LevelData levelData = default;

	public void Run()
	{
		if(UnityEngine.Input.GetKeyDown(levelData.DebugInputButton))
		{
			levelData.DebugInput.gameObject.SetActive(!levelData.DebugInput.gameObject.activeSelf);
		}
	}
}