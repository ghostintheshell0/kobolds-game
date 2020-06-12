using Leopotam.Ecs;
using System.Collections.Generic;
public class LeaderBoardSystem : IEcsRunSystem
{
	private readonly EcsFilter<ShowLeaderBoardComponent> filter = default;
	private readonly RuntimeData runtimeData = default;
	private readonly LevelData levelData = default;


	public void Run()
	{
		if (filter.IsEmpty()) return;

		levelData.LeaderBoard.gameObject.SetActive(true);
		levelData.LeaderBoard.Clear();

		var wallsDestroyingTop = GetOrderedRaws(p => p.WallsDestroyedInCurrentGame);

		for (int i = 0; i < wallsDestroyingTop.Count; ++i)
		{
			levelData.LeaderBoard.AddRow(wallsDestroyingTop[i]);
		}
		
		foreach (var i in filter)
		{
			filter.GetEntity(i).Destroy();
		}
	}

	public List<LeaderBoardRowData> GetOrderedRaws(System.Func<PlayerStats, int> selector)
	{
		var list = new List<LeaderBoardRowData>(runtimeData.PlayersInLastGame.Count);

		for(int i = 0; i < runtimeData.PlayersInLastGame.Count; ++i)
		{
			var data = new LeaderBoardRowData()
			{
				Name = runtimeData.PlayersInLastGame[i].Name,
				Value = selector(runtimeData.PlayersInLastGame[i]),
			};

			list.Add(data);
		};

		list.Sort(LeaderBoardRowDataDecreaseComparer.Instance);

		for (int i = 0; i < list.Count; ++i)
		{
			list[i].Number = i + 1;
		}

		return list;
	}
}

public struct ShowLeaderBoardComponent
{

}

public class LeaderBoardRowDataDecreaseComparer : IComparer<LeaderBoardRowData>
{
	public static LeaderBoardRowDataDecreaseComparer Instance = new LeaderBoardRowDataDecreaseComparer();

	public int Compare(LeaderBoardRowData x, LeaderBoardRowData y)
	{
		if (x.Value < y.Value) return 1;
		if (x.Value > y.Value) return -1;
		return 0;
	}
}