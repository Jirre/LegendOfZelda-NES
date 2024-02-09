using UnityEngine;
using Zelda.Internal;
using Zelda.Systems.Transitions;
using Zelda.UI;
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

            _uiManager = FindObjectOfType<UIManager>();
            
            InitCamera();
            InitPlayer();
            
            _states.Goto(EStates.Gameplay);
        }

        private void GameplayState(State<EStates> pState)
        {
            if (_camera.IsInsideBounds(_player.transform.position) || 
                !_worldManager.CurrentRoom.BorderTransition)
                return;

            Vector2 delta = _camera.GetBorderDelta(_player.transform.position);
            Vector2 target = (Vector2) _player.transform.position + delta;
            _camera.DeltaGoto(Vector2Int.RoundToInt(delta));
            Transition(new MoveOverTimeTransitionEvent(target, 1.4f));
            _worldManager.UpdateCurrentRoom(Room.PositionToRoomIndex(target));
        }
        
        private void TransitionState(State<EStates> pState)
        {
            if (_transitionHandler.IsRunning)
            {
                _transitionHandler.Update(Time.deltaTime);
                return;
            }

            _worldManager.UpdateCurrentRoom(Room.PositionToRoomIndex(_player.transform.position));
            _player.Idle();
            _states.Goto(EStates.Gameplay);
        }
    }
}
