﻿using UnityEngine;

[CreateAssetMenu(menuName ="Game/Game Data")]
public class GameData : ScriptableObject
{
	public TwitchCommands TwitchCommands;
	public VisualPlayerData VisualData;
	public PlayersData PlayersData;
	public GenerationSettings GenerationSettings;
	public HatsList Hats;
	public LocalizationStrings Localizations;
	public float RoundDuration;
	public float AfterFirstLeaverRoundDuration;
	public string LocalDataPath;
	public string SecretFileName;
	public string PlayersSaveFile;
	public bool IsDebug;
}
