using Leopotam.Ecs;

public class UpgradeSystem : IEcsRunSystem
{
	private EcsFilter<MessageComponent> filter = default;
	private readonly TwitchCommands commands = default;
	private readonly EcsWorld world = default;
	private readonly RuntimeData runtimeData = default;
	private readonly GameData gameData = default;

	public void Run()
	{
		foreach(var i in filter)
		{
			ref var mess = ref filter.Get1(i);

			if (commands.IsCommand(mess.FirstWord, commands.Upgrade) == false) continue;

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
				var playerEnt = runtimeData.GetSavedPlayer(mess.Sender);
				var player = runtimeData.GetSavedPlayer(mess.Sender);
				stats = player;
			}

			if(stats != null)
			{ 
				var cost = stats.Level * gameData.PlayersData.LevelCostMultipler;
				if (stats.CurrentOre >= cost)
				{
					stats.Level++;
					stats.CurrentOre -= cost;
					var nextUpgradeCost = stats.Level * gameData.PlayersData.LevelCostMultipler;
					err.Message = $"{gameData.Localizations.TwitchUserPrefix}{stats.Name} {stats.Level} {gameData.Localizations.LevelUp}. {gameData.Localizations.NextLevelCost} {stats.CurrentOre}/{nextUpgradeCost} {gameData.Localizations.Ores}.";
				}
				else
				{
					err.Message = $"{gameData.Localizations.TwitchUserPrefix}{stats.Name} {gameData.Localizations.NextLevelCost} {stats.CurrentOre}/{cost} {gameData.Localizations.Ores}.";
				}
			}
			else
			{
				err.Message = $"{gameData.Localizations.TwitchUserPrefix}{mess.Sender} , {gameData.Localizations.NotSpawned} {gameData.Localizations.SpawnHelp}";
			}

			filter.GetEntity(i).Destroy();
		}
	}
}