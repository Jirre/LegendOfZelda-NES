using UnityEngine;
using Zelda.Internal;

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
            InitRooms();
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
            _player.DeltaTransition(delta);

            _states.Goto(EStates.Transition);
        }

        private void TransitionState(State<EStates> pState)
        {
            if (pState.ActiveTime > 1.4f)
                _states.Goto(EStates.Gameplay);
        }
    }
}
