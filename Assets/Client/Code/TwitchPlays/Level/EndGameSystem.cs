using Leopotam.Ecs;
using System.Text;

public class EndGameSystem : IEcsRunSystem
{
	private readonly EcsFilter<EndGameComponent> filter = default;
	private readonly RuntimeData runtimeData = default;
	private readonly EcsWorld world = default;

	private string namesSeparator = " , ";
	private string namePrefix = "@";

	private StringBuilder stringBuilder = new StringBuilder();

	public void Run()
	{
		if (filter.IsEmpty()) return;

		var clearEnt = world.NewEntity();
		clearEnt.Set<ClearLevelComponent>();

		var messEnt = world.NewEntity();
		ref var message = ref messEnt.Set<ErrorComponent>();

		var leaderBoardEnt = world.NewEntity();
		ref var showLeaderBoard = ref leaderBoardEnt.Set<ShowLeaderBoardComponent>();

		var saveGameEnt = world.NewEntity();
		saveGameEnt.Set<SavePlayersComponent>();

		stringBuilder.Clear();

		if (runtimeData.EscapedPlayers.Count == 0)
		{
			stringBuilder.Append($"Level completed.");
		}
		else
		{
			stringBuilder.Append($"Level completed. Saved players: ");

			for (int i = 0; i < runtimeData.EscapedPlayers.Count; ++i)
			{
				stringBuilder.Append(namePrefix);
				stringBuilder.Append(runtimeData.EscapedPlayers[i].Name);
				stringBuilder.Append(namesSeparator);
			}
			stringBuilder.Remove(stringBuilder.Length - namesSeparator.Length, namesSeparator.Length);
			stringBuilder.Append(" .");
		}

		message.Message = stringBuilder.ToString();

		foreach (var i in filter)
		{
			filter.GetEntity(i).Destroy();
		}

	}
}