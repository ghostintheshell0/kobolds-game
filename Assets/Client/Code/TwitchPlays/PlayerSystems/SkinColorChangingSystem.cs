using Leopotam.Ecs;

public class SkinColorChangingSystem : IEcsRunSystem
{
	private readonly EcsFilter<SkinComponent, SkinColorChangerComponent> filter = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var skin = ref filter.Get1(i);
			ref var changer = ref filter.Get2(i);
			skin.View.MeshRenderer.material.color = changer.Color;

			filter.GetEntity(i).Unset<SkinColorChangerComponent>();
		}
	}
}