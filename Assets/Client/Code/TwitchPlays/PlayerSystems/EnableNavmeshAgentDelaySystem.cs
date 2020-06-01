using Leopotam.Ecs;

public class EnableNavmeshAgentDelaySystem : IEcsRunSystem
{
	private readonly EcsFilter<EnableNavmeshAgentDelayComponent> filter = default;

	public void Run()
	{
		foreach(var i in filter)
		{
			ref var delayComp = ref filter.Get1(i);
			delayComp.FramesLeft++;
			if(delayComp.FramesLeft >= delayComp.Frames)
			{
				var playerEnt = filter.GetEntity(i);
				ref var player = ref playerEnt.Set<PlayerComponent>();
				player.View.Agent.enabled = true;
				playerEnt.Unset<EnableNavmeshAgentDelayComponent>();
			}
		}
	}
}