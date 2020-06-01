using Leopotam.Ecs;
using UnityEngine;

public class HeadSizeChangingSystem : IEcsRunSystem
{
	private readonly EcsFilter<SkinComponent, HeadSizeChangerComponent> filter = default;
	private readonly GameData gameData = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var skin = ref filter.Get1(i);
			ref var changer = ref filter.Get2(i);
			float size = Mathf.Clamp(changer.Size, gameData.VisualData.MinHeadSize, gameData.VisualData.MaxHeadSize);
			skin.View.Head.localScale = new Vector3(size, size, size);

			filter.GetEntity(i).Unset<HeadSizeChangerComponent>();
		}
	}
}
