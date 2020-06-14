using Leopotam.Ecs;
using UnityEngine;

public class PlayerEnterSystem : IEcsRunSystem
{
	private readonly EcsFilter<MessageComponent> filter = default;
	private readonly TwitchCommands commands = default;
	private readonly EcsWorld world = default;
	private readonly RuntimeData runtimeData = default;
	private readonly GameData gameData = default;

	public void Run()
	{
		foreach(var i in filter)
		{
			ref var mess = ref filter.Get1(i);
			if (commands.IsCommand(mess.FirstWord, commands.Enter) == false) continue;
			
			if (runtimeData.IsEscapedPlayer(mess.Sender))
			{
				SendMessage($"@{mess.Sender} already escaped from this level");
				continue;
			}

			if (runtimeData.ContainsPlayer(mess.Sender))
			{
				SendMessage($"@{mess.Sender} already spawned");
				continue;
			}
			
			var ent = world.NewEntity();
			ref var spawn = ref ent.Set<PlayerSpawnComponent>();

			var stats = runtimeData.IsSavedPlayer(mess.Sender) ? runtimeData.GetSavedPlayer(mess.Sender) : GetNewPlayerStats(mess.Sender);
			stats.Level = 1;
			stats.CurrentOre = 0;
			stats.WallsDestroyedInCurrentGame = 0;

			spawn.Stats = stats;


		}
	}

	private void SendMessage(string text)
	{
		var ent = world.NewEntity();
		ref var mess = ref ent.Set<ErrorComponent>();
		mess.Message = text;
	}

	private PlayerStats GetNewPlayerStats(string playerName)
	{
		return new PlayerStats()
			{
				Name = playerName,
				Level = gameData.PlayersData.StartPlayerValues.Level,
				CurrentOre = gameData.PlayersData.StartPlayerValues.CurrentOre,
				Escapes = gameData.PlayersData.StartPlayerValues.Escapes,
				HeadSize = gameData.PlayersData.StartPlayerValues.HeadSize,
	//			SkinColor = gameData.PlayersData.StartPlayerValues.SkinColor,
				Deads = 0,
				WallsDestroyed = 0,
				WallsDestroyedInCurrentGame = 0
			};
	}
}