using System;
using UnityEngine;

namespace Zelda.Gameplay
{
    [RequireComponent(typeof(Animator))]
    public partial class PlayerController // Animation
    {
        private enum EAnimationStates
        {
            Idle,
            Walking,
            Attacking,
            Dead
        }
        
        private Animator _animator;

        private float _direction;

        private void InitAnimation()
        {
            _animator = GetComponent<Animator>();
            _direction = 270f;
        }
        
        public void UpdateAnimation()
        {
            _animator.SetFloat("Direction", _direction);
            _animator.SetInteger("State", (int)GetAnimationState());
        }

        private EAnimationStates GetAnimationState()
        {
            return _states.CurrentState switch
            {
                EPlayerStates.Idle => EAnimationStates.Idle,
                EPlayerStates.Walking => EAnimationStates.Walking,
                EPlayerStates.Transition => EAnimationStates.Walking,
                EPlayerStates.Attacking => EAnimationStates.Attacking,
                EPlayerStates.Dead => EAnimationStates.Dead,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
