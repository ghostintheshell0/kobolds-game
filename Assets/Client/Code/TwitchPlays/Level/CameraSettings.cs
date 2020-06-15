using UnityEngine;

[System.Serializable]
public struct CameraSettings
{
	public float MinZoom;
	public float MaxZoom;
	public float ZoomSpeed;
	public float RotationSpeed;
	public Vector3 MinAngle;
	public Vector3 MaxAngle;
	public float MovingSpeed;
}