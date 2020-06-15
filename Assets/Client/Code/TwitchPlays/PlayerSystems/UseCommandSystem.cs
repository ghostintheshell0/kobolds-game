using Leopotam.Ecs;

public class UseCommandSystem : IEcsRunSystem
{
	private readonly EcsFilter<MessageComponent> filter = default;
	private readonly TwitchCommands commands = default;
	private readonly EcsWorld world = default;
	private readonly RuntimeData runtimeData = default;
	private readonly GameData gameData = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var mess = ref filter.Get1(i);

			if (commands.IsCommand(mess.FirstWord, commands.Use) == false) continue;

			if (runtimeData.ContainsPlayer(mess.Sender))
			{
				var playerEnt = runtimeData.GetPlayer(mess.Sender);
		//		ref var player = ref playerEnt.Set<PlayerComponent>();
				ref var skinChanger = ref playerEnt.Set<UseCommand>();
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
