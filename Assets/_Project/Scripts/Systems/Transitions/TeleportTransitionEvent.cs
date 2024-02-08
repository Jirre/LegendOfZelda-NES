using UnityEngine;
using Zelda.Gameplay;
using Zelda.World;

namespace Zelda.Systems.Transitions
{
    public class TeleportTransitionEvent : ITransitionEvent
    {
        public EWorldLayer Layer { get; private set; }
        public Vector2Int Room { get; private set; }
        public Vector2 Position { get; private set; }

        public TeleportTransitionEvent(EWorldLayer pLayer, Vector2Int pRoom, Vector2 pPosition)
        {
            Layer = pLayer;
            Room = pRoom;
            Position = pPosition;
        }
        
        public void OnTrigger(GameManager pManager, PlayerController pPlayer)
        {
            pPlayer.Translate(World.Room.GetRoomPosition(Room, Position), true);
        }

        public bool IsReady(float pActiveTime) => true;
    }
}
