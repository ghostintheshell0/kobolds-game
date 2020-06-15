using Leopotam.Ecs;

public class HeadSizeCommandSystem : IEcsRunSystem
{
	private readonly EcsFilter<MessageComponent> filter = default;
	private readonly TwitchCommands commands = default;
	private readonly GameData gameData = default;
	private readonly EcsWorld world = default;
	private readonly RuntimeData runtimeData = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var mess = ref filter.Get1(i);

			if (commands.IsCommand(mess.FirstWord, commands.HeadSize) == false) continue;
			
			if (runtimeData.ContainsPlayer(mess.Sender))
			{
				if (float.TryParse(mess.Args, out var size))
				{
					var playerEnt = runtimeData.GetPlayer(mess.Sender);
					ref var player = ref playerEnt.Set<PlayerComponent>();
					player.Stats.HeadSize = size;
					ref var headSize = ref playerEnt.Set<HeadSizeChangerComponent>();
					headSize.Size = size;
				}
				else
				{
					SendError($"{gameData.Localizations.TwitchUserPrefix}{mess.Sender} {gameData.Localizations.SyntaxError} {gameData.Localizations.Example} {commands.HeadSize[0]} 1,5");
				}
			}
			else if (runtimeData.IsEscapedPlayer(mess.Sender))
			{
				SendError($"{gameData.Localizations.TwitchUserPrefix}{mess.Sender} , {gameData.Localizations.AlreadyEscaped}");
			}
			else
			{
				SendError($"{gameData.Localizations.TwitchUserPrefix}{mess.Sender} , {gameData.Localizations.NotSpawned} {gameData.Localizations.SpawnHelp}");
			}

			filter.GetEntity(i).Destroy();
			
		}
	}

	private void SendError(string message)
	{
		var ent = world.NewEntity();
		ref var err = ref ent.Set<ErrorComponent>();
		err.Message = message;
	}
}
