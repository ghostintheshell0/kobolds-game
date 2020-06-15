using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Game/Localization/Strings")]
public class LocalizationStrings : ScriptableObject
{
	public string TwitchUserPrefix = "@";
	public string LevelCompleted = "Level completed.";
	public string AlreadyEscaped = "already escaped from this level.";
	public string AlreadySpawned = "already spawned.";
	public string SavedPlayers = "Saved players:";
	public string GameOverWarning = "The world will be destroyed in";
	public string Seconds = "seconds";
	public string NotSpawned = "your character not spawned.";
	public string SyntaxError = "syntax error.";
	public string SpawnHelp = "Type !enter";
	public string Example = "Example:";
	[TextArea]
	public string HelpMessage = "type !e for spawn. Type !u, !r, !d or !l for moving or mining. When any player found exit come to ladder and type !exit for leave from the map. When time left all players will die.";
	public string Level = "Level";
	public string Ores = "ores";
	public string Mined = "mined";
	public string Hats = "hats";
	public string Escapes = "escapes";
	public string NextLevelCost = "For next level need";
	public string LevelUp = "level";
}

