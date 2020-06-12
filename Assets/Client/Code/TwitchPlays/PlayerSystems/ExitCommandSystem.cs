using Leopotam.Ecs;
using System;
using System.Collections.Generic;

public class ExitCommandSystem : IEcsRunSystem
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

			if (commands.IsCommand(mess.FirstWord, commands.Exit) == false) continue;

			if (runtimeData.ContainsPlayer(mess.Sender))
			{
				var playerEnt = runtimeData.GetPlayer(mess.Sender);
				playerEnt.Set<PlayerExitCommandComponent>();
			}

			if (runtimeData.IsEscapedPlayer(mess.Sender))
			{
				var e = world.NewEntity();
				ref var err = ref e.Set<ErrorComponent>();
				err.Message = $"@{mess.Sender} already leave this level";
			}

			filter.GetEntity(i).Destroy();
		}
	}
}
