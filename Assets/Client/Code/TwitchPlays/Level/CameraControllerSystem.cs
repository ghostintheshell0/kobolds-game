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

public class ObjectsRotationSystem : IEcsRunSystem
{
	private readonly EcsFilter<RotableComponent> objects = default;
	private readonly EcsFilter<RotationComponent> filter = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var rotation = ref filter.Get1(i);

			foreach(var o in objects)
			{
				ref var target = ref objects.Get1(o);

				var oldAngles = target.Transform.rotation.eulerAngles;
				var deltaX = rotation.Delta.y * target.Speed * Time.deltaTime;
				var deltaY = rotation.Delta.x * target.Speed * Time.deltaTime;
				var newX = Mathf.Clamp(oldAngles.x + deltaX, target.MinAngle.x, target.MaxAngle.x);

				target.Transform.rotation = Quaternion.Euler(newX, oldAngles.y + deltaY, oldAngles.z);

			}
			
			filter.GetEntity(i).Destroy();
		}
	}
}

public class ObjectMovingSystem : IEcsRunSystem
{

	private readonly EcsFilter<MovebleComponent> objects = default;
	private readonly EcsFilter<MoveComponent> filter = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var move = ref filter.Get1(i);

			foreach (var o in objects)
			{
				ref var target = ref objects.Get1(o);

				target.Transform.Translate(move.Delta * (Time.deltaTime * target.Speed));
			}
			filter.GetEntity(i).Destroy();
		}
	}
}


public class CameraZoomSystem : IEcsRunSystem
{
	private readonly EcsFilter<ZoomableCameraComponent> objects = default;
	private readonly EcsFilter<ZoomComponent> filter = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var zoom = ref filter.Get1(i);

			foreach (var o in objects)
			{
				ref var target = ref objects.Get1(o);

				var delta = zoom.Delta * Time.deltaTime * target.Speed;
				target.Camera.orthographicSize = Mathf.Clamp(target.Camera.orthographicSize + delta, target.MinZoom, target.MaxZoom);
			}

			filter.GetEntity(i).Destroy();
		}
	}
}

public struct RotableComponent
{
	public Transform Transform;
	public float Speed;
	public Vector3 MinAngle;
	public Vector3 MaxAngle;

}

public struct RotationComponent
{
	public Vector3 Delta;
}

public struct MovebleComponent
{
	public Transform Transform;
	public float Speed;
}

public struct MoveComponent
{
	public Vector3 Delta;
}

public struct ZoomableCameraComponent
{
	public Camera Camera;
	public float Speed;
	public float MinZoom;
	public float MaxZoom;
}

public struct ZoomComponent
{
	public float Delta;
}