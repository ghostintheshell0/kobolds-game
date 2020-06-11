using Leopotam.Ecs;
using UnityEngine;

public class CameraControllerSystem : IEcsRunSystem
{
	private readonly LevelData levelData = default;
	private Vector3 previousMousePos;

	public void Run()
	{
		if (levelData.EventsSystem.IsPointerOverGameObject()) return;
		if (levelData.EventsSystem.currentSelectedGameObject != null) return;

		var deltaZoom = Input.GetAxis("Mouse ScrollWheel") * levelData.CameraSettings.ZoomSpeed;

		if(deltaZoom != 0f)
		{
			var zoom = levelData.Camera.orthographicSize;
			levelData.Camera.orthographicSize = Mathf.Clamp(zoom + deltaZoom, levelData.CameraSettings.MinZoom, levelData.CameraSettings.MaxZoom);
		}

		if(Input.GetMouseButton(1))
		{
			var mouseDelta = previousMousePos - Input.mousePosition;
			var oldAngles = levelData.CameraObject.rotation.eulerAngles;
			var deltaX = mouseDelta.y * levelData.CameraSettings.RotationSpeed;
			var deltaY = mouseDelta.x * levelData.CameraSettings.RotationSpeed;

			var newX = Mathf.Clamp(oldAngles.x + deltaX, levelData.CameraSettings.MinXAngle, levelData.CameraSettings.MaxXAngle);

			levelData.CameraObject.rotation = Quaternion.Euler(newX, oldAngles.y + deltaY, oldAngles.z);
			
		}

		var xSpeed = Input.GetAxis("Horizontal");
		var ySpeed = Input.GetAxis("Vertical");

		if(xSpeed != 0f || ySpeed != 0f)
		{
			var delta = new Vector3(xSpeed, ySpeed, 0f) * (levelData.CameraSettings.MovingSpeed * Time.deltaTime);
			levelData.CameraObject.Translate(delta);
		}

		previousMousePos = Input.mousePosition;
	}
}