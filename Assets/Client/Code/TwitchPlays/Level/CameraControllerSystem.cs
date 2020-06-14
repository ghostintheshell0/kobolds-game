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

		ref var rotableMainCamera = ref mainCameraEnt.Set<RotableComponent>();
		rotableMainCamera.Transform = levelData.CameraObject;

		var arrowsCameraEnt = world.NewEntity();
		ref var rotableArrowsCamera = ref arrowsCameraEnt.Set<RotableComponent>();
		rotableArrowsCamera.Transform = levelData.GameUI.DirectionArrowsCameraObject.transform;
	}

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

			var e = world.NewEntity();
			ref var rotation = ref e.Set<RotationComponent>();

			var deltaX = mouseDelta.y * levelData.CameraSettings.RotationSpeed;
			var deltaY = mouseDelta.x * levelData.CameraSettings.RotationSpeed;

			rotation.Delta = new Vector3(deltaX, deltaY);

			
		}

		var xSpeed = Input.GetAxis("Horizontal");
		var ySpeed = Input.GetAxis("Vertical");

		if(xSpeed != 0f || ySpeed != 0f)
		{
			var e = world.NewEntity();
			ref var move = ref e.Set<MoveComponent>();
			move.Delta = new Vector3(xSpeed, ySpeed, 0f) * (levelData.CameraSettings.MovingSpeed * Time.deltaTime);
		}

		previousMousePos = Input.mousePosition;
	}
}

public class ObjectsRotationSystem : IEcsRunSystem
{
	private readonly EcsFilter<RotableComponent> objects = default;
	private readonly EcsFilter<RotationComponent> filter = default;
	private readonly LevelData levelData = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			ref var rotation = ref filter.Get1(i);

			foreach(var o in objects)
			{
				ref var target = ref objects.Get1(o);

				var oldAngles = target.Transform.rotation.eulerAngles;
				var newX = Mathf.Clamp(oldAngles.x + rotation.Delta.x, levelData.CameraSettings.MinXAngle, levelData.CameraSettings.MaxXAngle);

				target.Transform.rotation = Quaternion.Euler(newX, oldAngles.y + rotation.Delta.y, oldAngles.z);

			}
			
			filter.GetEntity(i).Destroy();
		}
	}
}

public class ObjectMovingSystem : IEcsRunSystem
{

	private readonly EcsFilter<MovebleComponent> objects = default;
	private readonly EcsFilter<MoveComponent> filter = default;
	private readonly LevelData levelData = default;

	public void Run()
	{
		foreach (var i in filter)
		{
			foreach (var o in objects)
			{
				ref var target = ref objects.Get1(o);

				ref var move = ref filter.Get1(i);
				levelData.CameraObject.Translate(move.Delta);
			}
			filter.GetEntity(i).Destroy();
		}
	}
}


public struct RotableComponent
{
	public Transform Transform;
}

public struct RotationComponent
{
	public Vector3 Delta;
}

public struct MovebleComponent
{
	public Transform Transform;
}

public struct MoveComponent
{
	public Vector3 Delta;
}