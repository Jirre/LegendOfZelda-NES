using UnityEngine;
using UnityEngine.InputSystem;

namespace Zelda.Gameplay
{
    [RequireComponent(typeof(PlayerInput))]
    public partial class PlayerController // Input
    {
        [SerializeField] private InputActionReference _MovementAction;
        [SerializeField] private InputActionReference _AttackAction;

        private PlayerInput _input;

        private Vector2 _movementInput;
        private bool _attackMeleeInput;
        
        private void SubscribeInput()
        {
            _input ??= GetComponent<PlayerInput>();
            
            _input.actions[_MovementAction.name].performed += MovementSetContext;
            _input.actions[_MovementAction.name].canceled += MovementSetContext;

            _input.actions[_AttackAction.name].performed += AttackSetContext;
            _input.actions[_AttackAction.name].canceled += AttackSetContext;
        }

        private void UnsubscribeInput()
        {
            _input ??= GetComponent<PlayerInput>();
            
            _input.actions[_MovementAction.name].performed -= MovementSetContext;
            _input.actions[_MovementAction.name].canceled -= MovementSetContext;
            
            _input.actions[_AttackAction.name].performed -= AttackSetContext;
            _input.actions[_AttackAction.name].canceled -= AttackSetContext;
        }

        private void MovementSetContext(InputAction.CallbackContext pContext)
        {
            _movementInput = pContext.ReadValue<Vector2>();
            if (_movementInput != Vector2.zero)
                _direction = (Mathf.Atan2(_movementInput.y, _movementInput.x) * Mathf.Rad2Deg + 360f) % 360f;
        }
        
        private void AttackSetContext(InputAction.CallbackContext pContext) => 
            _attackMeleeInput = pContext.ReadValue<bool>();
    }
}
