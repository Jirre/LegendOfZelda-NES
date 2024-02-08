using System;
using Zelda.Gameplay;

namespace Zelda.Systems.Transitions
{
    public interface ITransitionEvent
    {
        void OnTrigger(GameManager pManager, PlayerController pPlayer);

        bool IsReady(float pActiveTime);
    }
}
