using Leopotam.Ecs;

public class HatCommandSystem : IEcsRunSystem
{
	private readonly EcsFilter<MessageComponent> filter = default;
	private readonly RuntimeData runtimeData = default;
	private readonly TwitchCommands commands = default;
	private readonly GameData gameData = default;
	private readonly EcsWorld world = default;


	public void Run()
	{
		foreach (var i in filter)
		{
			ref var mess = ref filter.Get1(i);

			if (commands.IsCommand(mess.FirstWord, commands.Hats) == false) continue;
			
			if(!runtimeData.IsSavedPlayer(mess.Sender))
			{
				SendMessage($"@{mess.Sender} , your character not spawned. Type {commands.Enter[0]}");
			}
			else if(string.IsNullOrEmpty(mess.Args))
			{
				if (runtimeData.IsSavedPlayer(mess.Sender))
				{
					var playerStats = runtimeData.GetSavedPlayer(mess.Sender);
					var hats = PrintHatsList(playerStats);
					var text = playerStats.Hats.Count == 0 ? "no have hats." : $"your hats: {hats}";
					SendMessage($"@{playerStats.Name} {text}");
				}
			}
			else if(int.TryParse(mess.Args, out var hatIndex))
			{
				--hatIndex;

				if(runtimeData.ContainsPlayer(mess.Sender))
				{
					var playerEnt = runtimeData.GetPlayer(mess.Sender);

					ref var hatChanger = ref playerEnt.Set<ChangeHatComponent>();
					hatChanger.HatIndex = hatIndex;
				}
				else
				{
					var playerStats = runtimeData.GetSavedPlayer(mess.Sender);
					playerStats.CurrentHatIndex = hatIndex;
				}
			}
			else
			{
				SendMessage($"@{mess.Sender} syntax error. Example: {mess.FirstWord} 1");
			}

			filter.GetEntity(i).Destroy();

		}
	}

	private string PrintHatsList(PlayerStats playerStats)
	{
		string result = "";

		for(int i = 0; i < playerStats.Hats.Count; ++i)
		{
			result += $"{i+1}. {gameData.Hats.Hats[playerStats.Hats[i]].name}; ";
		}

		return result;
	}

	private void SendMessage(string message)
	{
		var ent = world.NewEntity();
		ref var err = ref ent.Set<ErrorComponent>();
		err.Message = message;
	}
}