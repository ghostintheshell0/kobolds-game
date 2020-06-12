using UnityEngine;
using TMPro;

public class LeaderBoardRow : MonoBehaviour
{
	[SerializeField] private TMP_Text number = default;
	[SerializeField] private TMP_Text leaderName = default;
	[SerializeField] private TMP_Text value = default;

	public void Show(LeaderBoardRowData data)
	{
		number.text = data.Number.ToString();
		leaderName.text = data.Name;
		value.text = data.Value.ToString();
	}
}
