using UnityEngine;
using Zelda.Internal;

namespace Zelda.Gameplay
{
    public partial class PlayerController // States
    {
        public enum EPlayerStates
        {
            Idle, 
            Walking,
            Attacking,
            Dead,
            
            Transition,
        }
        
        private StateMachine<EPlayerStates> _states;

        private void InitStates()
        {
            _states = new StateMachine<EPlayerStates>();
            _states.AddState(EPlayerStates.Idle, IdleState);
            _states.AddState(EPlayerStates.Walking, WalkingState);
            
            _states.AddState(EPlayerStates.Transition, TransitionState);
            
            _states.Goto(EPlayerStates.Idle);
        }

        private void UpdateState()
        {
            Debug.Log(_states.CurrentState);
            _states.Update(Time.deltaTime);
        }
        
        private void IdleState(State<EPlayerStates> pState)
        {
            if (_movementInput != Vector2.zero)
                _states.Goto(EPlayerStates.Walking);
            
            _rigidbody.velocity = Vector2.zero;
        }

        private void WalkingState(State<EPlayerStates> pState)
        {
            if (_movementInput == Vector2.zero)
            {
                _states.Goto(EPlayerStates.Idle);
                _rigidbody.velocity = Vector2.zero;
            }
            
            _rigidbody.velocity = _movementInput * _MovementSpeed;
        }

        public void DeltaTransition(Vector2Int pDelta)
        {
            _transitionStartPosition = transform.position;
            _transitionEndPosition = _transitionStartPosition + pDelta;
            
            _states.Goto(EPlayerStates.Transition);
        }
        
        private void TransitionState(State<EPlayerStates> pState)
        {
            _rigidbody.position =
                Vector2.Lerp(_transitionStartPosition, _transitionEndPosition, pState.ActiveTime - 0.2f);
            
            if (pState.ActiveTime >= 1.4f)
                _states.Goto(EPlayerStates.Idle);
        }
    }
}
