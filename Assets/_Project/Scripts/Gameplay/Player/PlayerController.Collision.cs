using System;
using UnityEngine;
using Zelda.Systems.Transitions;
using Zelda.World;

namespace Zelda.Gameplay
{
    public partial class PlayerController // Collision
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<TeleporterBehaviour>() != null)
                OnTeleporterCollision(other.GetComponent<TeleporterBehaviour>());
        }

        private void OnTeleporterCollision(TeleporterBehaviour pBehaviour)
        {
            if (CurrentState != EPlayerStates.Idle &&
                CurrentState != EPlayerStates.Walking &&
                CurrentState != EPlayerStates.Attacking) return;

            Vector2 targetPos = new Vector2(
                pBehaviour.WalkTargetX ? pBehaviour.transform.position.x : _rigidbody.position.x,
                pBehaviour.WalkTargetY ? pBehaviour.transform.position.y : _rigidbody.position.y);
            
            if (!pBehaviour.Invert)
            {
                _gameManager.Transition(new MoveTransitionEvent(targetPos));
                _gameManager.Transition(new ClimbTransitionEvent(Vector2.zero, Vector2.down, 1.4f));
                _gameManager.Transition(new HideScreenTransitionEvent(0.2f));
                _gameManager.Transition(new TeleportTransitionEvent(pBehaviour.Layer, pBehaviour.Room, pBehaviour.RoomPosition));
                _gameManager.Transition(new ClearTransitionEvent());
                _gameManager.Transition(new SleepTransitionEvent(0.2f));
                _gameManager.Transition(new MoveTransitionEvent(Room.GetRoomPosition(pBehaviour.Room, pBehaviour.RoomPosition + pBehaviour.WalkInOffset)));
                return;
            }
            
            _gameManager.Transition(new MoveTransitionEvent(targetPos));
            _gameManager.Transition(new HideScreenTransitionEvent(0.2f));
            _gameManager.Transition(new TeleportTransitionEvent(pBehaviour.Layer, pBehaviour.Room, pBehaviour.RoomPosition));
            _gameManager.Transition(new ClimbTransitionEvent(Vector2.down, Vector2.down, 0.1f));
            _gameManager.Transition(new SleepTransitionEvent(0.1f));
            _gameManager.Transition(new ClimbTransitionEvent(Vector2.down, Vector2.zero, 1.4f));
            _gameManager.Transition(new ClearTransitionEvent());
            _gameManager.Transition(new MoveTransitionEvent(Room.GetRoomPosition(pBehaviour.Room, pBehaviour.RoomPosition + pBehaviour.WalkInOffset)));
        }
    }
}
