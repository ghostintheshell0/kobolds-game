using Leopotam.Ecs;
using UnityEngine;

public class ObjectMovingSystem : IEcsRunSystem
{

	private readonly EcsFilter<MovebleComponent> objects = default;
	private readonly EcsFilter<MoveComponent> filter = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var move = ref filter.Get1(i);

			foreach (var o in objects)
			{
				ref var target = ref objects.Get1(o);

				target.Transform.Translate(move.Delta * (Time.deltaTime * target.Speed));
			}
			filter.GetEntity(i).Destroy();
		}
	}
}
