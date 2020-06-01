using Leopotam.Ecs;

public class PlayerHudLookToCameraSystem : IEcsRunSystem
{
	private readonly EcsFilter<PlayerHudComponent> filter = default;
	private readonly LevelData levelData = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var hud = ref filter.Get1(i);
			if(hud.View == null || hud.View.gameObject.activeSelf == false)
			{
				filter.GetEntity(i).Destroy();
				continue;
			}
			hud.View.transform.LookAt(hud.View.transform.position + levelData.Camera.transform.forward);
		}
	}
}