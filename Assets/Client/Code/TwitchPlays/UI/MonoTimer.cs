using UnityEngine;
using TMPro;
using System;

public class MonoTimer : MonoBehaviour
{
	[SerializeField] private TMP_Text timeOutput = default;
	[SerializeField] private string format = default;

	public void ShowTime(float seconds)
	{
		var time = TimeSpan.FromSeconds(seconds);
		timeOutput.text = time.ToString(format);
	}
}
