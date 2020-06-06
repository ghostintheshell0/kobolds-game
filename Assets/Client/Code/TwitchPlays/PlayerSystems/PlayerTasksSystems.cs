using UnityEngine;
using System.Collections;
using Leopotam.Ecs;
using System.Collections.Generic;

public class TaskCompleteSystems : IEcsRunSystem
{
	private readonly EcsFilter<TasksCompletedComponent, PlayerTasksComponent> filter = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var tasks = ref filter.Get2(i);
			ref var playerEnt = ref filter.GetEntity(i);

			if(tasks.CurrentTask < tasks.List.Count)
			{
				ref var target = ref playerEnt.Set<TargetComponent>();
				target.Direction = tasks.List[tasks.CurrentTask];
			}

			tasks.CurrentTask++;
			playerEnt.Unset<TasksCompletedComponent>();

		}
	}

}

public class TasksBreakSystem : IEcsRunSystem
{

	private readonly EcsFilter<TasksBreakComponent, PlayerTasksComponent> filter = default;
	public void Run()
	{
		foreach (var i in filter)
		{
			ref var tasks = ref filter.Get2(i);
			ref var ent = ref filter.GetEntity(i);
			tasks.List?.Clear();
			ent.Unset<TasksBreakComponent>();
		}
	}
}

public struct PlayerTasksComponent
{
	public int CurrentTask;
	public List<Vector2Int> List;
}

public struct TasksBreakComponent
{
}
public struct TasksCompletedComponent
{
}