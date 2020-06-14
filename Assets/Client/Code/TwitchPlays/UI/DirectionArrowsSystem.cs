using UnityEngine;
using Leopotam.Ecs;

public class DirectionArrowsSystem : IEcsInitSystem
{
	private readonly LevelData levelData = default;
	private readonly EcsWorld world = default;

	public void Init()
	{
		var width = (int)levelData.GameUI.DirectionArrowsImage.rectTransform.sizeDelta.x;
		var height = (int)levelData.GameUI.DirectionArrowsImage.rectTransform.sizeDelta.y;
		var texture = new RenderTexture(width, height, 32);
		levelData.GameUI.DirectionArrowsCamera.targetTexture = texture;
		levelData.GameUI.DirectionArrowsImage.texture = texture;

		for(int i = 0; i < levelData.GameUI.ArrowTexts.Length; ++i)
		{
			CreateArrow(levelData.GameUI.ArrowTexts[i], levelData.GameUI.DirectionArrowsCamera.transform);
		}
	}

	private void CreateArrow(Transform arrowObj, Transform lookTarget)
	{
		var arrowEnt = world.NewEntity();
		ref var lookToCamera = ref arrowEnt.Set<LookToObjectComponent>();
		lookToCamera.Transform = arrowObj;
		lookToCamera.Target = lookTarget;
	}
}
