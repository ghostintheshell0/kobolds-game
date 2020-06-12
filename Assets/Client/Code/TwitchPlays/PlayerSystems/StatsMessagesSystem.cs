using Leopotam.Ecs;

public class StatsMessagesSystem : IEcsRunSystem
{
	private readonly EcsFilter<MessageComponent> filter = default;
	private readonly TwitchCommands commands = default;
	private readonly GameData gameData = default;
	private readonly RuntimeData runtimeData = default;
	private readonly EcsWorld world = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var mess = ref filter.Get1(i);

			if (commands.IsCommand(mess.FirstWord, commands.Stats) == false) continue;
			
			var ent = world.NewEntity();
			ref var err = ref ent.Set<ErrorComponent>();

			PlayerStats stats = null;

			if (runtimeData.ContainsPlayer(mess.Sender))
			{
				var playerEnt = runtimeData.GetPlayer(mess.Sender);
				ref var player = ref playerEnt.Set<PlayerComponent>();
				stats = player.Stats;
			}
			else if (runtimeData.IsEscapedPlayer(mess.Sender))
			{
				stats = runtimeData.GetSavedPlayer(mess.Sender);
			}

			if(stats != null)
			{
				err.Message = $"@{mess.Sender} Level: {stats.Level}; Ore {stats.Ore}/{stats.Level * gameData.PlayersData.LevelCostMultipler}; Escaped {stats.Escapes} times";
			}
			else
			{
				err.Message = $"@{mess.Sender} , your character not spawned. Type {commands.Enter[0]}";
			}

			filter.GetEntity(i).Destroy();
			
		}
	}
}
