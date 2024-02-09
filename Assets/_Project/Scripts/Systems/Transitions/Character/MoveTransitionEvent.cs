using UnityEngine;
using Zelda.Gameplay;

namespace Zelda.Systems.Transitions
{
    public struct MoveTransitionEvent : ITransitionEvent, ITransitionCompleteEvent
    {
        public Vector2 Target { get; }
        public bool Instantaneous { get; }

        private float _duration;

        public MoveTransitionEvent(Vector2 pTarget, bool pInstantaneous = false)
        {
            Target = pTarget;
            Instantaneous = pInstantaneous;
            _duration = 0;
        }
        
        public void OnTrigger(GameManager pManager, PlayerController pPlayer)
        {
            _duration = pPlayer.Translate(Target, Instantaneous);
        }

        public void OnComplete(GameManager pManager, PlayerController pPlayer)
        {
            if (Instantaneous)
                return;
            pPlayer.Translate(Target, true);
        }

        public bool IsReady(float pActiveTime) => Instantaneous || pActiveTime >= _duration;
    }
}
