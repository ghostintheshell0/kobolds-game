using UnityEngine;
using UnityEngine.AI;

public class MonoFloor : MonoBehaviour
{
	[SerializeField] private NavMeshSurface surface = default;
	[SerializeField] private BoxCollider boxCollider = default;
	[SerializeField] private RandomTransformList tiles = default;

	public void SetSize(Vector2 size)
	{
		boxCollider.size = new Vector3(size.x, boxCollider.size.y, size.y);
	}

	public NavMeshSurface Surface => surface;
	public RandomTransformList Tiles => tiles;
}
