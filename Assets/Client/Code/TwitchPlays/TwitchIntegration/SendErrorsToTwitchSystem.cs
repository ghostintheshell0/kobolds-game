using Leopotam.Ecs;

public class SendErrorsToTwitchSystem : IEcsRunSystem
{
	private readonly EcsFilter<ErrorComponent> filter = default;
	private readonly EcsFilter<TwitchConnectionComponent> connections = default;

	public void Run()
	{
		foreach(var i in filter)
		{
			ref var mess = ref filter.Get1(i);
			foreach(var c in connections)
			{
				ref var connection = ref connections.Get1(i);
				connection.Client.SendMessage(connection.Channel, mess.Message);
			}
		}
	}
}