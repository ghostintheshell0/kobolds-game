using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonoGameUI : MonoBehaviour
{
	[SerializeField] private MonoTimer levelTimer = default;
	[SerializeField] private RawImage arrowsImage = default;
	[SerializeField] private Camera arrowsCamera = default;
	[SerializeField] private GameObject directionArrowsCameraObject = default;

	[SerializeField] private Transform[] arrowTexts = default;

	public Camera DirectionArrowsCamera => arrowsCamera;
	public RawImage DirectionArrowsImage => arrowsImage;
	public Transform[] ArrowTexts => arrowTexts;
	public GameObject DirectionArrowsCameraObject => directionArrowsCameraObject;
}
