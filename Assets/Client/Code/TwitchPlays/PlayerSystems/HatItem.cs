using UnityEngine;

[CreateAssetMenu(menuName = "Game/Hat")]
public class HatItem : RandomItem
{
	public Transform View;
	public Vector3 OffsetPosition;
	public Vector3 OffsetRotation;
	public Vector3 OffsetScale = Vector3.one;
	public int Index;
}
