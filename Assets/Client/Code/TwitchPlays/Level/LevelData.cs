﻿using UnityEngine;
using UnityEngine.EventSystems;

public class LevelData : MonoBehaviour
{
	public Camera Camera;
	public Transform CameraObject;
	public DebugInput DebugInput;
	public DebugOutput DebugOutput;
	public MonoPlayer PlayerTemplate;
	public MonoPlayerHUD PlayerHUDTemplate;
	public TwitchConnectionUI TwitchConnectionUI;
	public MonoGameUI GameUI;
	public CameraSettings CameraSettings;
	public EventSystem EventsSystem;
	public KeyCode DebugInputButton;
	public KeyCode SaveGameButton;
}