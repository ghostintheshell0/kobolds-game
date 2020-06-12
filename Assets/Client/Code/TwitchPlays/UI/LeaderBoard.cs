using UnityEngine;
using System.Collections.Generic;

public class LeaderBoard : MonoBehaviour
{
	[SerializeField] private Transform container = default;
	[SerializeField] private LeaderBoardRow rowTemplate = default;
	private List<LeaderBoardRow> rows = new List<LeaderBoardRow>();

	public void Clear()
	{
		for(var i = 0; i < rows.Count; ++i)
		{
			ObjectPool.Recycle(rows[i]);
		}

		rows.Clear();
	}

	public void AddRow(LeaderBoardRowData data)
	{
		var row = ObjectPool.Spawn(rowTemplate);
		row.transform.SetParent(container);
		row.Show(data);
	}
}