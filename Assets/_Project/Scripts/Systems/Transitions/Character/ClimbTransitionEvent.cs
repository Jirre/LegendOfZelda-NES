using UnityEngine;
using Zelda.Gameplay;

namespace Zelda.Systems.Transitions
{
    public readonly struct ClimbTransitionEvent : ITransitionEvent
    {
        public Vector2 StartOffset { get; }
        public Vector2 EndOffset { get; }
        public float Duration { get; }

        public ClimbTransitionEvent(Vector2 pStart, Vector2 pEnd, float pDuration)
        {
            StartOffset = pStart;
            EndOffset = pEnd;
            Duration = pDuration;
        }
        
        public void OnTrigger(GameManager pManager, PlayerController pPlayer)
        {
            pPlayer.Climb(StartOffset, EndOffset, Duration);
        }

        public bool IsReady(float pActiveTime) => pActiveTime >= Duration;
    }
}
