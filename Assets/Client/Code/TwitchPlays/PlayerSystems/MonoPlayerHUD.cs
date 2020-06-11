using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonoPlayerHUD : MonoBehaviour
{
	[SerializeField] private TMP_Text playerName = default;
	[SerializeField] private Slider progressBar = default;
	[SerializeField] private Image icon = default;
	[SerializeField] private Canvas canvas = default;
	public Vector3 Offset = default;

	public TMP_Text PlayerNameField => playerName;
	public Slider Progress => progressBar;
	public Image Icon => icon;
	public Canvas Canvas => canvas;
}