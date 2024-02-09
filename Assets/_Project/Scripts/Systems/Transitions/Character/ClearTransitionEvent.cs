using UnityEngine;
using Zelda.Gameplay;

namespace Zelda.Systems.Transitions
{
    public readonly struct ClearTransitionEvent : ITransitionEvent
    {
        public void OnTrigger(GameManager pManager, PlayerController pPlayer)
        {
            pPlayer.Climb(Vector2.zero, Vector2.zero, true);
            pPlayer.Sleep();
        }

        public bool IsReady(float pActiveTime) => true;
    }
}
