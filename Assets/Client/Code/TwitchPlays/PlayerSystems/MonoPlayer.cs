using UnityEngine;
using UnityEngine.AI;

public class MonoPlayer : MonoBehaviour
{
	[SerializeField] private Skin skin = default;
	[SerializeField] private NavMeshAgent agent = default;
	[SerializeField] private Animator animator = default;

	public Skin Skin => skin;
	public NavMeshAgent Agent => agent;
	public Animator Animator => animator;
}