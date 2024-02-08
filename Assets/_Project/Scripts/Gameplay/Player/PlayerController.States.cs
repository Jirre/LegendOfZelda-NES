using System;
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
            
            Sleep,
            
            Translate,
            Climbing,
        }
        
        private StateMachine<EPlayerStates> _states;
        private Action<StateMachine<EPlayerStates>> _OnStateCompleteEvent;

        public EPlayerStates CurrentState => _states.CurrentState;

        private void InitStates()
        {
            _states = new StateMachine<EPlayerStates>();
            _states.AddState(EPlayerStates.Idle, IdleState);
            _states.AddState(EPlayerStates.Walking, WalkingState);
            _states.AddState(EPlayerStates.Attacking, AttackingState);
            
            _states.AddState(EPlayerStates.Sleep, SleepState);
            
            _states.AddState(EPlayerStates.Translate, TranslationState);
            _states.AddState(EPlayerStates.Climbing, ClimbingState);
            
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

        public void Sleep()
        {
            _states.Goto(EPlayerStates.Sleep);
        }
        
        /// <summary> Flag State </summary>
        private void SleepState(State<EPlayerStates> pState) { }

        private void Rotate()
        {
            if (_movementInput != Vector2.zero)
                _direction = (Mathf.Atan2(_movementInput.y, _movementInput.x) * Mathf.Rad2Deg + 360f) % 360f;
        }
        
        private void Translate()
        {
            _rigidbody.velocity = _movementInput * _MovementSpeed;
        }

        public float Translate(Vector2 pPosition, bool pInstantaneous = false)
        {
            if (pInstantaneous)
            {
                _rigidbody.position = pPosition;
                return 0f;
            }

            float duration = Vector2.Distance(_rigidbody.position, pPosition) / _MovementSpeed;
            Translate(pPosition, duration);
            return duration;
        }
        
        public void Translate(Vector2 pPosition, float pDuration)
        {
            _lerpStartPosition = _rigidbody.position;
            _lerpEndPosition = pPosition;
            _lerpDuration = pDuration;
            _states.Goto(EPlayerStates.Translate);
        }
        
        private void TranslationState(State<EPlayerStates> pState)
        {
            _rigidbody.position =
                Vector2.Lerp(_lerpStartPosition, _lerpEndPosition, pState.ActiveTime - 0.2f);

            if (pState.ActiveTime < _lerpDuration) return;
            _states.Goto(EPlayerStates.Idle);
        }

        public void Climb(Vector2 pStart, Vector2 pEnd, float pDuration)
        {
            _lerpStartPosition = pStart;
            _lerpEndPosition = pEnd;
            _lerpDuration = pDuration;
            
            _states.Goto(EPlayerStates.Climbing);
        }
        
        private void ClimbingState(State<EPlayerStates> pState)
        {
            _ModelRoot.localPosition =
                Vector2.Lerp(_lerpStartPosition, _lerpEndPosition, pState.ActiveTime / _lerpDuration);

            if (pState.ActiveTime < _lerpDuration) return;
            _states.Goto(EPlayerStates.Idle);
        }
    }
}
