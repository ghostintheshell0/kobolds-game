using Leopotam.Ecs;
using TMPro;
using UnityEngine;

public class TwitchConnectionUI : MonoBehaviour
{
	[SerializeField] private TMP_InputField channelField = default;
	[SerializeField] private TMP_InputField tokenField = default;
	[SerializeField] private string userName = default;

	public EcsWorld World = default;

	public void Join()
	{
		var e = World.NewEntity();
		ref var join = ref e.Set<TwitchChannelJoinComponent>();

		join.ChannelName = channelField.text;

		var connectEnt = World.NewEntity();
		ref var secret = ref connectEnt.Set<TwitchSecretComponent>();
		secret.Oauth = tokenField.text;
		secret.Channel = userName;

		tokenField.text = string.Empty;
		channelField.text = string.Empty;
	}
}