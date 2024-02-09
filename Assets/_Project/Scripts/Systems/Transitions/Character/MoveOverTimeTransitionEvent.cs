using UnityEngine;
using Zelda.Gameplay;

namespace Zelda.Systems.Transitions
{
    public readonly struct MoveOverTimeTransitionEvent : ITransitionEvent, ITransitionCompleteEvent
    {
        public Vector2 Target { get; }
        public float Duration { get; }

        public MoveOverTimeTransitionEvent(Vector2 pTarget, float pDuration)
        {
            Target = pTarget;
            Duration = pDuration;
        }
        
        public void OnTrigger(GameManager pManager, PlayerController pPlayer)
        {
            pPlayer.Translate(Target, Duration);
        }

        public void OnComplete(GameManager pManager, PlayerController pPlayer)
        {
            pPlayer.Translate(Target, true);
        }

        public bool IsReady(float pActiveTime) => pActiveTime >= Duration;
    }
}
