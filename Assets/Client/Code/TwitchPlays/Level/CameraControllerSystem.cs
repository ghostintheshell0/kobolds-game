using Leopotam.Ecs;
using UnityEngine;

public class CameraControllerSystem : IEcsRunSystem, IEcsInitSystem
{
	private readonly LevelData levelData = default;
	private readonly EcsWorld world = default;

	private Vector3 previousMousePos;

	public void Init()
	{
		var mainCameraEnt = world.NewEntity();
		ref var movableMainCamera = ref mainCameraEnt.Set<MovebleComponent>();
		movableMainCamera.Transform = levelData.CameraObject;
		movableMainCamera.Speed = levelData.CameraSettings.MovingSpeed;

		ref var rotableMainCamera = ref mainCameraEnt.Set<RotableComponent>();
		rotableMainCamera.Transform = levelData.CameraObject;
		rotableMainCamera.Speed = levelData.CameraSettings.RotationSpeed;
		rotableMainCamera.MinAngle = levelData.CameraSettings.MinAngle;
		rotableMainCamera.MaxAngle = levelData.CameraSettings.MaxAngle;

		ref var zoomableCamera = ref mainCameraEnt.Set<ZoomableCameraComponent>();
		zoomableCamera.Camera = levelData.Camera;
		zoomableCamera.Speed = levelData.CameraSettings.ZoomSpeed;
		zoomableCamera.MinZoom = levelData.CameraSettings.MinZoom;
		zoomableCamera.MaxZoom = levelData.CameraSettings.MaxZoom;

		var arrowsCameraEnt = world.NewEntity();
		ref var rotableArrowsCamera = ref arrowsCameraEnt.Set<RotableComponent>();
		rotableArrowsCamera.Transform = levelData.GameUI.DirectionArrowsCameraObject.transform;
		rotableArrowsCamera.Speed = levelData.CameraSettings.RotationSpeed;
		rotableArrowsCamera.MinAngle = levelData.CameraSettings.MinAngle;
		rotableArrowsCamera.MaxAngle = levelData.CameraSettings.MaxAngle;
	}

	public void Run()
	{
		if (!levelData.EventsSystem.IsPointerOverGameObject())
		{

			if (levelData.EventsSystem.currentSelectedGameObject != null) return;

			var deltaZoom = Input.GetAxis("Mouse ScrollWheel");

			if (deltaZoom != 0f)
			{
				var e = world.NewEntity();
				ref var zoom = ref e.Set<ZoomComponent>();
				zoom.Delta = deltaZoom;
			}

			if (Input.GetMouseButton(1))
			{
				var mouseDelta = previousMousePos - Input.mousePosition;

				var e = world.NewEntity();
				ref var rotation = ref e.Set<RotationComponent>();
				rotation.Delta = new Vector3(mouseDelta.x, mouseDelta.y);

			}

			var xSpeed = Input.GetAxis("Horizontal");
			var ySpeed = Input.GetAxis("Vertical");

			if (xSpeed != 0f || ySpeed != 0f)
			{
				var e = world.NewEntity();
				ref var move = ref e.Set<MoveComponent>();
				move.Delta = new Vector3(xSpeed, ySpeed, 0f);
			}
		}
		previousMousePos = Input.mousePosition;
	}
}