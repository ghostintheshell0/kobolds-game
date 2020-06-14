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

			if (runtimeData.IsSavedPlayer(mess.Sender))
			{
				stats = runtimeData.GetSavedPlayer(mess.Sender);
				err.Message = $"@{mess.Sender} Level: {stats.Level}; Ore {stats.CurrentOre}/{stats.Level * gameData.PlayersData.LevelCostMultipler}; Total mined {stats.TotalOre} ore; {stats.Hats.Count} hats; Escaped {stats.Escapes} times";
			}
			else
			{
				err.Message = $"@{mess.Sender} , your character not spawned. Type {commands.Enter[0]}";
			}

			filter.GetEntity(i).Destroy();
			
		}
	}
}
