using System;
using UnityEngine;
using Zelda.Systems.Transitions;
using Zelda.World;

namespace Zelda.Gameplay
{
    public partial class PlayerController // Collision
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<TeleporterBehaviour>() != null)
                OnTeleporterCollision(other.GetComponent<TeleporterBehaviour>());
        }

        private void OnTeleporterCollision(TeleporterBehaviour pBehaviour)
        {
            if (CurrentState is 
                EPlayerStates.Walking or 
                EPlayerStates.Idle or 
                EPlayerStates.Attacking) return;

            Vector2Int pTarget = Vector2Int.RoundToInt(pBehaviour.transform.position);
            _gameManager.Transition(new MoveTransitionEvent(pTarget));
            _gameManager.Transition(new ClimbTransitionEvent(Vector2.zero, Vector2.down, 1.4f));
            _gameManager.Transition(new TeleportTransitionEvent(pBehaviour.Layer, pBehaviour.Room, pBehaviour.Position));
            _gameManager.Transition(new MoveTransitionEvent(Room.GetRoomPosition(pBehaviour.Room, pBehaviour.Position + pBehaviour.WalkInOffset)));
        }
    }
}
