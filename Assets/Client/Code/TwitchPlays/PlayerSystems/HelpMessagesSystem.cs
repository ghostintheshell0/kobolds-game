using Leopotam.Ecs;

public class HelpMessagesSystem : IEcsRunSystem
{
	private readonly EcsFilter<MessageComponent> filter = default;
	private readonly TwitchCommands commands = default;
	private readonly EcsWorld world = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var mess = ref filter.Get1(i);

			if (commands.IsCommand(mess.FirstWord, commands.Help) == false) continue;
			
			var ent = world.NewEntity();
			ref var err = ref ent.Set<ErrorComponent>();
			err.Message = $"@{mess.Sender} type !e for spawn. Type !u, !r, !d or !l for moving or mining. " +
				$"When any player found exit come to ladder and type !exit for leave from the map. When time left all players will die. " +
			$" Type !color and !size for customize your kobold.";
			filter.GetEntity(i).Destroy();
			
		}
	}
}
