using Leopotam.Ecs;

public class LookToCameraSystem :  IEcsRunSystem
{
	private readonly EcsFilter<LookToObjectComponent> filter = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var obj = ref filter.Get1(i);
			if (obj.Transform == null)
			{
				filter.GetEntity(i).Destroy();
				continue;
			}
			obj.Transform.LookAt(obj.Transform.position + obj.Target.forward);
		}
	}
}
