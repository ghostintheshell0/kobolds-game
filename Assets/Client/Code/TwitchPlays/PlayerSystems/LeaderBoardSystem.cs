using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardSystem : IEcsRunSystem
{
	private readonly EcsFilter<ShowLeaderBoardComponent> filter = default;
	private readonly RuntimeData runtimeData = default;
	private readonly LevelData levelData = default;


	public void Run()
	{
		if (filter.IsEmpty()) return;

		levelData.GameUI.TotalOreTop.Clear();

		var totalOreTop = GetOrderedRaws(runtimeData.SavedPlayers, p => p.TotalOre);

		var leaderBoardRows = Mathf.Min(totalOreTop.Count, levelData.GameUI.TotalOreTop.Size);

		for (int i = 0; i < leaderBoardRows; ++i)
		{
			levelData.GameUI.TotalOreTop.AddRow(totalOreTop[i]);
		}
		
		foreach (var i in filter)
		{
			filter.GetEntity(i).Destroy();
		}
	}

	public List<LeaderBoardRowData> GetOrderedRaws(IReadOnlyList<PlayerStats> statsList, System.Func<PlayerStats, int> selector)
	{
		var list = new List<LeaderBoardRowData>(statsList.Count);

		for(int i = 0; i < statsList.Count; ++i)
		{
			var data = new LeaderBoardRowData()
			{
				Name = statsList[i].Name,
				Value = selector(statsList[i]),
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