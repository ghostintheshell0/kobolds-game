using UnityEngine;

public class Skin : MonoBehaviour
{
	[SerializeField] private SkinnedMeshRenderer meshRenderer = default;
	[SerializeField] private Transform rightHand = default;
	[SerializeField] private Transform headTop = default;
	[SerializeField] private Transform head = default;
	public Transform Hat = default;

	public SkinnedMeshRenderer MeshRenderer => meshRenderer;
	public Transform RightHand => rightHand;
	public Transform HeadTop => headTop;
	public Transform Head => head;
}