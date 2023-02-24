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
            _states.AddState(EPlayerStates.Attacking, AttackingState);
            
            _states.AddState(EPlayerStates.Transition, TransitionState);
            
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

            if (_attackMeleeInput)
            {
                _states.Goto(EPlayerStates.Attacking);
                return;
            }

            Rotate();
            Translate();
        }

        private void WalkingState(State<EPlayerStates> pState)
        {
            if (_movementInput == Vector2.zero)
            {
                _states.Goto(EPlayerStates.Idle);
                _rigidbody.velocity = Vector2.zero;
            }
            
            if (_attackMeleeInput)
            {
                _states.Goto(EPlayerStates.Attacking);
                return;
            }
            
            Rotate();
            Translate();
        }

        private void AttackingState(State<EPlayerStates> pState)
        {
            _rigidbody.velocity = Vector2.zero;
            if (pState.IsFirstFrame)
            {
                _WeaponRoot.gameObject.SetActive(true);
                _WeaponRoot.rotation = Quaternion.Euler(0, 0, _direction - 90f);
            }
            
            _WeaponRenderer.transform.localPosition = Vector3.up * 
                                                      Mathf.Clamp(
                                                          Mathf.Round(
                                                              Mathf.Sin((pState.ActiveTime / _AttackDuration) * Mathf.PI) * 4f) * 0.25f, 
                                                          0, 0.75f);

            if (!(pState.ActiveTime >= _AttackDuration)) return;
            _WeaponRoot.gameObject.SetActive(false);
            _states.Goto(EPlayerStates.Idle);
        }

        private void Rotate()
        {
            if (_movementInput != Vector2.zero)
                _direction = (Mathf.Atan2(_movementInput.y, _movementInput.x) * Mathf.Rad2Deg + 360f) % 360f;
        }
        
        private void Translate()
        {
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
