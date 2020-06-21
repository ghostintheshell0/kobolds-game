using Leopotam.Ecs;
using UnityEngine;

public class CameraZoomSystem : IEcsRunSystem
{
	private readonly EcsFilter<ZoomableCameraComponent> objects = default;
	private readonly EcsFilter<ZoomComponent> filter = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var zoom = ref filter.Get1(i);

			foreach (var o in objects)
			{
				ref var target = ref objects.Get1(o);

				var delta = zoom.Delta * Time.deltaTime * target.Speed;
				target.Camera.orthographicSize = Mathf.Clamp(target.Camera.orthographicSize + delta, target.MinZoom, target.MaxZoom);
			}

			filter.GetEntity(i).Destroy();
		}
	}
}
