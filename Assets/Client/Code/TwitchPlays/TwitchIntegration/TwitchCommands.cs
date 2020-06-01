using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Game/Twitch commands")]
public class TwitchCommands : ScriptableObject
{
	public List<string> Enter = default;
	public List<string> Left = default;
	public List<string> Right = default;
	public List<string> Up = default;
	public List<string> Down = default;
	public List<string> HeadSize = default;
	public List<string> Color = default;
	public List<string> Exit = default;
	public List<string> Help = default;
	public List<string> Stats = default;
	public List<string> Upgrade = default;

	public bool IsCommand(string text, List<string> variants)
	{
		for (int i = 0; i < variants.Count; i++)
		{
			if (variants[i].Equals(text, StringComparison.CurrentCultureIgnoreCase))
			{
				return true;
			}
		}

		return false;
	}
}