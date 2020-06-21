using Leopotam.Ecs;
using UnityEngine;

public class ObjectsRotationSystem : IEcsRunSystem
{
	private readonly EcsFilter<RotableComponent> objects = default;
	private readonly EcsFilter<RotationComponent> filter = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var rotation = ref filter.Get1(i);

			foreach(var o in objects)
			{
				ref var target = ref objects.Get1(o);

				var oldAngles = target.Transform.rotation.eulerAngles;
				var deltaX = rotation.Delta.y * target.Speed * Time.deltaTime;
				var deltaY = rotation.Delta.x * target.Speed * Time.deltaTime;
				var newX = Mathf.Clamp(oldAngles.x + deltaX, target.MinAngle.x, target.MaxAngle.x);

				target.Transform.rotation = Quaternion.Euler(newX, oldAngles.y + deltaY, oldAngles.z);

			}
			
			filter.GetEntity(i).Destroy();
		}
	}
}
