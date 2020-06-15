using Leopotam.Ecs;
using TwitchPlays.Utils;

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

			if (runtimeData.IsSavedPlayer(mess.Sender))
			{
				var stats = runtimeData.GetSavedPlayer(mess.Sender);
				err.Message = PrintPlayerStats(stats);
			}
			else
			{
				err.Message = $"{gameData.Localizations.TwitchUserPrefix}{mess.Sender} , {gameData.Localizations.NotSpawned} {gameData.Localizations.SpawnHelp}";
			}

			filter.GetEntity(i).Destroy();
			
		}
	}

	private string PrintPlayerStats(PlayerStats stats)
	{
		var sb = StringBuilder.Get();
		sb.Append(gameData.Localizations.TwitchUserPrefix);
		sb.Append(stats.Name);
		sb.Append(' ');

		sb.Append(gameData.Localizations.Level);
		sb.Append(": ");
		sb.Append(stats.Level);
		sb.Append("; ");

		sb.Append(stats.CurrentOre);
		sb.Append("/");
		sb.Append(stats.Level* gameData.PlayersData.LevelCostMultipler);
		sb.Append(' ');
		sb.Append(gameData.Localizations.Ores);
		sb.Append("; ");


		sb.Append(gameData.Localizations.Mined);
		sb.Append(' ');
		sb.Append(stats.TotalOre);
		sb.Append(' ');
		sb.Append(gameData.Localizations.Ores);
		sb.Append("; ");

		sb.Append(stats.Hats.Count);
		sb.Append(' ');
		sb.Append(gameData.Localizations.Hats);
		sb.Append("; ");

		sb.Append(stats.Escapes);
		sb.Append(' ');
		sb.Append(gameData.Localizations.Escapes);
		sb.Append("; ");


		return sb.ToString();
	}
}
