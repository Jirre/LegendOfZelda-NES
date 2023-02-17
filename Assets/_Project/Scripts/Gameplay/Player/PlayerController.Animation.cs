using UnityEngine;

namespace Zelda.Gameplay
{
    [RequireComponent(typeof(Animator))]
    public partial class PlayerController // Animation
    {
        private Animator _animator;

        private float _direction;

        private void InitAnimation()
        {
            _animator = GetComponent<Animator>();
            _direction = 270f;
        }
        
        public void UpdateAnimation()
        {
            Debug.Log(_direction);
            _animator.SetFloat("Direction", _direction);
            _animator.SetInteger("State", (int)_states.CurrentState);
        }
    }
}
