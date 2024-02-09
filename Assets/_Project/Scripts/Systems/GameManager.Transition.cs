using Zelda.Systems.Transitions;
using Zelda.UI.Visualizers;

namespace Zelda.Systems
{
    public partial class GameManager // Transition
    {
        private TransitionHandler _transitionHandler;
        
        public void Transition(ITransitionEvent pEvent)
        {
            _transitionHandler ??= new TransitionHandler(this, _player);
            _transitionHandler.Add(pEvent);
            
            if (_states.CurrentState != EStates.Transition)
                _states.Goto(EStates.Transition);
        }

        public void HideScreen(ScreenFadeContext pContext) => 
            _uiManager.ScreenScreenFade.SetContext(pContext);
    }
}
