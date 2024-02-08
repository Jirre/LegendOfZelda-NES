using UnityEngine;
using Zelda.Gameplay;

namespace Zelda.Systems.Transitions
{
    public class ClimbTransitionEvent : ITransitionEvent
    {
        public Vector2 StartOffset { get; private set; }
        public Vector2 EndOffset { get; private set; }
        public float Duration { get; private set; }

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
