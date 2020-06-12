using UnityEngine;

public class Skin : MonoBehaviour
{
	[SerializeField] private Transform rightHand = default;
	[SerializeField] private Transform head = default;
	[SerializeField] private Transform headTop = default;
	[SerializeField] private SkinnedMeshRenderer meshRenderer = default;
	
	public SkinnedMeshRenderer MeshRenderer => meshRenderer;
	public Transform Head => head;
	public Transform HeadTop => headTop;
	public Transform RightHand => rightHand;
}