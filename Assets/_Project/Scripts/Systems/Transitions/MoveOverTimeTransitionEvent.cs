using UnityEngine;
using Zelda.Gameplay;

namespace Zelda.Systems.Transitions
{
    public class MoveOverTimeTransitionEvent : ITransitionEvent
    {
        public Vector2Int Target { get; private set; }
        public float Duration { get; private set; }

        public MoveOverTimeTransitionEvent(Vector2Int pTarget, float pDuration)
        {
            Target = pTarget;
            Duration = pDuration;
        }
        
        public void OnTrigger(GameManager pManager, PlayerController pPlayer)
        {
            pPlayer.Translate(Target, Duration);
        }
        
        public bool IsReady(float pActiveTime) => pActiveTime >= Duration;
    }
}
