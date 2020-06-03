using Leopotam.Ecs;

public class PlayerHudRemovingSystem :IEcsRunSystem
{
	private readonly EcsFilter<PlayerHudComponent> filter = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var hud = ref filter.Get1(i);

			if (hud.Target.IsAlive()) continue;

			hud.View.Canvas.transform.SetParent(null);
			ObjectPool.Recycle(hud.View);
			filter.GetEntity(i).Destroy();
		}
	}
}