using Zelda.Gameplay;

namespace Zelda.Systems.Transitions
{
    public interface ITransitionEvent
    {
        void OnTrigger(GameManager pManager, PlayerController pPlayer);

        bool IsReady(float pActiveTime);
    }

    public interface ITransitionCompleteEvent
    {
        void OnComplete(GameManager pManager, PlayerController pPlayer);
    }
}
