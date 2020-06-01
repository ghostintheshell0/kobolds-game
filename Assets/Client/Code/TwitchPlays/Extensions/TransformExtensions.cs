using UnityEngine;

public static class TransformExtensions
{
	public static bool IsNear(this Transform transform, Vector3 target, float distance)
	{
	//	Debug.Log($"from {transform.position} to {target} = {(target - transform.position).sqrMagnitude}");
		return (target - transform.position).sqrMagnitude < distance * distance;
	}
}