using System.Collections.Generic;
using UnityEngine;
using Zelda.Internal;
using Zelda.Systems.Transitions;
using Zelda.World;

namespace Zelda.Systems
{
    public partial class GameManager // States
    {
        private enum EStates
        {
            Init,
            Gameplay,
            Transition,
            GameOver
        }
        
        private StateMachine<EStates> _states;

        private void InitStates()
        {
            _states = new StateMachine<EStates>();
            _states.AddState(EStates.Init, InitState);
            _states.AddState(EStates.Gameplay, GameplayState);
            _states.AddState(EStates.Transition, TransitionState);
            
            _states.Goto(EStates.Init);
        }

        private void InitState(State<EStates> pState)
        {
            _worldManager = FindObjectOfType<WorldManager>();
            _worldManager.Initialize();
            
            InitCamera();
            InitPlayer();
            
            _states.Goto(EStates.Gameplay);
        }

        private void GameplayState(State<EStates> pState)
        {
            if (_camera.IsInsideBounds(_player.transform.position))
                return;

            Vector2Int delta = _camera.GetBorderDelta(_player.transform.position);
            _camera.DeltaGoto(delta);
            Transition(new MoveOverTimeTransitionEvent(delta, 1.4f));
        }
        
        
        private void TransitionState(State<EStates> pState)
        {
            _transitionQueue ??= new Queue<ITransitionEvent>();
            if (_currentTransition != null && !_currentTransition.IsReady(pState.ActiveTime))
                return;

            if (_transitionQueue.Count > 0)
            {
                _states.Goto(EStates.Gameplay);
                return;
            }
            
            _currentTransition = _transitionQueue.Dequeue();
            _currentTransition.OnTrigger(this, _player);
        }
    }
}
