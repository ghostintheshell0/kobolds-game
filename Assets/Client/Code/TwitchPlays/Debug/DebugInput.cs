using TMPro;
using Leopotam.Ecs;
using UnityEngine;

public class DebugInput : MonoBehaviour
{
	[SerializeField] private TMP_InputField playerNameField = default;
	[SerializeField] private TMP_InputField messageField = default;

	public EcsWorld World;

	public void Input()
	{
		var ent = World.NewEntity();
		ref var mess = ref ent.Set<TwitchMessageComponent>();

		mess.Sender = playerNameField.text;
		mess.Text = messageField.text;

		messageField.text = string.Empty;
	}

	public void ResetLevel()
	{
		var e = World.NewEntity();
		e.Set<EndGameComponent>();
	}
}