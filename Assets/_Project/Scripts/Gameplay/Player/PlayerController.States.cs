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
            Dead
        }
        
        private StateMachine<EPlayerStates> _states;

        private void InitStates()
        {
            _states = new StateMachine<EPlayerStates>();
            _states.AddState(EPlayerStates.Idle, IdleState);
            _states.AddState(EPlayerStates.Walking, WalkingState);
            
            _states.Goto(EPlayerStates.Idle);
        }

        private void UpdateState()
        {
            _states.Update(Time.deltaTime);
        }
        
        private void IdleState(State<EPlayerStates> pState)
        {
            if (_movementInput != Vector2.zero)
                _states.Goto(EPlayerStates.Walking);
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
    }
}
