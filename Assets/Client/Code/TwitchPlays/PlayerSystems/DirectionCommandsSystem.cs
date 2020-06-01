using Leopotam.Ecs;
using UnityEngine;

public class DirectionCommandsSystem : IEcsRunSystem
{
	private readonly EcsFilter<MessageComponent> filter = default;
	private readonly RuntimeData runtimeData = default;
	private readonly TwitchCommands commands = default;
	private readonly EcsWorld world = default;

	public void Run()
	{
		foreach(var i in filter)
		{
			ref var mess = ref filter.Get1(i);

			if (IsDirectionCommand(mess.FirstWord))
			{
				if(runtimeData.ContainsPlayer(mess.Sender))
				{
					var dir = CommandToDirection(mess.FirstWord);

					var playerEnt = runtimeData.GetPlayer(mess.Sender);
					ref var target = ref playerEnt.Set<TargetComponent>();
					target.Direction = dir;
				}

				if(runtimeData.IsSavedPlayer(mess.Sender))
				{
					var e = world.NewEntity();
					ref var err = ref e.Set<ErrorComponent>();
					err.Message = $"@{mess.Sender} already leave this level";
				}

			}
		}
	}


	private bool IsDirectionCommand(string command)
	{
		return commands.IsCommand(command, commands.Up) || commands.IsCommand(command, commands.Right) ||
			commands.IsCommand(command, commands.Down) || commands.IsCommand(command, commands.Left);
	}

	private Vector2Int CommandToDirection(string command)
	{
		if (commands.IsCommand(command, commands.Up))
		{
			return Vector2Int.up;
		}

		if (commands.IsCommand(command, commands.Right))
		{
			return Vector2Int.right;
		}

		if (commands.IsCommand(command, commands.Down))
		{
			return Vector2Int.down;
		}

		return Vector2Int.left;
	}
}
