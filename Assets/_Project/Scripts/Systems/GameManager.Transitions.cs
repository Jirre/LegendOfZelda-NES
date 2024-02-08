using System.Collections.Generic;
using Zelda.Systems.Transitions;

namespace Zelda.Systems
{
    public partial class GameManager // Transitions
    {
        private Queue<ITransitionEvent> _transitionQueue;
        private ITransitionEvent _currentTransition;
        
        public void Transition(ITransitionEvent pEvent)
        {
            _transitionQueue ??= new Queue<ITransitionEvent>();
            _transitionQueue.Enqueue(pEvent);
            
            if (_states.CurrentState != EStates.Transition)
                _states.Goto(EStates.Transition);
        }
    }
}
