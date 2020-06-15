using Leopotam.Ecs;

public class HelpMessagesSystem : IEcsRunSystem
{
	private readonly EcsFilter<MessageComponent> filter = default;
	private readonly TwitchCommands commands = default;
	private readonly GameData gameData = default;
	private readonly EcsWorld world = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var mess = ref filter.Get1(i);

			if (commands.IsCommand(mess.FirstWord, commands.Help) == false) continue;
			
			var ent = world.NewEntity();
			ref var err = ref ent.Set<ErrorComponent>();
			err.Message = $"{gameData.Localizations.TwitchUserPrefix}{mess.Sender} {gameData.Localizations.HelpMessage}";
			filter.GetEntity(i).Destroy();
			
		}
	}
}