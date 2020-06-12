using UnityEngine;

[CreateAssetMenu(menuName ="Game/Game Data")]
public class GameData : ScriptableObject
{
	public TwitchCommands TwitchCommands;
	public VisualPlayerData VisualData;
	public PlayersData PlayersData;
	public GenerationSettings GenerationSettings;
	public HatsList Hats;
	public float RoundDuration;
	public float AfterFirstLeaverRoundDuration;
	public string TimerFormat;
	public string SecretFileName;
	public bool IsDebug;
}
