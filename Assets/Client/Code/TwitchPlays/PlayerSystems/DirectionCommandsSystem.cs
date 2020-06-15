using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class DirectionCommandsSystem : IEcsRunSystem
{

	private readonly EcsFilter<MessageComponent> filter = default;
	private readonly RuntimeData runtimeData = default;
	private readonly GameData gameData = default;
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

					playerEnt.Set<TasksCompletedComponent>();
					ref var tasks = ref playerEnt.Set<PlayerTasksComponent>();
					if(tasks.List == null)
					{
						tasks.List = new List<Vector2Int>();
					}
					tasks.List.Clear();
					tasks.List.Add(dir);
					tasks.CurrentTask = 0;

					if(mess.Args.Length > 0)
					{
						var words = mess.Args.Split(' ');
						ParseDirections(words, tasks.List);
					}
				}

				if(runtimeData.IsEscapedPlayer(mess.Sender))
				{
					var e = world.NewEntity();
					ref var err = ref e.Set<ErrorComponent>();
					err.Message = $"@{mess.Sender} {gameData.Localizations.AlreadyEscaped}";
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

	private void ParseDirections(string[] args, List<Vector2Int> directions)
	{
		for (int k = 0; k < args.Length; ++k)
		{
			if(!IsDirectionCommand(args[k])) break;

			var taskDir = CommandToDirection(args[k]);
			directions.Add(taskDir);
		}
	}

}
