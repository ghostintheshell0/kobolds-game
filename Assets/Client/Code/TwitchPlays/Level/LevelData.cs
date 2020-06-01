using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class LevelData : MonoBehaviour
{
	public Camera Camera;
	public Transform CameraObject;
	public Transform StartPoint;
	public NavMeshSurface NavMeshSurface;
	public DebugInput DebugInput;
	public DebugOutput DebugOutput;
	public MonoPlayer PlayerTemplate;
	public TMP_Text LevelTimer;
	public MonoPlayerHUD PlayerHUDTemplate;
	public Vector3 PlayerHUDOffset;
	public TwitchConnectionUI TwitchConnectionUI;
	public CameraSettings CameraSettings;
	public KeyCode DebugInputButton;
}
