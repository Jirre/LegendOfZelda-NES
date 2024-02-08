using UnityEngine;
using Zelda.Gameplay;

namespace Zelda.Systems.Transitions
{
    public class MoveTransitionEvent : ITransitionEvent
    {
        public Vector2 Target { get; private set; }
        public bool Instantaneous { get; private set; }

        private float _duration;

        public MoveTransitionEvent(Vector2 pTarget, bool pInstantaneous = false)
        {
            Target = pTarget;
            Instantaneous = pInstantaneous;
        }
        
        public void OnTrigger(GameManager pManager, PlayerController pPlayer)
        {
            _duration = pPlayer.Translate(Target, Instantaneous);
        }
        
        public bool IsReady(float pActiveTime) => Instantaneous || pActiveTime >= _duration;
    }
}
