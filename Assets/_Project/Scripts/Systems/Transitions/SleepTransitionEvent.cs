using Zelda.Gameplay;

namespace Zelda.Systems.Transitions
{
    public readonly struct SleepTransitionEvent : ITransitionEvent
    {
        public float Duration { get; }

        public SleepTransitionEvent(float pDuration)
        {
            Duration = pDuration;
        }
        
        public void OnTrigger(GameManager pManager, PlayerController pPlayer)
        {
            pPlayer.Sleep();
        }

        public bool IsReady(float pActiveTime) => pActiveTime >= Duration;
    }
}
