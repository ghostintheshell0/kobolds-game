using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MessageView : MonoBehaviour
{
	[SerializeField] private TMP_Text textField = default;
	[SerializeField] private Image background = default;

	public TMP_Text TextField => textField;
	public Image Background => background;

	public string Text
	{
		get => textField.text;
		set => textField.text = value;
	}
}