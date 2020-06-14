using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonoGameUI : MonoBehaviour
{
	[SerializeField] private MonoTimer levelTimer = default;
	[Space]
	[SerializeField] private RawImage arrowsImage = default;
	[SerializeField] private Camera arrowsCamera = default;
	[SerializeField] private GameObject directionArrowsCameraObject = default;
	[Space]
	[SerializeField] private LeaderBoard minersTop = default;
	[SerializeField] private LeaderBoard levelsTop = default;

	[SerializeField] private Transform[] arrowTexts = default;

	public Camera DirectionArrowsCamera => arrowsCamera;
	public RawImage DirectionArrowsImage => arrowsImage;
	public Transform[] ArrowTexts => arrowTexts;
	public GameObject DirectionArrowsCameraObject => directionArrowsCameraObject;
	public MonoTimer LevelTimer => levelTimer;
}
