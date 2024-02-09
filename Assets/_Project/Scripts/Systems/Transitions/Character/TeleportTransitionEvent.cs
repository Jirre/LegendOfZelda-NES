using UnityEngine;
using Zelda.Gameplay;
using Zelda.World;

namespace Zelda.Systems.Transitions
{
    public readonly struct TeleportTransitionEvent : ITransitionEvent
    {
        public EWorldLayer Layer { get;  }
        public Vector2Int Room { get;  }
        public Vector2 Position { get;  }

        public TeleportTransitionEvent(EWorldLayer pLayer, Vector2Int pRoom, Vector2 pPosition)
        {
            Layer = pLayer;
            Room = pRoom;
            Position = pPosition;
        }
        
        public void OnTrigger(GameManager pManager, PlayerController pPlayer)
        {
            pManager.SetLayer(Layer);
            pPlayer.Translate(World.Room.GetRoomPosition(Room, Position), true);
        }

        public bool IsReady(float pActiveTime) => true;
    }
}
