using UnityEngine;

public static class WallComponentExtensions
{
	public static void EnablePlane(this ref WallComponent wall, Vector2Int direction)
	{
		var plane = GetPlane(ref wall, direction);
		plane.SetActive(true);
	}

	public static void DisablePlane(this ref WallComponent wall, Vector2Int direction)
	{
		var plane = GetPlane(ref wall, direction);
		plane.SetActive(false);
	}

	public static void DisableAllPlanes(this ref WallComponent wall)
	{
		wall.View.Left.SetActive(false);
		wall.View.Right.SetActive(false);
		wall.View.Forward.SetActive(false);
		wall.View.Backward.SetActive(false);
	}

	private static GameObject GetPlane(this ref WallComponent wall, Vector2Int direction)
	{
		if (direction.x < 0) return wall.View.Left;
		if (direction.x > 0) return wall.View.Right;
		if (direction.y < 0) return wall.View.Backward;
		return wall.View.Forward;
	}
}