using Zelda.Gameplay;
using Zelda.UI.Visualizers;

namespace Zelda.Systems.Transitions
{
    public readonly struct HideScreenTransitionEvent : ITransitionEvent
    {
        public ScreenFadeContext Context { get; }

        public HideScreenTransitionEvent(ScreenFadeContext pContext)
        {
            Context = pContext;
        }

        public HideScreenTransitionEvent(float pDuration) =>
            Context = new ScreenFadeContext(pDuration);
        
        public void OnTrigger(GameManager pManager, PlayerController pPlayer)
        {
            pManager.HideScreen(Context);
        }

        public bool IsReady(float pActiveTime) => true;
    }
}
